using UnityEngine;

namespace JiuyaoTianxu.Gameplay.World
{
    /// <summary>
    /// Data tag that turns "some object with Health" into "an enemy of type X"
    /// for gameplay purposes. TargetId is plain data set on the prefab, so a
    /// quest targets "Phase0D_TestMonster" by id — not by C# class — and a new
    /// monster type is a new prefab with a new id, not new quest code.
    /// </summary>
    public class EnemyIdentity : MonoBehaviour
    {
        [SerializeField] private string _targetId = "Phase0D_TestMonster";

        public string TargetId => _targetId;
    }
}
