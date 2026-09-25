using Fusion;
using JiuyaoTianxu.Combat;
using JiuyaoTianxu.Core;
using JiuyaoTianxu.Gameplay.Events;
using UnityEngine;

namespace JiuyaoTianxu.Gameplay.Quests
{
    /// <summary>
    /// The only quest logic in the project (HANDOFF-008 §7–§10). Lives on the
    /// Player prefab next to PlayerQuestLog, mirroring SpiritSealSystem/Loadout.
    ///
    /// Server side:
    ///   - initialises every registry quest to Locked/Available from data;
    ///   - validates accept requests (ClientCommands.QuestAccept from the owning
    ///     client — not an [Rpc], see ClientCommands for why);
    ///   - advances Accepted → InProgress on the next tick;
    ///   - consumes GameplayEvents.EnemyKilled for kills credited to this player;
    ///   - completes, grants the test reward and unlocks dependent quests.
    /// Client side: only SENDS accept requests and logs replicated changes.
    ///
    /// Never touches Health/DamageService, never searches the scene for
    /// monsters, never branches on a specific QuestId — every rule comes from
    /// QuestDefinition data run through QuestStateMachine/QuestObjectiveLogic.
    /// </summary>
    [RequireComponent(typeof(PlayerQuestLog))]
    [RequireComponent(typeof(Health))]
    public class QuestTracker : NetworkBehaviour
    {
        [SerializeField] private QuestRegistry _registry;

        private const float AutoAcceptIntervalSeconds = 1f;

        private PlayerQuestLog _log;
        private Health _health;
        private bool _spawned;
        private bool _subscribed;
        private PlayerRef _commandOwner; // cached: Despawned must unregister the same key
        private float _nextAutoAcceptTime;
        private readonly QuestEntry[] _lastSeen = new QuestEntry[PlayerQuestLog.Capacity];

        public QuestRegistry Registry => _registry;
        public PlayerQuestLog Log => _log;

        /// <summary>This peer's own player's tracker (null on a dedicated server).
        /// Read by the debug quest list in the HUD.</summary>
        public static QuestTracker Local { get; private set; }

        public override void Spawned()
        {
            _log = GetComponent<PlayerQuestLog>();
            _health = GetComponent<Health>();
            _spawned = true;
            if (Object.HasInputAuthority) Local = this;

            if (_registry == null)
            {
                Debug.LogError($"[QuestTracker] {name} has no QuestRegistry assigned.");
                return;
            }

            if (Object.HasStateAuthority)
            {
                InitializeEntries();
                GameplayEvents.EnemyKilled += OnEnemyKilled;
                _commandOwner = Owner;
                ClientCommands.Register(Runner, _commandOwner, ClientCommands.QuestAccept, ServerHandleAccept);
                _subscribed = true;
            }
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            if (_subscribed)
            {
                GameplayEvents.EnemyKilled -= OnEnemyKilled;
                ClientCommands.Unregister(runner, _commandOwner, ClientCommands.QuestAccept);
                _subscribed = false;
            }
            _spawned = false;
            if (Local == this) Local = null;
        }

        // ---------------- Server ----------------

        private void InitializeEntries()
        {
            var count = Mathf.Min(_registry.All?.Length ?? 0, PlayerQuestLog.Capacity);
            if ((_registry.All?.Length ?? 0) > PlayerQuestLog.Capacity)
            {
                Debug.LogWarning($"[QuestTracker] Registry has {_registry.All.Length} quests; only the first " +
                                 $"{PlayerQuestLog.Capacity} fit in PlayerQuestLog.");
            }

            for (var i = 0; i < count; i++)
            {
                var def = _registry.All[i];
                if (def == null) continue;

                var state = QuestStateMachine.InitialState(def.PrerequisiteQuestNumId != 0);
                _log.Entries.Set(i, new QuestEntry { QuestNumId = def.QuestNumId, State = (int)state, Progress = 0 });
                GameLog.Info($"[QuestTracker] {Owner} init {def.QuestId} → {state}.");
            }
        }

        public override void FixedUpdateNetwork()
        {
            if (!Object.HasStateAuthority || _registry == null) return;

            for (var i = 0; i < PlayerQuestLog.Capacity; i++)
            {
                var entry = _log.Entries[i];
                if (entry.QuestState != QuestState.Accepted) continue;

                var def = _registry.GetByNumId(entry.QuestNumId);
                if (def == null) continue;

                SetState(i, entry, def, QuestTrigger.BeginTracking);

                // A quest whose objective is already satisfied completes at once.
                entry = _log.Entries[i];
                if (QuestObjectiveLogic.IsObjectiveMet(entry.Progress, def.RequiredCount))
                {
                    Complete(i, def);
                }
            }
        }

        /// <summary>Registered with ClientCommands for this tracker's owner only, so it
        /// never sees another player's requests.</summary>
        private void ServerHandleAccept(int questNumId)
        {
            var slot = _log.FindSlot(questNumId);
            var def = _registry != null ? _registry.GetByNumId(questNumId) : null;
            if (slot < 0 || def == null)
            {
                Debug.LogWarning($"[QuestTracker] {Owner} accept REJECTED: unknown quest {questNumId}.");
                return;
            }

            var entry = _log.Entries[slot];
            if (!QuestStateMachine.TryTransition(entry.QuestState, QuestTrigger.Accept, out _))
            {
                GameLog.Info($"[QuestTracker] {Owner} accept REJECTED: {def.QuestId} is {entry.QuestState}.");
                return;
            }

            SetState(slot, entry, def, QuestTrigger.Accept);
        }

