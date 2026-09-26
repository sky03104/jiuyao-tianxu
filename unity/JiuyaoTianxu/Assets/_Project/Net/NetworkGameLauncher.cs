using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fusion;
using Fusion.Photon.Realtime;
using Fusion.Sockets;
using JiuyaoTianxu.Combat.Framework;
using JiuyaoTianxu.Config;
using JiuyaoTianxu.Core;
using JiuyaoTianxu.Gameplay.World;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JiuyaoTianxu.Net
{
    /// <summary>
    /// Phase 0-A minimal network bootstrap. Deliberately hand-rolled instead of using
    /// Fusion's shipped FusionBootstrap sample, so the project's own Combat/Core code
    /// stays decoupled from Photon's demo scaffolding (HANDOFF-005 §4.2).
    ///
    /// Game mode is chosen by command line: -netmode server | client | host.
    /// Defaults to Host when run from the Editor (Play button) for one-click local
    /// testing; a real dedicated-server build should always be launched with
    /// -netmode server explicitly (01_ARCHITECTURE_DECISIONS locks Dedicated Server
    /// as the production topology — Host here is a local Phase 0-A testing
    /// convenience, not the shipping architecture).
    /// </summary>
    public class NetworkGameLauncher : MonoBehaviour, INetworkRunnerCallbacks
    {
        [SerializeField] private NetworkRunner _runnerPrefab;
        [SerializeField] private NetworkObject _playerPrefab;
        [SerializeField] private string _sessionName = "Phase0ATest";
        /// <summary>Photon region every peer joins (可調整; -region &lt;code&gt; overrides).
        /// Empty = each peer pings for its own best region: on 2026-09-26 a WebGL client
        /// and the PC server picked different regions and the client got GameNotFound.
        /// "hk" is what the Taiwan dev PC measured as its best region that day.</summary>
        [SerializeField] private string _photonRegion = "hk";

        private NetworkRunner _runner;
        private readonly Dictionary<PlayerRef, NetworkObject> _spawnedPlayers = new();
        private readonly Dictionary<PlayerRef, int> _spawnSlots = new();

        private async void Start()
        {
            var mode = ResolveGameModeFromArgs();
            Debug.Log($"[NetworkGameLauncher] Starting as {mode}, session '{_sessionName}'.");

            // Server decides every combat/seal/quest number, so only it reads the
            // table overrides (roadmap Phase 0: switch test data without rebuilding).
            if (mode is GameMode.Server or GameMode.Host)
            {
                ConfigOverrideLoader.ApplyServerOverrides();
            }

            _runner = Instantiate(_runnerPrefab);
            _runner.name = $"NetworkRunner-{mode}";
            _runner.ProvideInput = mode is GameMode.Client or GameMode.Host or GameMode.AutoHostOrClient;
            _runner.AddCallbacks(this);

            var sceneManager = _runner.GetComponent<INetworkSceneManager>();
            if (sceneManager == null)
            {
                sceneManager = _runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
            }

            var objectProvider = _runner.GetComponent<INetworkObjectProvider>();
            if (objectProvider == null)
            {
                objectProvider = _runner.gameObject.AddComponent<NetworkObjectProviderDefault>();
            }

            var activeScene = SceneManager.GetActiveScene();
            var sceneInfo = new NetworkSceneInfo();
            var sceneRef = SceneRef.FromIndex(activeScene.buildIndex >= 0 ? activeScene.buildIndex : 0);
            sceneInfo.AddSceneRef(sceneRef, LoadSceneMode.Single);

            var appSettings = PhotonAppSettings.Global.AppSettings.GetCopy();
            appSettings.FixedRegion = ResolveRegionFromArgs(_photonRegion);

            var result = await _runner.StartGame(new StartGameArgs
            {
                GameMode = mode,
                SessionName = _sessionName,
                CustomPhotonAppSettings = appSettings,
                Scene = sceneInfo,
                SceneManager = sceneManager,
                ObjectProvider = objectProvider,
            });

            if (!result.Ok)
            {
                Debug.LogError($"[NetworkGameLauncher] StartGame failed: {result.ShutdownReason}");
            }
            else
            {
                Debug.Log($"[NetworkGameLauncher] StartGame succeeded as {mode} (region '{_runner.SessionInfo.Region}').");
                BeginMonsterSpawning(_runner);
            }
        }

        /// <summary>Phase 0-D: server starts the scene's MonsterSpawner, if the scene
        /// has one (Phase0A_NetworkTest doesn't, and is unaffected). Idempotent —
        /// called after StartGame and again from OnSceneLoadDone, whichever finds
        /// the live spawner instance first.</summary>
        private static void BeginMonsterSpawning(NetworkRunner runner)
        {
            if (runner == null || !runner.IsServer) return;
            var spawner = FindAnyObjectByType<MonsterSpawner>();
            if (spawner != null) spawner.Begin(runner);
        }

        private static string ResolveRegionFromArgs(string fallback)
        {
            var args = Environment.GetCommandLineArgs();
            for (var i = 0; i < args.Length - 1; i++)
            {
                if (args[i] == "-region") return args[i + 1].ToLowerInvariant();
            }
            return fallback;
        }

        private static GameMode ResolveGameModeFromArgs()
        {
            var args = Environment.GetCommandLineArgs();
            for (var i = 0; i < args.Length; i++)
            {
                if (args[i] != "-netmode" || i + 1 >= args.Length) continue;

                return args[i + 1].ToLowerInvariant() switch
                {
                    "server" => GameMode.Server,
                    "client" => GameMode.Client,
                    "host" => GameMode.Host,
                    _ => GameMode.Host,
                };
            }

#if UNITY_EDITOR
            return GameMode.Host;
#else
            return GameMode.Client;
#endif
        }

        // ---- INetworkRunnerCallbacks ----

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            Debug.Log($"[NetworkGameLauncher] Player joined: {player}. HasStateAuthority(local)={runner.LocalPlayer == player}");

            if (!runner.IsServer) return; // Only the server spawns players (0-A-07 Server Authority).
            if (_playerPrefab == null)
            {
                Debug.LogError("[NetworkGameLauncher] No player prefab assigned; cannot spawn.");
                return;
            }

            // Face spawned players toward each other so directional melee hit
            // shapes (offset along transform.forward) actually reach a target —
            // otherwise two players spawned side-by-side both facing +Z (the old
            // Quaternion.identity default) never overlap on short-range weapons.
            // Tech review D13: lowest slot nobody currently holds. Using the player
            // count put a rejoining player on top of someone still in the game.
            var index = 0;
            while (_spawnSlots.ContainsValue(index)) index++;
            _spawnSlots[player] = index;
            var spawnPosition = new Vector3(index * 1.5f, 1f, 0f);
            var facing = index % 2 == 0 ? Quaternion.LookRotation(Vector3.right) : Quaternion.LookRotation(Vector3.left);

            // Phase 0-D: scenes with PlayerSpawnPoint markers use them (server picks,
            // round-robin by Index); scenes without (Phase0A_NetworkTest) keep the
            // legacy face-to-face layout above unchanged.
            var spawnPoints = FindObjectsByType<PlayerSpawnPoint>(FindObjectsSortMode.None)
                .OrderBy(p => p.Index).ToArray();
            if (spawnPoints.Length > 0)
            {
                var point = spawnPoints[index % spawnPoints.Length].transform;
                spawnPosition = point.position;
                facing = point.rotation;
            }

            var playerObject = runner.Spawn(_playerPrefab, spawnPosition, facing, player);
            _spawnedPlayers[player] = playerObject;

            // Phase 0-C: no backpack UI exists yet, so the three prototype Spirit
            // Seals are simply equipped at spawn (HANDOFF-007 §2/§12).
            playerObject.GetComponent<SpiritSealSystem>()?.EquipTestLoadout(
                SpiritSealIds.Blaze, SpiritSealIds.Guard, SpiritSealIds.Shadow);
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            Debug.Log($"[NetworkGameLauncher] Player left: {player}");

            if (_spawnedPlayers.TryGetValue(player, out var playerObject))
            {
                runner.Despawn(playerObject);
                _spawnedPlayers.Remove(player);
            }
            _spawnSlots.Remove(player);
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            input.Set(CommandLineFlags.AutoTest ? AutoTestInputProvider.Poll(runner.Tick.Raw) : LocalInputProvider.Poll());
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            ClientCommands.ClearRunner(runner);
            Debug.Log($"[NetworkGameLauncher] Shutdown: {shutdownReason}");
        }
        public void OnConnectedToServer(NetworkRunner runner) =>
            Debug.Log("[NetworkGameLauncher] Connected to server.");
        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) =>
            Debug.Log($"[NetworkGameLauncher] Disconnected from server: {reason}");
        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) =>
            Debug.LogWarning($"[NetworkGameLauncher] Connect failed: {reason}");
        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
        public void OnSceneLoadDone(NetworkRunner runner)
        {
            Debug.Log("[NetworkGameLauncher] Scene load done.");
            BeginMonsterSpawning(runner);
        }
        public void OnSceneLoadStart(NetworkRunner runner) =>
            Debug.Log("[NetworkGameLauncher] Scene load start.");
        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ReadOnlySpan<byte> data) =>
            ClientCommands.Dispatch(runner, player, key, data);
        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    }
}
