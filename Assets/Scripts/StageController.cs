using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class StageController : MonoBehaviour
{
    //[SerializeField] private StageData levelData;
    [SerializeField] LevelData levelData;
    [SerializeField] int stageSize;
    [SerializeField] string stageName;
    public PieceData[] pieceData;

    [Serializable]
    public struct PieceData
    {
        public GameObject shape { get; set; }
        public Vector3 position { get; set; }
        public Quaternion rotation { get; set; }
    }

    //private void Start()
    //{
    //    LoadStageData();
    //}

    public void SaveStageData()
    {
        levelData = new LevelData();
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
                panel.SetAsTarget();
            }
            else
            {
                panel.currentState = PanelStateController.State.None;
            }
        }
    }

    private void LoadShapePieces()
    {
        pieceData = levelData.piece;
        foreach (PieceData piece in pieceData)
        {
            GameObject go = piece.shape;
            Vector3 pos = piece.position;
            Quaternion rot = piece.rotation;
            Instantiate(go, pos, rot);
        }

    }


    public void SaveToJSON()
    {
        string fileName = levelData.StageName;
        string data = JsonUtility.ToJson(levelData);
        string path = Application.dataPath + "/Data/StagePuzzles/" + fileName + ".json";
        System.IO.File.WriteAllText(path, data);
        Debug.Log("massage: a file is saved to " + path);
    }

    public void Save()
    {
        BinaryFormatter bf = new();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.streamingAssetsPath +  "/" + stageName + ".gd"); //you can call it anything you want
        bf.Serialize(file, levelData);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            levelData = (LevelData)bf.Deserialize(file);
            file.Close();
        }
    }
}