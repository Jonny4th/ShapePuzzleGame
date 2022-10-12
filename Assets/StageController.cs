using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private StageData data;

    private void Start()
    {
        SaveStageData();
    }

    public void SaveStageData()
    {
        PanelOnOff[] activePanels = Array.FindAll(FindObjectsOfType<PanelOnOff>(), x => x.IsOn);
        data.ActivePanels = activePanels;
    }

    public void LoadStageData()
    {
        PanelOnOff[] activePanels = data.ActivePanels;
        foreach (PanelOnOff panel in activePanels)
        {
            panel.SetPanelOn();
        }
    }
}
