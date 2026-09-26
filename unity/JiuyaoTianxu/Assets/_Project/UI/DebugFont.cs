using UnityEngine;

namespace JiuyaoTianxu.UI
{
    /// <summary>
    /// Font with Chinese glyphs for the IMGUI debug HUD and touch buttons. Desktop and
    /// native mobile builds fall back to an OS font, but WebGL has none, so the default
    /// font showed no Chinese at all there (found 2026-09-26 while preparing the iPhone
    /// touch test). Noto Sans TC, SIL OFL 1.1 (license: Resources/Fonts/OFL.txt).
    /// Placeholder only — the production font is 15_UI_UX / 16_ART_DIRECTION's call.
    /// </summary>
    public static class DebugFont
    {
        private const string ResourcePath = "Fonts/NotoSansTC-Variable";
        private static Font _font;
        private static bool _loaded;

        /// <summary>Null (= default font) if the asset is missing.</summary>
        public static Font Get()
        {
            if (_loaded) return _font;
            _loaded = true;
            _font = Resources.Load<Font>(ResourcePath);
            if (_font == null) Debug.LogWarning($"[DebugFont] Resources/{ResourcePath} not found; using the default font.");
            return _font;
        }
    }
}
