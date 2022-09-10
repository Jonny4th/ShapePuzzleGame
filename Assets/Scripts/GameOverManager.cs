using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] List<PanelOnOff> targetPanels;
    [SerializeField] List<PanelOnOff> emptyPanels;
    // [SerializeField] List<PanelOnOff> corrects;
    public static event Action Success;
    public static bool gameIsOver;
    private void Awake() {
    }
    private void Start() {
        targetPanels.AddRange(Array.FindAll(FindObjectsOfType<PanelOnOff>(), IsTargetPanel));
        emptyPanels.AddRange(Array.FindAll(FindObjectsOfType<PanelOnOff>(), x => !IsTargetPanel(x)));
        gameIsOver = false;
        ShapeMovementManager.OnRotate += PanelCheck;
        ShapeMovementManager.OnTranslate += PanelCheck;
    }
    private void OnDisable() {
        ShapeMovementManager.OnRotate -= PanelCheck;
        ShapeMovementManager.OnTranslate -= PanelCheck;
    }

    private static bool IsTargetPanel(PanelOnOff panel)
    {
        return panel.PanelOn;
    }

    private static bool IsBlockOn(PanelOnOff panel)
    {
        return panel.BlockOn;
    }

    private void PanelCheck()
    {
        List<PanelOnOff> corrects = new List<PanelOnOff>();
        corrects = targetPanels.FindAll(IsBlockOn);
        List<PanelOnOff> misses = new List<PanelOnOff>();
        misses = emptyPanels.FindAll(IsBlockOn);
        if (corrects.Count == targetPanels.Count && misses.Count == 0)
        {
            gameIsOver = true;
            Success?.Invoke();
        }
    }
}