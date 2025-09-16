using UnityEngine;

namespace Scripts.Models
{
    [CreateAssetMenu(fileName = "NewStageDataCollection", menuName = "Scriptable Objects/StageDataCollection")]
    public class StageDataCollection : ScriptableObject
    {
        [SerializeField] private StageBlueprint[] m_StageDataScriptables;
        public StageBlueprint[] StageDataScriptables => m_StageDataScriptables;
    }
}