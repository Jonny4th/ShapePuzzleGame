using UnityEngine;

namespace Scripts.Models
{
    [CreateAssetMenu(fileName = "NewStageDataCollection", menuName = "Scriptable Objects/StageDataCollection")]
    public class StageDataCollection : ScriptableObject
    {
        [SerializeField] private StageBlueprint[] m_StageBlueprints;
        public StageBlueprint[] StageBlueprints => m_StageBlueprints;
    }
}