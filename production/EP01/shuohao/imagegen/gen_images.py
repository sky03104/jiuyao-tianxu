"""
EP01〈第七室報到〉出圖腳本（OpenAI 圖像 API）

讀 shuohao-skills 五段產出的 JSON，組出最終提示詞後呼叫 OpenAI 出圖：
  sheets  : 角色設定圖（characters/images/）、場景與道具設定圖（art/images/）
  frames  : 分鏡圖（storyboard/export/h3/<段號>/f<序>.png），掛設定圖當參考

畫風統一在這裡附加（依 docs/MASTER_VISUAL_STYLE_LOCK_V1.0.md），各段 JSON 的提示詞本身不寫畫風。
金鑰建議放在雲端環境的「API credentials」（主機 api.openai.com，Authorization: Bearer <key>）：
由代理在請求離開 VM 後附上，工作階段看不到金鑰，也不必另外放行網路。
本機執行或用環境變數時，照常設 OPENAI_API_KEY。

用法：
  python gen_images.py sheets --dry-run            # 只列工作，不呼叫 API
  python gen_images.py sheets                      # 出 10 張設定圖（已存在的跳過）
  python gen_images.py sheets --only 齊衡烈 --force  # 重出指定一張
  python gen_images.py frames --segments E01-01,E01-02
  python gen_images.py jobs                        # 寫 IMAGE_JOBS.md（手動貼 ChatGPT 用的備援清單）
"""
import argparse
import base64
import datetime
import hashlib
import json
import os
import sys
import time

HERE = os.path.dirname(os.path.abspath(__file__))
ROOT = os.path.dirname(HERE)  # production/EP01/shuohao

CAST = os.path.join(ROOT, "characters", "EP01-cast.json")
ART = os.path.join(ROOT, "art", "EP01-art.json")
SCRIPT = os.path.join(ROOT, "script", "EP01-script.json")
STORYBOARD = os.path.join(ROOT, "storyboard", "EP01-storyboard.json")
MANIFEST = os.path.join(HERE, "manifest.json")

# GPT 參考稿選版（見 DECISIONS.md）：只鎖臉、髮型與整體氣質，服裝武器以文字為準
GPT_REFS = {
    "C01": "PLAYER_GPT.png",
    "C02": "QI_HENGLIE_GPT.png",
    "C03": "YU_CENYE_GPT.png",
    "C04": "LI_RUOFENG_FACE_GPT.png",  # 只給臉：全身稿的兜帽／肩箭袋／無袖會被模型照抄
    "C05": "XIAO_YAOLIN_GPT.png",
    "S01": "TIANXUAN_ACADEMY_GPT.png",
    "S02": "EAST_CORRIDOR_GPT.png",
}

# ---- 畫風層（MASTER_VISUAL_STYLE_LOCK_V1.0 一～七節）----
# 2026-09-24 第一批純文字出圖偏寫實照片感、與咖哩選定的 GPT 參考稿落差大，
# 改為：掛參考稿＋畫風句明確描述參考稿的渲染質感（見 DECISIONS.md）
STYLE_RENDER = (
    "Rendering style: match the attached reference images exactly — polished Chinese xianxia MMORPG key-art "
    "CG in the look of a premium donghua (Chinese 3D animation) feature: semi-realistic stylized 3D, beautiful "
    "idealised young faces with smooth luminous skin, clear bright detailed eyes with strong catchlights, "
    "crisp glossy highlights on hair, metal and leather, rich fine costume detailing with antique-gold "
    "filigree, soft bloom and clean airy colour. It must NOT look like a real photograph, a real person, "
    "a 3D scan or a costume-drama still. "
    "Art direction: premium 3D Eastern xuanhuan MMORPG cinematic art — a high-end 3D game character and "
    "environment render with the finish of an Eastern fantasy animated feature film. Stylized 3D, "
    "NOT a photograph, not live-action, not a period TV drama, not 2D anime, not chibi, not western fantasy. "
    "Natural adult proportions, slim and fit young characters, refined East Asian faces with personality in the "
    "eyes, no oversized anime eyes, no bodybuilder musculature. Materials: fine fabric, detailed leather, antique "
    "bronze, jade and wood with subtle mystical texture. Palette: deep teal, ink black, dark blue, dark brown, "
    "warm gold accents, small touches of vermilion. All characters are adults and fully clothed."
)
STYLE_WORLD = (
    "Cinematic environmental light, depth of field, volumetric light, aerial perspective, a little morning mist, "
    "natural shadows; everything exists inside the world, never a studio backdrop."
)
# 逐項補強：參考稿或模型沒守住的規格再寫死一次（依驗圖紀錄累積，見 DECISIONS.md）
FIX = {
    "厲若楓": "COSTUME CORRECTIONS (override the references): NO hood anywhere, in every view. NO shoulder quiver "
              "and no arrows on the back — his arrows are kept only in a small closed cylindrical lacquered arrow "
              "case at the right hip. The bow is a compact THREE-SECTION folding short bow: three clearly jointed "
              "segments of dark lacquered wood with bronze joint fittings, slung diagonally across the back. Both "
              "sleeves are full length (not sleeveless). Exactly ONE leather arm guard, on the LEFT forearm only. "
              "The academy badge is antique bronze inset with carved jade — no skull or other motif.",
    "齊衡烈": "CORRECTIONS: exactly ONE long leather gauntlet, on the LEFT forearm only; the right forearm has no "
              "gauntlet, only the red-gold arm ring on the right upper arm. Arms strong but lean and relaxed, not a "
              "bodybuilder. The weapon is a single heavy single-edged broad saber (dao) with a thick straight back.",
    "蕭曜霖": "CORRECTIONS: exactly ONE weapon in total — a single huge door-plank heavy sword strapped diagonally "
              "across his back, one long grip rising above the right shoulder; no second sword, no weapon at the "
              "hip. No fur collar. The pale-gold mark sits on the brow bone between the eyebrows.",
    "第七室": "CORRECTION: a FOUR-person room — exactly FOUR low sleeping platforms, TWO end to end along the left "
              "wall and TWO along the right wall, each with a wooden chest at its foot, all visible in the main view.",
}


