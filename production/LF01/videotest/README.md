# 參考圖直接生影片測試（2026-10-07）

咖哩問：別人都是角色圖＋場景圖＋劇本直接生影片，我們為什麼要一格一格出分鏡圖？
可靈 Omni 帳號點數 0（無訂閱、無每日免費點數）、Gemini 為免費版（無 Veo/Flow），所以先用 **Kaggle 免費 GPU 跑開源 Wan2.1 VACE 1.3B** 測試。

- 鏡頭：第 2 場 A05 f2（齊衡烈從裡面拉開門、上下打量机遙、咧嘴拍臂環），約 5 秒、832×480、16fps
- (a) `wan_a_chars`：只給齊衡烈、机遙兩張角色參考
- (b) `wan_b_chars_room`：再加第七室場景參考
- 參考圖：`ref_*.jpg`（從 EP01 已定稿設定圖裁出）；Notebook：`wan_vace_test_LF01.ipynb`；Kaggle kernel `sky03104/jiuyao-wan-vace-test`

看的重點：角色像不像設定圖、動作自不自然、畫風是不是 3D 國漫、跑一段要多久。
