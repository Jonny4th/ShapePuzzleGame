using UnityEngine;

namespace Scripts.Models
{
    [CreateAssetMenu(fileName = "StageData", menuName = "Stage Data")]

    [System.Serializable]
    public class StageData : ScriptableObject
    {
        public int StageId;
        public string StageName;

        public int StageSize;

        public Vector3[] PanelData;
        public PieceData[] piece;
    }
}