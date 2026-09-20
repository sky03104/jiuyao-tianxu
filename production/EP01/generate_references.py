"""
EP01《第七室報到》Reference 生成腳本
規格來源：docs/30_EP01_QINGLAN_TRIAL_ANIMATION_PRODUCTION_V1.0.md 第三、四、五節
美術方向最高優先權來源：docs/MASTER_VISUAL_STYLE_LOCK_V1.0.md（凌駕本檔任何舊prompt）

流程（照文件第二十五節順序）：場景 -> 人物 -> 關鍵幀
固定 seed，確保同一角色/場景之後重複使用同一張 Reference，不重新隨機生成。

風格驗收未過（見MASTER_VISUAL_STYLE_LOCK_V1.0.md第十三節）前，只跑
`--only li_ruofeng_style_test`，不生成其他角色/場景/正式Shot。
"""
import argparse
import os
import torch
from diffusers import StableDiffusionXLPipeline

OUT_ROOT = os.path.join(os.path.dirname(__file__), "references")

# 依 MASTER_VISUAL_STYLE_LOCK_V1.0.md 第一~七節重寫：目標是「高品質3D MMORPG遊戲角色
# ＋東方玄幻電影＋高級動畫電影級角色表現」之間，不是真人照片、不是角色設定集、不是攝影棚。
GLOBAL_STYLE = (
    "premium 3D Eastern xuanhuan MMORPG cinematic game screenshot, high-end mobile MMORPG hero "
    "splash art, stylized realistic 3D game character render (not a photograph, not real human "
    "photography), idealized but natural adult proportions, single unified image, single scene, "
    "original Chinese xuanhuan fantasy world spiritual civilization (not simply ancient China, "
    "not western fantasy), Han-inspired cultivation robes and architecture, fine fabric and "
    "leather and jade and wood detail, cinematic environmental lighting, depth of field, "
    "volumetric light, subtle morning mist, aerial perspective, natural environmental shadows, "
    "character existing inside the world (not a studio backdrop), deep teal, ink black, dark "
    "blue, dark brown, warm gold and small amounts of vermillion red color palette, subtle "
    "spiritual energy glow, vertical 9:16, adult character, fully clothed, modest practical "
    "clothing, safe for work, all-ages appropriate, family friendly"
)

GLOBAL_NEGATIVE = (
    "low quality, blurry face, deformed face, deformed hands, extra fingers, extra limbs, "
    "duplicate character, duplicate weapon, floating weapon, inconsistent costume, "
    "inconsistent hairstyle, different character identity, modern clothing, modern building, "
    "western medieval castle, sci-fi armor, guns, neon city, chibi, cartoon, anime, manga, "
    "exaggerated anime eyes, childish proportions, giant head, 2D flat illustration, "
    "character design sheet, turnaround sheet, model sheet, multiple views, multiple panels, "
    "collage, diptych, grid layout, side by side comparison, inset panel, close-up inset, "
    "Japanese kimono, japanese armor, katana, samurai, shoji screen, tatami mat, torii, "
    "watermark, logo, signature, text, chinese text, chinese calligraphy, hanzi, ink brush "
    "text, vertical text column, text banner, scroll banner, red seal stamp, name chop stamp, "
    "caption, title, subtitle, label, border, frame, picture frame, ornamental border, UI, "
    "game HUD, jpeg artifacts, real photograph, real photography, photojournalism, stock photo, "
    "e-commerce clothing photo, fashion catalog photo, product photography, studio portrait, "
    "plain gray studio background, seamless backdrop, real human skin pores, bodybuilder "
    "muscular physique, fitness model physique, western ranger, viking, medieval fantasy armor, "
    "d&d ranger, hunter cosplay, leather ranger armor, cyberpunk, neon purple blue lighting, "
    "over-saturated colors, glowing energy blade, light saber, lens flare weapon, nsfw, nudity, "
    "sexualized, revealing clothing, underwear, swimsuit, child, loli, shota, underage, "
    "japanese school uniform, sailor uniform, skirt, short shorts, bare skin, cleavage, "
    "suggestive pose, drawing bow, nocking arrow, action pose, dynamic action shot, heroic "
    "pose, protagonist aura, chosen one glow, dramatic hero lighting, dramatic wind effect, "
    "giant flowing cape, oversized weapon, legendary weapon glow"
)