def fix_of(name):
    return f"\n\n{FIX[name]}" if name in FIX else ""


SHEET_SIZE = "1536x1024"  # chatgpt-image-latest 只支援 1024x1024／1024x1536／1536x1024
NO_TEXT = "No text, no letters, no labels, no captions, no logos, no watermark, no UI."


def load(path):
    with open(path, encoding="utf-8") as f:
        return json.load(f)


def ref_paths(key):
    names = GPT_REFS.get(key) or []
    names = [names] if isinstance(names, str) else names
    return [p for p in (os.path.join(HERE, "refs", n) for n in names) if os.path.exists(p)]


def char_sheet_path(name):
    return os.path.join(ROOT, "characters", "images", f"{name}-sheet.png")


def art_sheet_path(name):
    return os.path.join(ROOT, "art", "images", f"{name}-sheet.png")


def build_sheet_jobs():
    cast, art = load(CAST), load(ART)
    jobs = []
    for c in cast["characters"]:
        refs = ref_paths(c.get("id"))
        note = (
            "The attached reference images show the approved look of this character: match the rendering style, "
            "the face, facial proportions and hairstyle closely. Where the references differ from the text below "
            "(costume, armour, weapon, accessories), follow the text. Ignore any text, logos or UI in the "
            "references.\n\n"
            if refs else ""
        )
        prompt = (f"{STYLE_RENDER}\n\n{note}{c['image']['sheet']}{fix_of(c['name'])}\n\n"
                  f"Avoid: {c['image']['negativePrompt']}. {NO_TEXT}")
        jobs.append({"key": f"角色：{c['name']}", "id": c.get("id"), "name": c["name"], "kind": "sheet",
                     "out": char_sheet_path(c["name"]), "refs": refs, "size": SHEET_SIZE, "prompt": prompt})
    for s in art["scenes"]:
        refs = ref_paths(s["id"])
        note = (
            "The attached reference image shows the approved mood and architecture of this location: keep its "
            "architectural language, materials and atmosphere, but follow the text below for layout and details. "
            "The reference may contain people; the sheet must contain none.\n\n" if refs else ""
        )
        prompt = (f"{STYLE_RENDER} {STYLE_WORLD}\n\n{note}{s['image']['sheet']}{fix_of(s['name'])}\n\n"
                  f"Avoid: {s['image']['negativePrompt']}. {NO_TEXT}")
        jobs.append({"key": f"場景：{s['name']}", "id": s["id"], "name": s["name"], "kind": "sheet",
                     "out": art_sheet_path(s["name"]), "refs": refs, "size": SHEET_SIZE, "prompt": prompt})
    for p in art.get("props", []):
        prompt = (f"{STYLE_RENDER}\n\n{p['image']['sheet']}\n\n"
                  f"Avoid: {p['image']['negativePrompt']}. {NO_TEXT}")
        jobs.append({"key": f"道具：{p['name']}", "id": p["id"], "name": p["name"], "kind": "sheet",
                     "out": art_sheet_path(p["name"]), "refs": [], "size": SHEET_SIZE, "prompt": prompt})
    return jobs


