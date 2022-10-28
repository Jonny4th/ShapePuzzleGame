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
        PanelOnOff[] activePanels = Array.FindAll(FindObjectsOfType<PanelOnOff>(), x => x.IsOn == true);
        dataStorage.SavedData = new Vector3[activePanels.Length];
        for (int i = 0; i < activePanels.Length; i++)
        {
            dataStorage.SavedData[i] = Vector3Int.RoundToInt(activePanels[i].transform.position);
        }
    }

    private void StoreShape()
    {
        ShapeModel[] shapes = FindObjectsOfType<ShapeModel>();
        
    }

    public void LoadStageData()
    {
        Vector3[] activePanelCoordinates = dataStorage.SavedData;
        PanelOnOff[] panels = FindObjectsOfType<PanelOnOff>();
        foreach (PanelOnOff panel in panels)
        {
            if ( Array.Exists(activePanelCoordinates, x => x == panel.transform.position) )
            {
                panel.SetPanelOn();
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
