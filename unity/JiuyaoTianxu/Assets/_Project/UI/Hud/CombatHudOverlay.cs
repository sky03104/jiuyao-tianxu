using JiuyaoTianxu.Combat.Targeting;
using JiuyaoTianxu.Gameplay.Quests;
using UnityEngine;

namespace JiuyaoTianxu.UI.Hud
{
    /// <summary>
    /// Phase 0-E debug HUD (IMGUI, prototype only — not the 15_UI_UX production HUD):
    /// HP bars over every Health that has a HealthFeedback, floating damage numbers,
    /// a marker on the local player's locked target, and a plain-text quest list
    /// (explicitly "(debug)": HANDOFF-008 §14 keeps the real quest tracker UI out of
    /// Phase 0). Reads replicated state only.
    /// </summary>
    public class CombatHudOverlay : MonoBehaviour
    {
        [SerializeField] private float _numberLifetime = 0.8f;
        [SerializeField] private float _numberRisePixels = 40f;

        private Texture2D _white;
        private GUIStyle _numberStyle, _textStyle;

        private void Awake()
        {
            if (Application.isBatchMode) { enabled = false; return; }
            _white = Texture2D.whiteTexture;
        }

        private void OnGUI()
        {
            var cam = Camera.main;
            if (cam == null) return;
            EnsureStyles();

            foreach (var fb in HealthFeedback.All)
            {
                if (fb == null || fb.Health == null || fb.Health.MaxHp <= 0) continue;
                if (!ToGui(cam, fb.transform.position + Vector3.up * 1.4f, out var p)) continue;

                const float w = 60f, h = 6f;
                var frac = Mathf.Clamp01(fb.Health.HP / (float)fb.Health.MaxHp);
                Fill(new Rect(p.x - w / 2, p.y, w, h), new Color(0f, 0f, 0f, 0.6f));
                Fill(new Rect(p.x - w / 2, p.y, w * frac, h), new Color(0.85f, 0.2f, 0.2f, 0.9f));
            }

            var now = Time.time;
            HealthFeedback.RecentNumbers.RemoveAll(n => now - n.Time > _numberLifetime);
            foreach (var n in HealthFeedback.RecentNumbers)
            {
                if (!ToGui(cam, n.WorldPosition, out var p)) continue;
                var age = (now - n.Time) / _numberLifetime;
                var old = GUI.color;
                GUI.color = new Color(1f, 0.95f, 0.6f, 1f - age);
                GUI.Label(new Rect(p.x - 40, p.y - 20 - age * _numberRisePixels, 80, 30), n.Amount.ToString(), _numberStyle);
                GUI.color = old;
            }

            var myLock = TargetLock.Local;
            if (myLock != null && myLock.TryGetLockedTarget(out var target) &&
                ToGui(cam, target.transform.position + Vector3.up * 2.0f, out var lp))
            {
                GUI.Label(new Rect(lp.x - 40, lp.y - 30, 80, 30), "▼ 鎖定", _numberStyle);
            }

            DrawQuestList();
        }

        private void DrawQuestList()
        {
            var tracker = QuestTracker.Local;
            if (tracker == null || tracker.Log == null || tracker.Registry == null) return;

            var y = 10f;
            GUI.Label(new Rect(10, y, 400, 22), "任務 (debug)  Q/任務鍵 接取", _textStyle);
            for (var i = 0; i < PlayerQuestLog.Capacity; i++)
            {
                var e = tracker.Log.Entries[i];
                var def = tracker.Registry.GetByNumId(e.QuestNumId);
                if (def == null) continue;
                y += 22f;
                GUI.Label(new Rect(10, y, 400, 22),
                    $"{def.DisplayName}  [{e.QuestState}]  {e.Progress}/{def.RequiredCount}", _textStyle);
            }
        }

        private static bool ToGui(Camera cam, Vector3 world, out Vector2 gui)
        {
            var s = cam.WorldToScreenPoint(world);
            gui = new Vector2(s.x, Screen.height - s.y);
            return s.z > 0f;
        }

        private void Fill(Rect r, Color c)
        {
            var old = GUI.color;
            GUI.color = c;
            GUI.DrawTexture(r, _white);
            GUI.color = old;
        }

        private void EnsureStyles()
        {
            if (_numberStyle != null) return;
            _numberStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter, fontSize = 20, fontStyle = FontStyle.Bold,
                normal = { textColor = Color.white },
            };
            _textStyle = new GUIStyle(GUI.skin.label) { fontSize = 16, normal = { textColor = Color.white } };
        }
    }
}
