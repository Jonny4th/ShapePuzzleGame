using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string StageName;
    public int StageId;
    public int StageSize;
    public Vector3[] PanelData;
    public StageController.PieceData[] piece;
}