# id -> (子資料夾, 檔名, 專屬 prompt, seed)
REFERENCES = {
    "academy": ("scenes", "TIANXUAN_ACADEMY_REF_01", (
        "enormous ancient Chinese cultivation academy built into a mountain, layered dark jade "
        "and warm stone architecture, elegant curved roofs, long elevated corridors, training "
        "courtyards, distant mountain peaks, morning clouds flowing through the academy, subtle "
        "spiritual energy in the air, young cultivators walking in the distance, cinematic sunrise, "
        "realistic architecture"
    ), 100001),
    "corridor": ("scenes", "EAST_CORRIDOR_REF_01", (
        "long elegant eastern fantasy academy corridor, dark wooden pillars, stone floor, warm "
        "wooden doors, hanging academy lanterns, mountain view visible through open side, young "
        "cultivators passing naturally in background, morning light and soft mist"
    ), 100002),
    "room07": ("scenes", "ROOM_07_REF_01", (
        "quiet dormitory corridor inside an ancient Chinese cultivation academy, Chinese "
        "architecture not Japanese, simple dark solid wooden door with no carving and no text, "
        "small completely blank wooden plaque area above the door, warm morning light, subtle "
        "spiritual lantern, clean stone floor, cinematic depth of field"
    ), 100003),
    "player": ("characters", "PLAYER_REF_01", (
        "one single young adult male human cultivator standing alone, full body, front view, "
        "ordinary newcomer to a Chinese cultivation academy, neutral plain appearance, practical "
        "dark teal and charcoal traditional Chinese cultivation robe (not a skirt, not a modern "
        "uniform), simple academy travel gear, youthful but mature face, average athletic build, "
        "calm observant eyes, no noble insignia, no royal symbols, no legendary bloodline "
        "markings, no glowing eyes, no excessive ornaments, standing neutral pose, video game "
        "character cinematic screenshot"
    ), 100004),
    "qi_henglie": ("characters", "QI_HENGLIE_REF_01", (
        "one single young adult male cultivator standing alone, full body, front view, Qi "
        "Heng-Lie, saber cultivator, strong broad athletic build, energetic expression, short "
        "dark reddish-brown hair, red-gold arm-ring markings, holding one heavy curved saber, "
        "warm confident smile, practical Chinese martial cultivation robe, powerful standing "
        "pose, video game character cinematic screenshot"
    ), 100005),
    "yu_cenye": ("characters", "YU_CENYE_REF_01", (
        "one single young adult male cultivator standing alone, full body, front view, Yu "
        "Cen-Ye, Chinese sword cultivator (not a Japanese samurai, not wearing kimono), short "
        "black-blue hair, luminous meridian mark on forehead, right hand wrapped with cloth, "
        "lean athletic build, sharp calm eyes, restrained expression, dark blue-black Chinese "
        "cultivation robe, holding one straight double-edged Chinese jian sword (not a curved "
        "katana), precise standing posture, video game character cinematic screenshot"
    ), 100006),
    "li_ruofeng": ("characters", "LI_RUOFENG_REF_01", (
        "one single young adult male archer standing alone, full body, front view, Li Ruo-Feng, "
        "amber eyes, quiet observant expression, short dark hair, holding a short recurve hunting "
        "bow, subtle old scar on left shoulder, slim athletic body, practical dark earth-tone "
        "Chinese cultivation robe, calm standing posture, video game character cinematic "
        "screenshot"
    ), 100007),
    # 風格驗收測試專用，非正式Reference，先只跑這個確認方向見docs/MASTER_VISUAL_STYLE_LOCK_V1.0.md第十三節
    "li_ruofeng_style_test": ("characters", "LI_RUOFENG_STYLE_TEST_01", (
        "one single young adult male cultivator-archer, Li Ruo-Feng, slender toned lean build "
        "(not bodybuilder, not bulky), Eastern Chinese young man facial features, refined "
        "idealized game-hero face (not a real photograph face), short dark hair, calm quiet "
        "amber-colored eyes with an observant watchful gaze, subtle faded old scar on left "
        "shoulder, dark earth-tone Chinese xuanhuan cultivation robe with fine fabric and subtle "
        "spiritual talisman patterns (not hunter cosplay, not western ranger leather armor, not "
        "Viking, not D&D ranger), holding a small Eastern-fantasy three-section short recurve bow "
        "(not a large western longbow), standing quietly at the edge of an ancient Chinese "
        "spiritual cultivation academy corridor at dawn, misty mountains and warm lantern light "
        "visible behind him, he is watching something off-frame rather than posing for camera, "
        "full body, front-three-quarter view"
    ), 100107),
    # 第二輪風格測試：咖哩給了逐項極詳細規格（自然站姿、不拉弓、不英雄pose、低調服裝）
    "li_ruofeng_style_test_2": ("characters", "LI_RUOFENG_STYLE_TEST_02", (
        "one single young adult male cultivator, Li Ruo-Feng, slender lean fit natural adult "
        "male build (not a bodybuilder, not a fashion model), Eastern Chinese young man facial "
        "features, refined but natural idealized high-end MMORPG game character face (not a real "
        "photograph, not anime big eyes, not over-beautified), short naturally tousled black "
        "hair (not long, not flowing wuxia hair), calm restrained amber eyes with a quiet "
        "watchful observant gaze, looking off to the side rather than at camera, relaxed natural "
        "standing pose with body weight slightly shifted, one hand resting naturally near his "
        "bow (not drawing it, not an action pose, not a heroic pose), subtle faded old scar on "
        "left shoulder, low-key dark Tianxuan Academy young cultivator robe in ink black, dark "
        "brown and deep teal with small muted gray-blue accents, fine fabric with small amounts "
        "of leather and metal and subtle Eastern craft detailing, deliberately understated (not "
        "royal costume, not legendary armor, not a giant cape, not heavy armor, not western "
        "leather ranger armor, not hunter costume), carrying a small reasonably-sized three-"
        "section short recurve bow of Eastern xuanhuan design that looks well-worn from years of "
        "use (not a giant western longbow, not glowing, not a legendary weapon, no magic "
        "effects), standing naturally in the stone corridor of an ancient mountain cultivation "
        "academy at dawn, dark wood architecture, distant mountains, morning mist, a few other "
        "students faintly visible far in the background, soft natural morning light, full body, "
        "vertical portrait"
    ), 100207),
    "xiao_yaolin": ("characters", "XIAO_YAOLIN_REF_01", (
        "one single mature adult male instructor standing alone, full body, front view, Xiao "
        "Yao-Lin, heavy-blade instructor, powerful broad build, heavy shoulder guard armor, gold "
        "meridian mark visible on forehead, dark Chinese instructor robe and armor, imposing but "
        "controlled presence, stern experienced eyes, holding one large wide heavy blade (broad "
        "dao, not a thin straight sword), veteran warrior, calm authority, standing pose, video "
        "game character cinematic screenshot"
    ), 100008),
}

