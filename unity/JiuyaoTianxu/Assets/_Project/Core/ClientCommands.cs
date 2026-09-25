using System;
using System.Buffers.Binary;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

namespace JiuyaoTianxu.Core
{
    /// <summary>
    /// One-shot client → server commands. Used instead of [Rpc]: Fusion 2.1.2's woven
    /// RPC bodies call internal Fusion.Runtime methods, which Mono player builds reject
    /// with MethodAccessException (found in the 2026-09-25 local run, see
    /// docs/00_AI_HANDOFF_BRIDGE.md CLAUDE-REPLY-008).
    ///
    /// Sent with NetworkRunner.SendReliableDataToServer: delivered reliably, once per
    /// call, and on a host it loops back through the same callback (see Dispatch for the
    /// sender fix-up) — one code path for every game mode. The server gets the sender's
    /// PlayerRef from the transport, so a client can only act for itself; the receiving
    /// system still validates every command.
    /// </summary>
    public static class ClientCommands
    {
        /// <summary>Command ids (ReliableKey slot 0). Never renumber once shipped.</summary>
        public const int QuestAccept = 1;

        /// <summary>Server side: (runner, sender, command, argument).</summary>
        public static event Action<NetworkRunner, PlayerRef, int, int> Received;

        private static int _sequence; // keeps every send's key distinct

        public static void Send(NetworkRunner runner, int command, int argument)
        {
            Span<byte> payload = stackalloc byte[sizeof(int)];
            BinaryPrimitives.WriteInt32LittleEndian(payload, argument);
            runner.SendReliableDataToServer(ReliableKey.FromInts(command, ++_sequence, 0, 0), payload);
        }

        /// <summary>Called by NetworkGameLauncher.OnReliableDataReceived.</summary>
        public static void Dispatch(NetworkRunner runner, PlayerRef sender, ReliableKey key, ReadOnlySpan<byte> data)
        {
            if (!runner.IsServer) return;

            key.GetInts(out var command, out _, out _, out _);
            // A host's own sends loop back with sender PlayerRef.None (seen in the
            // 2026-09-26 host run). Remote data always carries the client's index, so
            // None can only be the local peer. On a dedicated server LocalPlayer is
            // also None and nothing matches it.
            if (sender == PlayerRef.None) sender = runner.LocalPlayer;

            if (data.Length != sizeof(int))
            {
                Debug.LogWarning($"[ClientCommands] {sender} sent command {command} with {data.Length} bytes; ignored.");
                return;
            }
            var argument = BinaryPrimitives.ReadInt32LittleEndian(data);
            GameLog.Info($"[ClientCommands] command {command}({argument}) from {sender}.");
            Received?.Invoke(runner, sender, command, argument);
        }
    }
}