        private void OnEnemyKilled(EnemyKilledEvent evt)
        {
            if (!_spawned || evt.Runner != Runner) return;
            if (evt.Killer == null || evt.Killer != _health) return; // credited to the killing blow only.

            for (var i = 0; i < PlayerQuestLog.Capacity; i++)
            {
                var entry = _log.Entries[i];
                var def = _registry.GetByNumId(entry.QuestNumId);
                if (def == null) continue;

                if (!QuestObjectiveLogic.TryApplyKill(def.ObjectiveType, entry.QuestState, entry.Progress,
                        def.RequiredCount, def.TargetId, evt.TargetId, out var newProgress))
                {
                    continue;
                }

                entry.Progress = newProgress;
                _log.Entries.Set(i, entry);
                GameLog.Info($"[QuestTracker] {Owner} {def.QuestId} progress {newProgress}/{def.RequiredCount} " +
                          $"(killed {evt.TargetId}).");
                QuestEvents.Raise(new QuestChangedEvent(Runner, Owner, def.QuestNumId, entry.QuestState,
                    entry.QuestState, newProgress, def.RequiredCount));

                if (QuestObjectiveLogic.IsObjectiveMet(newProgress, def.RequiredCount))
                {
                    Complete(i, def);
                }
            }
        }

        private void Complete(int slot, QuestDefinition def)
        {
            if (!SetState(slot, _log.Entries[slot], def, QuestTrigger.ObjectiveReached)) return;

            GrantReward(def);

            // Data-driven unlock: any Locked quest whose prerequisite is this one.
            for (var i = 0; i < PlayerQuestLog.Capacity; i++)
            {
                var other = _log.Entries[i];
                var otherDef = _registry.GetByNumId(other.QuestNumId);
                if (otherDef == null || otherDef.PrerequisiteQuestNumId != def.QuestNumId) continue;

                SetState(i, other, otherDef, QuestTrigger.PrerequisitesMet);
            }
        }

        private void GrantReward(QuestDefinition def)
        {
            switch (def.RewardType)
            {
                case QuestRewardType.DebugCounter:
                    _log.DebugRewardPoints += def.RewardAmount;
                    GameLog.Info($"[QuestTracker] {Owner} reward {def.RewardId} +{def.RewardAmount} " +
                              $"(DebugRewardPoints={_log.DebugRewardPoints}).");
                    break;
            }
        }

        private bool SetState(int slot, QuestEntry entry, QuestDefinition def, QuestTrigger trigger)
        {
            var old = entry.QuestState;
            if (!QuestStateMachine.TryTransition(old, trigger, out var next)) return false;

            entry.State = (int)next;
            _log.Entries.Set(slot, entry);
            GameLog.Info($"[QuestTracker] {Owner} {def.QuestId} {old} → {next} ({entry.Progress}/{def.RequiredCount}).");
            QuestEvents.Raise(new QuestChangedEvent(Runner, Owner, def.QuestNumId, old, next, entry.Progress,
                def.RequiredCount));
            return true;
        }

        private PlayerRef Owner => Object.InputAuthority;

        // ---------------- Client ----------------

        /// <summary>Client request; the server decides (Server Authority, §9).</summary>
        public void RequestAccept(int questNumId)
        {
            if (!_spawned || !Object.HasInputAuthority) return;
            GameLog.Info($"[QuestTracker] {Owner} requesting accept of quest {questNumId}.");
            ClientCommands.Send(Runner, ClientCommands.QuestAccept, questNumId);
        }

        private void Update()
        {
            if (!_spawned || !Object.HasInputAuthority || _registry == null) return;

            var wantsAccept = LocalInputProvider.QuestAcceptPressed();
            if (CommandLineFlags.AutoTest && Time.unscaledTime >= _nextAutoAcceptTime)
            {
                _nextAutoAcceptTime = Time.unscaledTime + AutoAcceptIntervalSeconds;
                wantsAccept = true;
            }
            if (!wantsAccept) return;

            for (var i = 0; i < PlayerQuestLog.Capacity; i++)
            {
                var entry = _log.Entries[i];
                if (entry.QuestNumId == 0 || entry.QuestState != QuestState.Available) continue;

                RequestAccept(entry.QuestNumId);
                return; // one request at a time.
            }
        }

        /// <summary>Client-side proof of replication: logs whenever the replicated
        /// quest state of ANY player changes on this peer.</summary>
        public override void Render()
        {
            if (Runner == null || Runner.IsServer || _registry == null) return;

            for (var i = 0; i < PlayerQuestLog.Capacity; i++)
            {
                var entry = _log.Entries[i];
                var last = _lastSeen[i];
                if (entry.QuestNumId == last.QuestNumId && entry.State == last.State &&
                    entry.Progress == last.Progress)
                {
                    continue;
                }

                _lastSeen[i] = entry;
                var def = _registry.GetByNumId(entry.QuestNumId);
                if (def == null) continue;

                var who = Object.HasInputAuthority ? "local" : "remote";
                GameLog.Info($"[QuestSync] ({who} {Owner}) {def.QuestId} {entry.QuestState} " +
                          $"{entry.Progress}/{def.RequiredCount}");
            }
        }
    }
}
