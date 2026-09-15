using System;
using UnityEngine;

namespace Scripts.Models
{
    [CreateAssetMenu(fileName = "NewStageDataSO", menuName = "Stage Data SO")]

    [Serializable]
    public class LevelBlueprint : ScriptableObject
    {
        public ShapeDataList shapeDataCollection;
        public LevelData Data;
    }
}