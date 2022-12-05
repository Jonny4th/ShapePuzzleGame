using System;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private StageData dataStorage;
    [SerializeField] int stageSize;
    [SerializeField] string stageName;
    public GameObject[] pieces;
    public Vector3[] piecesPosition;
    public Quaternion[] piecesRotation;

    public void SaveStageData()
    {
        StorePuzzle();
        StoreShape();
    }

    private void StorePuzzle()
    {
        PanelStateController[] activePanels = Array.FindAll(FindObjectsOfType<PanelStateController>(), x => (x.currentState & PanelStateController.State.Target) != 0);
        dataStorage.PanelData = new Vector3[activePanels.Length];
        for (int i = 0; i < activePanels.Length; i++)
        {
            dataStorage.PanelData[i] = Vector3Int.RoundToInt(activePanels[i].transform.position);
        }
        dataStorage.StageName = stageName;
        dataStorage.StageSize = stageSize;
    }

    private void StoreShape()
    {
        ShapeModel[] shape = FindObjectsOfType<ShapeModel>();
        pieces = new GameObject[shape.Length];
        for (int i = 0; i < shape.Length; i++)
        {
            Debug.Log(shape[i].transform);
            pieces[i] = shape[i].plainShape;
            Debug.Log(shape[i].transform.position);
            Debug.Log(shape[i].transform.rotation);
        }
        dataStorage.pieces = pieces;
    }

    public void LoadStageData()
    {
        Vector3[] activePanelCoordinates = dataStorage.PanelData;
        PanelStateController[] panels = FindObjectsOfType<PanelStateController>();
        foreach (PanelStateController panel in panels)
        {
            if ( Array.Exists(activePanelCoordinates, x => x == panel.transform.position) )
            {
                panel.SetAsTarget();
            }
            else
            {
                panel.currentState = PanelStateController.State.None;
            }
        }

        pieces = dataStorage.pieces;
        foreach (Tuple<GameObject,Transform> piece in pieces)
        {
            GameObject go = piece.Item1;
            Transform transform = piece.Item2;
            Debug.Log("Load: " + transform.position);
            Instantiate(go, transform);
        }
    }

    public void SaveToJSON()
    {
        string fileName = dataStorage.StageName;
        string data = JsonUtility.ToJson(dataStorage);
        string path = Application.dataPath + "/Data/StagePuzzles/" + fileName + ".json";
        System.IO.File.WriteAllText(path, data);
        Debug.Log("massage: a file is saved to " + path);
    }
}
