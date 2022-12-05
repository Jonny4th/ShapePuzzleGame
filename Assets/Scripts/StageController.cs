using System;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private StageData dataStorage;

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
    }

    private void StoreShape()
    {
        ShapeModel[] shapes = FindObjectsOfType<ShapeModel>();
        
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
