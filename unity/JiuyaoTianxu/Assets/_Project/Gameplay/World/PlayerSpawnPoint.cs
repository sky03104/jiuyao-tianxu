using UnityEngine;

namespace JiuyaoTianxu.Gameplay.World
{
    /// <summary>
    /// Scene marker read by the server when a player joins. The server picks the
    /// point (ordered by Index, round-robin) — clients never choose where their
    /// networked player appears (HANDOFF-008 §4).
    /// </summary>
    public class PlayerSpawnPoint : MonoBehaviour
    {
        public int Index;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, 0.4f);
            Gizmos.DrawLine(transform.position, transform.position + transform.forward);
        }
    }
}
