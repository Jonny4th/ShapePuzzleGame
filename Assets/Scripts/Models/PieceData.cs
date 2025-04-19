using System;
using UnityEngine;

namespace Scripts.Models
{
    [Serializable]
    public struct PieceData
    {
        public int shapeIndex;
        public Vector3 position;
        public Quaternion rotation;
    }
}