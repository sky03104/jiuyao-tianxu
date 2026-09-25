using System;
using System.Collections.Generic;
using JiuyaoTianxu.Core;
using UnityEngine;

namespace JiuyaoTianxu.UI.TouchControls
{
    /// <summary>
    /// Phase 0-E prototype of the 15_UI_UX §1 battle controls: fixed move stick
    /// bottom-left; bottom-right an attack button that doubles as the aim stick
    /// (press = attack, drag = aim — twin-stick), plus 閃避 / 鎖定 / 換武器 / 接任務
    /// buttons. Drawn with IMGUI on purpose: a throwaway prototype for feel
    /// testing, not the ink-wash production HUD (that is Phase 1 UI work).
    ///
    /// Writes only TouchInputState; the network layer never sees this class.
    /// Shows automatically on touch devices, or anywhere with -touchui (then the
    /// mouse acts as one finger, for testing in the Editor/desktop).
    /// Layout numbers are fractions of screen height, all 可調整.
    /// </summary>
    public class VirtualControlsOverlay : MonoBehaviour
    {
        [SerializeField] private bool _forceShow;
        [SerializeField] private float _stickRadius = 0.11f;
        [SerializeField] private float _moveDeadzone = 0.15f;
        [SerializeField] private float _aimDeadzone = 0.35f; // a plain tap on 攻 must not also aim
        [SerializeField] private float _attackRadius = 0.085f;
        [SerializeField] private float _smallButtonRadius = 0.05f;

        private enum Control { None, MoveStick, AttackStick, Dodge, LockOn, SwitchWeapon, QuestAccept }

        private const int MousePointerId = -100;
        private readonly Dictionary<int, Control> _owners = new();
        private Vector2 _moveKnob, _aimKnob; // knob offsets (screen px, y-up) for drawing
        private Texture2D _circle;
        private GUIStyle _labelStyle;
        private bool _show;

        private void Awake()
        {
            _show = !Application.isBatchMode &&
                    (_forceShow || Input.touchSupported || Array.IndexOf(Environment.GetCommandLineArgs(), "-touchui") >= 0);
            TouchInputState.Active = _show;
            if (_show) _circle = MakeCircleTexture(64);
        }

        private void OnDestroy()
        {
            TouchInputState.Reset();
            if (_circle != null) Destroy(_circle);
        }

        // ---------------- Layout (screen px, y-up like Input positions) ----------------

        private float H => Screen.safeArea.height;
        private Rect Safe => Screen.safeArea;
        private Vector2 MoveCenter => new(Safe.xMin + H * 0.2f, Safe.yMin + H * 0.22f);
        private Vector2 AttackCenter => new(Safe.xMax - H * 0.2f, Safe.yMin + H * 0.22f);
        private Vector2 DodgeCenter => AttackCenter + new Vector2(-H * 0.19f, -H * 0.08f);
        private Vector2 LockCenter => AttackCenter + new Vector2(-H * 0.16f, H * 0.12f);
        private Vector2 SwitchCenter => AttackCenter + new Vector2(0f, H * 0.18f);
        private Vector2 QuestCenter => new(Safe.xMax - H * 0.1f, Safe.yMax - H * 0.12f);

        private Control HitTest(Vector2 p)
        {
            bool In(Vector2 c, float r) => (p - c).sqrMagnitude <= (r * H) * (r * H);
            if (In(DodgeCenter, _smallButtonRadius)) return Control.Dodge;
            if (In(LockCenter, _smallButtonRadius)) return Control.LockOn;
            if (In(SwitchCenter, _smallButtonRadius)) return Control.SwitchWeapon;
            if (In(QuestCenter, _smallButtonRadius)) return Control.QuestAccept;
            if (In(AttackCenter, _attackRadius * 1.3f)) return Control.AttackStick;
            if (In(MoveCenter, _stickRadius * 1.6f)) return Control.MoveStick; // generous: thumbs miss
            return Control.None;
        }

        // ---------------- Pointer handling ----------------

        private void Update()
        {
            if (!_show) return;

            if (Input.touchCount > 0)
            {
                for (var i = 0; i < Input.touchCount; i++)
                {
                    var t = Input.GetTouch(i);
                    var ended = t.phase is TouchPhase.Ended or TouchPhase.Canceled;
                    HandlePointer(t.fingerId, t.position, t.phase == TouchPhase.Began, ended);
                }
            }
            else if (!Input.touchSupported)
            {
                // Desktop/Editor testing with -touchui: the mouse is one finger.
                HandlePointer(MousePointerId, Input.mousePosition, Input.GetMouseButtonDown(0), Input.GetMouseButtonUp(0));
            }

            RebuildState();
        }

