"""EP01 設定圖生成：shuohao 產出的 image.sheet ＋ MASTER_VISUAL_STYLE_LOCK 畫風層 → gpt-image-2。

用法：python3 gen_sheets.py [名稱 ...]   （不給名稱＝角色＋場景＋道具全部）
輸出：
  characters/images/<角色名>-sheet.png   ← cast.json（novel-characters render 會自動撿圖）
  art/images/<場景或道具名>-sheet.png    ← art.json（novel-art render 會自動撿圖）
"""
import base64, json, pathlib, sys, urllib.request

ROOT = pathlib.Path(__file__).resolve().parent.parent
CAST = ROOT / "characters" / "EP01-cast.json"
ART = ROOT / "art" / "EP01-art.json"
MODEL = "gpt-image-2"
SIZE = "2048x1152"  # 16:9，對應 image.sheet 版面

# 畫風層（STYLE LOCK 第一～五、七節）。設定圖資產依 DECISIONS.md 保留白底三視圖版面。
STYLE = (
    "ART STYLE (applies to every figure on the sheet): premium 3D Eastern xuanhuan MMORPG "
    "cinematic character art — a high-end stylized 3D game character render in the look of a "
    "top-tier Chinese fantasy MMORPG / donghua feature film. Not a photograph, not a real person, "
    "not a costume-drama still, not a studio fashion photo, not anime, not chibi. "
    "Natural adult proportions, slender and fit, slightly idealised but restrained; no muscle "
    "showcase. Refined East Asian young adult face with clear character identity and a gaze that "
    "carries personality; stylized skin shading without pore-level realism, no big anime eyes. "
    "Fine woven cloth, fine leather, a little antique bronze, wood and jade, ancient Eastern "
    "craftsmanship with subtle mystical cultivation motifs — the costume must read as belonging to "
    "the cultivation world of 'Nine Luminaries', not historical China and never European fantasy. "
    "Palette leans deep teal, ink black, dark blue, dark brown, warm gold accents, a little "
    "vermilion. No Western ranger, no leather-armour rogue, no Viking or medieval fantasy.\n\n"
)
# 場景／道具的畫風層（STYLE LOCK 第一、五、六、七節；EP01 天空不得出現天墟裂隙，見 DECISIONS.md）
ENV_STYLE = (
    "ART STYLE: premium 3D Eastern xuanhuan MMORPG cinematic environment art — a high-end "
    "stylized 3D game render in the look of a top-tier Chinese fantasy MMORPG / donghua feature "
    "film, not a photograph, not a historical-drama set, not European fantasy. The world is the "
    "cultivation realm of 'Nine Luminaries': ancient Eastern craftsmanship with subtle mystical "
    "cultivation motifs. Cinematic ambient light, depth of field, volumetric light, aerial "
    "perspective, faint morning mist, natural shadows. Palette leans deep teal, ink black, dark "
    "blue, dark brown, warm gold accents, a little vermilion; no neon purple-blue, no cyberpunk, "
    "no excessive golden glow.\n\n"
)
SAFETY = (
    "\n\nAll characters are adults, fully clothed, safe for work. Absolutely no text, letters, "
    "captions, labels, numbers, logos or watermark anywhere on the canvas."
)

# 第一輪驗圖後的逐角色補強（2026-09-24）：模型沒守住的規格再寫死一次
FIX = {
    "厲若楓": "CORRECTION: exactly ONE leather arm guard, on the LEFT forearm only; the right forearm "
              "is bare cloth sleeve with no guard, in every view.",
    "齊衡烈": "CORRECTION: exactly ONE long leather gauntlet, on the LEFT forearm only; the right "
              "forearm has no gauntlet, only the red-gold arm ring on the right upper arm. Arms are "
              "strong but lean and relaxed, like a game hero, not a bodybuilder — no bulging muscle "
              "definition. The weapon is a single heavy single-edged broad saber (dao) with a thick "
              "straight back and one cutting edge, not a double-edged sword. Face is stylized game "
              "character, not a photograph.",
    "蕭曜霖": "CORRECTION: he carries exactly ONE weapon in total — a single huge door-plank heavy "
              "sword strapped diagonally across his back, its one long grip rising above his right "
              "shoulder. No second sword, no second grip, no weapon at the hip, in every view. The "
              "pale-gold mark sits low on the brow bone between the eyebrows, not high on the forehead.",
    "第七室": "CORRECTION: this is a FOUR-person room — exactly FOUR low sleeping platforms, TWO "
              "placed end to end along the left wall and TWO along the right wall, each with its own "
              "wooden chest at its foot; all four must be visible in the master view.",
}


def build_character(c):
    img = c["image"]
    fix = ("\n\n" + FIX[c["name"]]) if c["name"] in FIX else ""
    return STYLE + img["sheet"] + fix + "\n\nAVOID: " + img["negativePrompt"] + \
        ", photorealistic photo, real human photograph, studio fashion catalogue, Western RPG, " \
        "hunter cosplay, anime big eyes, chibi" + SAFETY


def build_art(a):
    img = a["image"]
    # 場景附上主光照狀態；道具走白底，不附光照
    light = ("\n\nLIGHTING: " + a["lighting"][0]["prompt"]) if a.get("lighting") else ""
    fix = ("\n\n" + FIX[a["name"]]) if a["name"] in FIX else ""
    return ENV_STYLE + img["sheet"] + light + fix + "\n\nAVOID: " + img["negativePrompt"] + \
        ", photorealistic photo, Western fantasy, European castle" + SAFETY


def gen(name, prompt, out_dir):
    body = json.dumps({"model": MODEL, "prompt": prompt, "size": SIZE,
                       "quality": "high", "n": 1,
                       # 代理 30 秒無資料會斷線，串流讓連線持續有資料
                       "stream": True, "partial_images": 3}).encode()
    req = urllib.request.Request("https://api.openai.com/v1/images/generations", data=body,
                                 headers={"Content-Type": "application/json"})
    b64 = None
    with urllib.request.urlopen(req, timeout=600) as r:
        for line in r:
            line = line.decode().strip()
            if line.startswith("data:") and '"image_generation.completed"' in line:
                b64 = json.loads(line[5:])["b64_json"]
    if not b64:
        raise RuntimeError("stream ended without completed image")
    out = out_dir / f"{name}-sheet.png"
    out.write_bytes(base64.b64decode(b64))
    print("OK", out, flush=True)


def jobs():
    for c in json.loads(CAST.read_text())["characters"]:
        yield c["name"], build_character(c), ROOT / "characters" / "images"
    art = json.loads(ART.read_text())
    for a in art["scenes"] + art["props"]:
        yield a["name"], build_art(a), ROOT / "art" / "images"


if __name__ == "__main__":
    want = sys.argv[1:]
    for name, prompt, out_dir in jobs():
        if want and name not in want:
            continue
        out_dir.mkdir(parents=True, exist_ok=True)
        try:
            gen(name, prompt, out_dir)
        except urllib.error.HTTPError as e:
            print("FAIL", name, e.code, e.read().decode()[:500], flush=True)
