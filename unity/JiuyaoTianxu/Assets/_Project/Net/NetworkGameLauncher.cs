using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using JiuyaoTianxu.Core;
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

        private NetworkRunner _runner;
        private readonly Dictionary<PlayerRef, NetworkObject> _spawnedPlayers = new();
        private static bool AutoTestMode => Environment.GetCommandLineArgs().Contains("-autotest");

        private async void Start()
        {
            var mode = ResolveGameModeFromArgs();
            Debug.Log($"[NetworkGameLauncher] Starting as {mode}, session '{_sessionName}'.");

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

            var result = await _runner.StartGame(new StartGameArgs
            {
                GameMode = mode,
                SessionName = _sessionName,
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
                Debug.Log($"[NetworkGameLauncher] StartGame succeeded as {mode}.");
            }
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

            var spawnPosition = new Vector3(_spawnedPlayers.Count * 1.5f, 1f, 0f);
            var playerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
            _spawnedPlayers[player] = playerObject;
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            Debug.Log($"[NetworkGameLauncher] Player left: {player}");

            if (_spawnedPlayers.TryGetValue(player, out var playerObject))
            {
                runner.Despawn(playerObject);
                _spawnedPlayers.Remove(player);
            }
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            input.Set(AutoTestMode ? AutoTestInputProvider.Poll() : KeyboardInputProvider.Poll());
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) =>
            Debug.Log($"[NetworkGameLauncher] Shutdown: {shutdownReason}");
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
        public void OnSceneLoadDone(NetworkRunner runner) =>
            Debug.Log("[NetworkGameLauncher] Scene load done.");
        public void OnSceneLoadStart(NetworkRunner runner) =>
            Debug.Log("[NetworkGameLauncher] Scene load start.");
        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ReadOnlySpan<byte> data) { }
        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    }
}
