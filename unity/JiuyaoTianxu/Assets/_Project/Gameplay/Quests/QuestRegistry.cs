using UnityEngine;

namespace JiuyaoTianxu.Gameplay.Quests
{
    /// <summary>
    /// Networked QuestNumId → local QuestDefinition lookup, shared by server and
    /// every client. Array order also defines each quest's slot in PlayerQuestLog.
    /// </summary>
    [CreateAssetMenu(menuName = "JiuyaoTianxu/Gameplay/Quest Registry", fileName = "QuestRegistry")]
    public class QuestRegistry : ScriptableObject
    {
        public QuestDefinition[] All;

        public QuestDefinition GetByNumId(int questNumId)
        {
            if (questNumId <= 0 || All == null) return null;
            foreach (var def in All)
            {
                if (def != null && def.QuestNumId == questNumId) return def;
            }
            return null;
        }
    }
}
