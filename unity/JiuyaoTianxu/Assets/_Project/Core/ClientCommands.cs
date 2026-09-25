using System;
using System.Buffers.Binary;
using System.Collections.Generic;
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
    /// Remote clients send with NetworkRunner.SendReliableDataToServer (reliable, once per
    /// call) and the server learns the sender's PlayerRef from the transport. A host's own
    /// commands are delivered locally instead of through Fusion's loopback, whose sender
    /// is PlayerRef.None. Handlers are registered per (runner, owner, command), so a
    /// command only ever reaches the sender's own handler — the guarantee
    /// RpcSources.InputAuthority used to give, enforced here rather than by every receiver.
    /// </summary>
    public static class ClientCommands
    {
        /// <summary>Command ids (ReliableKey slot 0). Never renumber once shipped.</summary>
        public const int QuestAccept = 1;

        private static readonly Dictionary<(NetworkRunner, PlayerRef, int), Action<int>> Handlers = new();
        private static int _sequence; // keeps every send's key distinct

        /// <summary>Server side: <paramref name="command"/> sent by <paramref name="owner"/>
        /// goes to <paramref name="handler"/>, and nobody else's commands do.</summary>
        public static void Register(NetworkRunner runner, PlayerRef owner, int command, Action<int> handler) =>
            Handlers[(runner, owner, command)] = handler;

        public static void Unregister(NetworkRunner runner, PlayerRef owner, int command) =>
            Handlers.Remove((runner, owner, command));

        /// <summary>Called on the peer with input authority.</summary>
        public static void Send(NetworkRunner runner, int command, int argument)
        {
            if (runner.IsServer)
            {
                // Host: its own player's command never leaves the process.
                Deliver(runner, runner.LocalPlayer, command, argument);
                return;
            }

            Span<byte> payload = stackalloc byte[sizeof(int)];
            BinaryPrimitives.WriteInt32LittleEndian(payload, argument);
            runner.SendReliableDataToServer(ReliableKey.FromInts(command, ++_sequence, 0, 0), payload);
        }

        /// <summary>Called by NetworkGameLauncher.OnReliableDataReceived.</summary>
        public static void Dispatch(NetworkRunner runner, PlayerRef sender, ReliableKey key, ReadOnlySpan<byte> data)
        {
            if (!runner.IsServer) return;

            key.GetInts(out var command, out _, out _, out _);
            if (data.Length != sizeof(int))
            {
                Debug.LogWarning($"[ClientCommands] {sender} sent command {command} with {data.Length} bytes; ignored.");
                return;
            }
            Deliver(runner, sender, command, BinaryPrimitives.ReadInt32LittleEndian(data));
        }

        private static void Deliver(NetworkRunner runner, PlayerRef sender, int command, int argument)
        {
            // Also rejects PlayerRef.None: nothing is ever registered for it.
            if (!Handlers.TryGetValue((runner, sender, command), out var handler))
            {
                Debug.LogWarning($"[ClientCommands] no handler for command {command} from {sender}; ignored.");
                return;
            }
            GameLog.Info($"[ClientCommands] command {command}({argument}) from {sender}.");
            handler(argument);
        }
    }
}
