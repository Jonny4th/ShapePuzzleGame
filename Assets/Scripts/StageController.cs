using System;
using UnityEngine;
using static PuzzleData.PuzzleCreator;

public class StageController : MonoBehaviour
{
    [SerializeField] StageData levelData;
    [SerializeField] int stageSize;
    [SerializeField] string stageName;
    [SerializeField] Mesh BlockTheme;
    public PieceData[] pieceData;

    ShapeData[] shapeDataCollection;

    private void OnEnable()
    {
        shapeDataCollection = Resources.LoadAll<ShapeData>("ShapeData");
        LoadStageData();
    }

    public void SaveStageData()
    {
        StorePuzzle();
        StoreShape();
    }

    private void StorePuzzle()
    {
        PanelStateController[] activePanels = Array.FindAll(FindObjectsOfType<PanelStateController>(), x => (x.currentState & PanelStateController.State.Target) != 0);
        levelData.PanelData = new Vector3[activePanels.Length];
        for (int i = 0; i < activePanels.Length; i++)
        {
            levelData.PanelData[i] = Vector3Int.RoundToInt(activePanels[i].transform.position);
        }
        levelData.StageName = stageName;
        levelData.StageSize = stageSize;
    }

    private void StoreShape()
    {
        ShapeModel[] shape = FindObjectsOfType<ShapeModel>();
        pieceData = new PieceData[shape.Length];
        for (int i = 0; i < shape.Length; i++)
        {
            pieceData[i] = new PieceData
            {
                shape = shape[i].plainShape,
                position = shape[i].transform.position,
                rotation = shape[i].transform.rotation,
            };
        }
        levelData.piece = pieceData;
    }

    public void LoadStageData()
    {
        LoadPuzzle();
        LoadShapePieces();
    }

    private void LoadPuzzle()
    {
        Vector3[] activePanelCoordinates = levelData.PanelData;
        PanelStateController[] panels = FindObjectsOfType<PanelStateController>();
        foreach (PanelStateController panel in panels)
        {
            if (Array.Exists(activePanelCoordinates, x => x == panel.transform.position))
            {
                panel.SetAsTarget(true);
            }
            else
            {
                panel.currentState = PanelStateController.State.None;
            }
        }
    }

    private void LoadShapePieces()
    {
        foreach (var piece in FindObjectsOfType<ShapeModel>())
        {
            DestroyImmediate(piece.gameObject);
        }

        pieceData = levelData.piece;
        foreach (PieceData piece in pieceData)
        {
            GameObject go = GetShape(piece.shapeIndex);
            Vector3 pos = piece.position;
            Quaternion rot = piece.rotation;
            var shape = Instantiate(go, pos, rot);
            shape.GetComponent<ShapeModel>().SetMesh(BlockTheme);
        }
    }

    private GameObject GetShape(int index)
    {
        GameObject shape = Array.Find<ShapeData>(shapeDataCollection, x => x.ShapeIndex == index).PlainShape;
        return shape;
    }

    //public void Load()
    //{
    //    if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
    //    {
    //        BinaryFormatter bf = new BinaryFormatter();
    //        FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
    //        levelData = (LevelData)bf.Deserialize(file);
    //        file.Close();
    //    }
    //}
}