def build_frame_jobs(segments_filter=None):
    cast, art, script, sb = load(CAST), load(ART), load(SCRIPT), load(STORYBOARD)
    char_name = {c["id"]: c["name"] for c in cast["characters"]}
    scene_by_id = {s["id"]: s for s in art["scenes"]}
    prop_by_id = {p["id"]: p for p in art.get("props", [])}
    ep_script = script["episodes"][0]
    jobs = []
    for seg in sb["episodes"][0]["segments"]:
        if segments_filter and seg["id"] not in segments_filter:
            continue
        sc = ep_script["scenes"][seg["sceneIndex"] - 1]
        scene = scene_by_id[sc["sceneId"]]
        light = next((l["prompt"] for l in scene["lighting"] if l["state"] == sc.get("lighting")), "")
        seg_dir = os.path.join(ROOT, "storyboard", "export", "h3", seg["id"])
        for k, cut in enumerate(seg["cuts"], start=1):
            refs, lines = [], []
            refs.append(art_sheet_path(scene["name"]))
            lines.append(f"Image {len(refs)}: environment sheet of this location ({scene['name']}) — match its "
                         "architecture, materials, layout and wear exactly; the frame shows the same place.")
            for cid in cut.get("characters", []):
                refs.append(char_sheet_path(char_name[cid]))
                lines.append(f"Image {len(refs)}: character model sheet of {char_name[cid]} — this is the person "
                             f"called {char_name[cid]} in the shot text; match the SAME face, hairstyle, costume "
                             "and weapon exactly.")
            for pid in cut.get("props", []):
                refs.append(art_sheet_path(prop_by_id[pid]["name"]))
                lines.append(f"Image {len(refs)}: prop sheet of {prop_by_id[pid]['name']} — match this object "
                             "exactly; any plaque or token surface stays blank, with no characters on it.")
            if k > 1:
                refs.append(os.path.join(seg_dir, "f1.png"))
                lines.append(f"Image {len(refs)}: the opening frame of this same sequence — keep the world, "
                             "lighting, mist density and every character's look consistent with it; do not copy "
                             "its composition.")
            prompt = (
                "Reference images:\n" + "\n".join(lines) + "\n\n"
                f"{STYLE_RENDER} {STYLE_WORLD}\n\n"
                f"Lighting: {light}\n\n"
                f"Blocking (Chinese): {seg['blocking']}\n\n"
                f"Shot (Chinese): {cut['frame']}\n\n"
                f"Camera: {cut['lens']}; position {cut['cameraPosition']}; composition {cut['composition']}; "
                f"eyeline {cut['eyeline']}; focus {cut['focus']}.\n\n"
                "Vertical 9:16 portrait frame. Only the characters named in the shot text appear, plus any "
                "background students the shot text explicitly mentions. "
                "No text, no watermark, no borders — a single clean full-bleed frame."
            )
            jobs.append({"key": f"分鏡：{seg['id']} f{k}", "id": f"{seg['id']}-f{k}", "kind": "frame",
                         "out": os.path.join(seg_dir, f"f{k}.png"), "refs": refs, "size": "1024x1536",
                         "prompt": prompt})
    return jobs


def load_manifest():
    return load(MANIFEST) if os.path.exists(MANIFEST) else {}


def save_manifest(m):
    with open(MANIFEST, "w", encoding="utf-8") as f:
        json.dump(m, f, ensure_ascii=False, indent=2)


def rel(p):
    return os.path.relpath(p, ROOT)


def final_b64(stream):
    b64 = None
    for ev in stream:
        if ev.type.endswith(".completed"):
            b64 = ev.b64_json
    if not b64:
        raise RuntimeError("串流結束但沒有收到完成的圖")
    return b64