WIDTH, HEIGHT = 832, 1216  # 9:16 比例，SDXL 原生支援的直式解析度桶


def build_pipeline(model_path: str):
    pipe = StableDiffusionXLPipeline.from_pretrained(
        model_path, dtype=torch.float16, use_safetensors=True
    )
    pipe.enable_model_cpu_offload()  # 6GB VRAM 必須靠 offload，不能整包塞進顯存
    pipe.vae.enable_slicing()
    return pipe


def generate_one(pipe, key: str):
    subdir, name, prompt, seed = REFERENCES[key]
    out_dir = os.path.join(OUT_ROOT, subdir)
    os.makedirs(out_dir, exist_ok=True)
    generator = torch.Generator(device="cpu").manual_seed(seed)
    image = pipe(
        prompt=f"{prompt}, {GLOBAL_STYLE}",
        negative_prompt=GLOBAL_NEGATIVE,
        width=WIDTH,
        height=HEIGHT,
        num_inference_steps=30,
        guidance_scale=5.0,  # 降低guidance，SDXL在高guidance下更容易幻覺出文字/邊框裝飾
        generator=generator,
    ).images[0]
    out_path = os.path.join(out_dir, f"{name}.png")
    image.save(out_path)
    print(f"[OK] {key} -> {out_path} (seed={seed})")
    return out_path


if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    # 只用來源可查證、非匿名合併的模型。DreamShaper XL由Lykon維護（SD社群知名長期
    # 創作者，非匿名帳號），偏藝術化/半寫實風格，介於RealVisXL的純寫實與SDXL Base
    # 的插畫海報化之間，較適合MASTER_VISUAL_STYLE_LOCK要的「遊戲電影級」方向。
    # 不再使用來源不明的匿名Civitai鏡像模型（如John6666系列，已證實混入成人向資料）。
    parser.add_argument("--model", default="Lykon/dreamshaper-xl-1-0")
    parser.add_argument("--only", nargs="*", default=None, help="只生成指定 key，例如 academy corridor")
    args = parser.parse_args()

    pipe = build_pipeline(args.model)
    keys = args.only if args.only else list(REFERENCES.keys())
    for k in keys:
        generate_one(pipe, k)
