using PuzzleData;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Stage Data")]

[System.Serializable]
public class StageData : ScriptableObject
{
    public string StageName;
    public int StageId;
    public int StageSize;
    public Vector3[] PanelData;
    public PuzzleCreator.PieceData[] piece;
}
