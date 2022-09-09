using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] List<PanelOnOff> panels;
    private void Awake() {
        
        panels.AddRange(Array.FindAll(FindObjectsOfType<PanelOnOff>(), IsTargetPanel));
        
    }

    private static bool IsTargetPanel(PanelOnOff panel)
    {
        return panel.PanelOn;
    }
}