def run_jobs(jobs, args):
    from openai import OpenAI  # 延後匯入：dry-run / jobs 不需要

    # 雲端環境用 API credentials 時，代理會替 api.openai.com 的請求附上真正的金鑰；
    # SDK 仍要求有值，所以沒設環境變數時給一個佔位字串。
    client = OpenAI(api_key=os.environ.get("OPENAI_API_KEY") or "injected-by-agent-proxy")
    manifest = load_manifest()
    done = skipped = failed = 0
    for j in jobs:
        if os.path.exists(j["out"]) and not args.force:
            print(f"＝ 已存在，跳過：{j['key']}（{rel(j['out'])}）")
            skipped += 1
            continue
        missing = [r for r in j["refs"] if not os.path.exists(r)]
        if missing:
            print(f"✗ 缺參考圖，跳過：{j['key']} ← {', '.join(rel(m) for m in missing)}")
            failed += 1
            continue
        print(f"→ 出圖：{j['key']}（{j['size']}，參考圖 {len(j['refs'])} 張）")
        for attempt in range(3):
            try:
                # 雲端代理 30 秒沒資料就斷線（502），一律串流，讓 partial image 持續有資料
                opts = dict(model=args.model, prompt=j["prompt"], size=j["size"], quality=args.quality,
                            stream=True, partial_images=3)
                if j["refs"]:
                    files = [open(r, "rb") for r in j["refs"]]
                    try:
                        b64 = final_b64(client.images.edit(image=files, **opts))
                    finally:
                        for f in files:
                            f.close()
                else:
                    b64 = final_b64(client.images.generate(**opts))
                data = base64.b64decode(b64)
                os.makedirs(os.path.dirname(j["out"]), exist_ok=True)
                with open(j["out"], "wb") as f:
                    f.write(data)
                manifest[rel(j["out"])] = {
                    "model": args.model, "quality": args.quality, "size": j["size"],
                    "refs": [rel(r) for r in j["refs"]],
                    "promptSha1": hashlib.sha1(j["prompt"].encode("utf-8")).hexdigest(),
                    "generatedAt": datetime.datetime.now(datetime.timezone.utc).isoformat(timespec="seconds"),
                }
                save_manifest(manifest)
                print(f"  ✓ {rel(j['out'])}")
                done += 1
                break
            except Exception as e:  # 單張失敗不阻斷整批，最後彙總
                print(f"  ! 第 {attempt + 1} 次失敗：{e}")
                if attempt == 2:
                    failed += 1
                else:
                    time.sleep(5 * (attempt + 1))
    print(f"\n完成 {done}、跳過 {skipped}、失敗 {failed}")


def write_jobs_md(jobs):
    out = os.path.join(HERE, "IMAGE_JOBS.md")
    lines = ["# EP01 出圖工作清單（手動貼 ChatGPT 的備援用）", "",
             "每一張：把「提示詞」整段貼進 ChatGPT，附上列出的參考圖，出圖後存成「輸出」的檔名。",
             "設定圖先出完，才出分鏡圖（分鏡圖要掛設定圖當參考）。", ""]
    for j in jobs:
        lines += [f"## {j['key']}", "", f"- 輸出：`{rel(j['out'])}`（{j['size']}）",
                  f"- 參考圖：{'、'.join('`' + rel(r) + '`' for r in j['refs']) or '無'}", "",
                  "```text", j["prompt"], "```", ""]
    with open(out, "w", encoding="utf-8") as f:
        f.write("\n".join(lines))
    print(f"✓ {rel(out)}（{len(jobs)} 張）")


def main():
    ap = argparse.ArgumentParser(description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    ap.add_argument("phase", choices=["sheets", "frames", "jobs"])
    ap.add_argument("--only", help="只做名稱或 id 相符的項目，逗號分隔（例：齊衡烈,S01）")
    ap.add_argument("--segments", help="frames 用：段號，逗號分隔（例：E01-01,E01-02）")
    ap.add_argument("--model", default=os.environ.get("IMAGE_MODEL", "chatgpt-image-latest"))
    ap.add_argument("--quality", default="high")
    ap.add_argument("--force", action="store_true", help="已存在也重出")
    ap.add_argument("--dry-run", action="store_true")
    args = ap.parse_args()

    if args.phase == "jobs":
        write_jobs_md(build_sheet_jobs() + build_frame_jobs())
        return
    segs = set(args.segments.split(",")) if args.segments else None
    jobs = build_sheet_jobs() if args.phase == "sheets" else build_frame_jobs(segs)
    if args.only:
        want = set(args.only.split(","))
        jobs = [j for j in jobs if j.get("name") in want or j.get("id") in want]
    if args.dry_run:
        for j in jobs:
            print(f"{j['key']} → {rel(j['out'])}  [{j['size']}]  參考圖：{[rel(r) for r in j['refs']]}")
        print(f"共 {len(jobs)} 張（模型 {args.model}，品質 {args.quality}）")
        return
    run_jobs(jobs, args)


if __name__ == "__main__":
    main()
