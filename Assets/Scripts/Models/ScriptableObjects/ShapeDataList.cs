using UnityEngine;

namespace Scripts.Models
{
    [CreateAssetMenu]
    public class ShapeDataList : ScriptableObject
    {
        [SerializeField] public ShapeData[] shapeDataList;
    }
}
