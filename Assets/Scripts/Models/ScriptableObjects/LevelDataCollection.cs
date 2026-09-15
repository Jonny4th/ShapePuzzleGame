using UnityEngine;

namespace Scripts.Models
{
    [CreateAssetMenu(fileName = "NewLevelDataCollection", menuName = "Scriptable Objects/LevelDataCollection")]
    public class LevelDataCollection : ScriptableObject
    {
        [SerializeField] private LevelBlueprint[] m_StageBlueprints;
        public LevelBlueprint[] StageBlueprints => m_StageBlueprints;
    }
}