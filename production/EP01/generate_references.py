"""
EP01《第七室報到》Reference 生成腳本
規格來源：docs/30_EP01_QINGLAN_TRIAL_ANIMATION_PRODUCTION_V1.0.md 第三、四、五節

流程（照文件第二十五節順序）：場景 -> 人物 -> 關鍵幀
固定 seed，確保同一角色/場景之後重複使用同一張 Reference，不重新隨機生成。
"""
import argparse
import os
import torch
from diffusers import StableDiffusionXLPipeline

OUT_ROOT = os.path.join(os.path.dirname(__file__), "references")

GLOBAL_STYLE = (
    "premium 3D Eastern fantasy MMORPG cinematic, original Chinese xuanhuan fantasy world, "
    "ancient cultivation academy, realistic stylized 3D characters, high-end mobile MMORPG "
    "cinematic quality, physically based rendering, detailed fabric and armor materials, "
    "natural human proportions, cinematic depth of field, volumetric morning mist, subtle "
    "spiritual energy particles, elegant Chinese architecture, grounded fantasy, mature visual "
    "tone, restrained color palette, realistic lighting, cinematic composition, vertical 9:16"
)

GLOBAL_NEGATIVE = (
    "low quality, blurry face, deformed face, deformed hands, extra fingers, extra limbs, "
    "duplicate character, duplicate weapon, floating weapon, inconsistent costume, "
    "inconsistent hairstyle, different character identity, modern clothing, modern building, "
    "western medieval castle, sci-fi armor, guns, neon city, chibi, cartoon, exaggerated anime "
    "eyes, childish proportions, giant head, watermark, logo, text, subtitles, UI, game HUD"
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
        "quiet dormitory corridor inside an ancient Chinese cultivation academy, simple dark "
        "wooden door, small blank carved wooden plaque area above the door with no text, warm "
        "morning light, subtle spiritual lantern, clean stone floor, cinematic depth of field"
    ), 100003),
    "player": ("characters", "PLAYER_REF_01", (
        "young adult human cultivator, ordinary newcomer to a cultivation academy, neutral "
        "appearance, practical dark teal and charcoal cultivation clothing, simple academy "
        "travel gear, youthful but mature face, average athletic build, calm observant eyes, "
        "no noble insignia, no royal symbols, no legendary bloodline markings, no glowing eyes, "
        "no excessive ornaments, front-facing character reference sheet pose"
    ), 100004),
    "qi_henglie": ("characters", "QI_HENGLIE_REF_01", (
        "Qi Heng-Lie, young male saber cultivator, strong broad athletic build, energetic "
        "expression, short dark reddish-brown hair, red-gold arm-ring markings, heavy saber, "
        "warm confident smile, practical martial cultivation clothing, powerful stance, "
        "front-facing character reference sheet pose"
    ), 100005),
    "yu_cenye": ("characters", "YU_CENYE_REF_01", (
        "Yu Cen-Ye, young male sword cultivator, short black-blue hair, luminous meridian mark "
        "on forehead, right hand wrapped with cloth, lean athletic build, sharp calm eyes, "
        "restrained expression, dark blue-black cultivation uniform, elegant straight sword at "
        "waist, precise posture, front-facing character reference sheet pose"
    ), 100006),
    "li_ruofeng": ("characters", "LI_RUOFENG_REF_01", (
        "Li Ruo-Feng, young male archer, amber eyes, quiet observant expression, short dark "
        "hair, three-section short bow carried on back, subtle old scar on left shoulder, slim "
        "athletic body, practical dark earth-tone cultivation clothing, calm posture, "
        "front-facing character reference sheet pose"
    ), 100007),
    "xiao_yaolin": ("characters", "XIAO_YAOLIN_REF_01", (
        "Xiao Yao-Lin, mature male heavy-blade instructor, powerful broad build, heavy shoulder "
        "guard, gold meridian mark on forehead, dark instructor robe and armor, imposing but "
        "controlled presence, stern experienced eyes, large heavy blade carried securely, "
        "veteran warrior, calm authority, front-facing character reference sheet pose"
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
        guidance_scale=6.5,
        generator=generator,
    ).images[0]
    out_path = os.path.join(out_dir, f"{name}.png")
    image.save(out_path)
    print(f"[OK] {key} -> {out_path} (seed={seed})")
    return out_path


if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument("--model", default="stabilityai/stable-diffusion-xl-base-1.0")
    parser.add_argument("--only", nargs="*", default=None, help="只生成指定 key，例如 academy corridor")
    args = parser.parse_args()

    pipe = build_pipeline(args.model)
    keys = args.only if args.only else list(REFERENCES.keys())
    for k in keys:
        generate_one(pipe, k)
