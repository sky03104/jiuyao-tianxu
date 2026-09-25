using UnityEngine;

namespace JiuyaoTianxu.Gameplay.World
{
    /// <summary>Scene marker for MonsterSpawner. Pure placement data.</summary>
    public class MonsterSpawnPoint : MonoBehaviour
    {
        public int Index;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, Vector3.one * 0.6f);
        }
    }
}
