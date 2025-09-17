using System;
using UnityEngine;

namespace Scripts.Models
{
    [CreateAssetMenu(fileName = "NewStageDataSO", menuName = "Stage Data SO")]

    [Serializable]
    public class StageBlueprint : ScriptableObject
    {
        public ShapeDataList shapeDataCollection;
        public StageData Data;
    }
}