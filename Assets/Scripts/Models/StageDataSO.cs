using System;
using UnityEngine;

namespace Scripts.Models
{
    [CreateAssetMenu(fileName = "NewStageDataSO", menuName = "Stage Data SO")]

    [Serializable]
    public class StageDataSO : ScriptableObject
    {
        public StageData Data;
        public ShapeDataList shapeDataCollection;
    }
}