        private void HandlePointer(int id, Vector2 pos, bool began, bool ended)
        {
            if (began && !_owners.ContainsKey(id))
            {
                var control = HitTest(pos);
                if (control == Control.None) return;
                _owners[id] = control;
                switch (control)
                {
                    case Control.Dodge: TouchInputState.PressDodge(); break;
                    case Control.LockOn: TouchInputState.PressLockOn(); break;
                    case Control.SwitchWeapon: TouchInputState.PressSwitchWeapon(); break;
                    case Control.QuestAccept: TouchInputState.PressQuestAccept(); break;
                }
            }

            if (!_owners.TryGetValue(id, out var owned)) return;

            if (owned == Control.MoveStick) _moveKnob = pos - MoveCenter;
            if (owned == Control.AttackStick) _aimKnob = pos - AttackCenter;

            if (ended)
            {
                _owners.Remove(id);
                if (owned == Control.MoveStick) _moveKnob = Vector2.zero;
                if (owned == Control.AttackStick) _aimKnob = Vector2.zero;
            }
        }

        private void RebuildState()
        {
            var moveOwned = _owners.ContainsValue(Control.MoveStick);
            var attackOwned = _owners.ContainsValue(Control.AttackStick);

            StickMath.Evaluate(_moveKnob.x, _moveKnob.y, _stickRadius * H, moveOwned ? _moveDeadzone : 1f, out var mx, out var my);
            StickMath.Evaluate(_aimKnob.x, _aimKnob.y, _stickRadius * H, attackOwned ? _aimDeadzone : 1f, out var ax, out var ay);

            // Screen up = world +Z: the Phase 0 test cameras look along +Z.
            TouchInputState.Move = new Vector2(mx, my);
            TouchInputState.Aim = new Vector2(ax, ay);
            TouchInputState.AttackHeld = attackOwned;
        }

        // ---------------- Drawing ----------------

        private void OnGUI()
        {
            if (!_show) return;

            DrawCircle(MoveCenter, _stickRadius, new Color(1f, 1f, 1f, 0.18f), null);
            DrawCircle(MoveCenter + ClampKnob(_moveKnob), _stickRadius * 0.45f, new Color(1f, 1f, 1f, 0.45f), null);

            var attackHeld = _owners.ContainsValue(Control.AttackStick);
            DrawCircle(AttackCenter, _attackRadius, new Color(0.85f, 0.25f, 0.2f, attackHeld ? 0.7f : 0.45f), "攻");
            if (_aimKnob.sqrMagnitude > 1f)
                DrawCircle(AttackCenter + ClampKnob(_aimKnob), _attackRadius * 0.35f, new Color(1f, 0.9f, 0.6f, 0.6f), null);

            DrawCircle(DodgeCenter, _smallButtonRadius, new Color(1f, 1f, 1f, 0.35f), "閃");
            DrawCircle(LockCenter, _smallButtonRadius, new Color(1f, 1f, 1f, 0.35f), "鎖");
            DrawCircle(SwitchCenter, _smallButtonRadius, new Color(1f, 1f, 1f, 0.35f), "換");
            DrawCircle(QuestCenter, _smallButtonRadius, new Color(0.9f, 0.8f, 0.4f, 0.4f), "任務");
        }

        private Vector2 ClampKnob(Vector2 knob) => Vector2.ClampMagnitude(knob, _stickRadius * H);

        private void DrawCircle(Vector2 centerYUp, float radiusFraction, Color color, string label)
        {
            var r = radiusFraction * H;
            var rect = new Rect(centerYUp.x - r, Screen.height - centerYUp.y - r, r * 2f, r * 2f);
            var old = GUI.color;
            GUI.color = color;
            GUI.DrawTexture(rect, _circle);
            GUI.color = old;
            if (label == null) return;

            _labelStyle ??= new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white },
            };
            _labelStyle.fontSize = Mathf.RoundToInt(r * 0.7f);
            GUI.Label(rect, label, _labelStyle);
        }

        private static Texture2D MakeCircleTexture(int size)
        {
            var tex = new Texture2D(size, size, TextureFormat.RGBA32, false) { wrapMode = TextureWrapMode.Clamp };
            var c = (size - 1) / 2f;
            for (var y = 0; y < size; y++)
            for (var x = 0; x < size; x++)
            {
                var d = Mathf.Sqrt((x - c) * (x - c) + (y - c) * (y - c)) / c;
                tex.SetPixel(x, y, new Color(1f, 1f, 1f, Mathf.Clamp01((1f - d) * c))); // 1px soft edge
            }
            tex.Apply();
            return tex;
        }
    }
}
