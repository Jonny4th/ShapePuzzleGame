using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PuzzleData;

[System.Serializable]
public class LevelData
{
    public string StageName;
    public int StageId;
    public int StageSize;
    public Vector3[] PanelData;
    public PuzzleCreator.PieceData[] piece;
}
