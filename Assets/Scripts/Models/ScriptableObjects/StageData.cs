using System;
using UnityEngine;

namespace Scripts.Models
{
    [Serializable]
    public struct StageData
    {
        public string StageId;
        public string StageName;
        public Vector3Int StageSize;
        public PanelIdentifier[] PanelData;
        public PieceData[] piece;

        [TextArea(3, 10)]
        public string DisplayDiscription;
        [TextArea(3, 10)]
        public string DeveloperDiscription;
    }
}
