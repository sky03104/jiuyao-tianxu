using System.Linq;
using UnityEngine;

namespace JiuyaoTianxu.Combat.Framework
{
    /// <summary>
    /// Fusion can only network primitives (int SealId), never a ScriptableObject
    /// reference. Both server and every client resolve the same asset locally
    /// through this shared lookup, so "which seal is in slot 3" only ever needs
    /// to travel over the network as a single int.
    /// </summary>
    [CreateAssetMenu(menuName = "JiuyaoTianxu/Combat/Spirit Seal Registry", fileName = "SpiritSealRegistry")]
    public class SpiritSealRegistry : ScriptableObject
    {
        public SpiritSealDefinition[] All;

        public SpiritSealDefinition GetById(int sealId)
        {
            if (sealId <= 0 || All == null) return null;
            return All.FirstOrDefault(s => s != null && s.SealId == sealId);
        }
    }
}
