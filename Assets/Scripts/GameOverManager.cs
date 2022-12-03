using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] List<PanelStateController> targetPanels;
    [SerializeField] List<PanelStateController> emptyPanels;
    [SerializeField] bool invalidPlacement;
    
    public static event Action Success;
    public static bool gameIsOver;

    private void Start()
    {
        targetPanels.AddRange(Array.FindAll(FindObjectsOfType<PanelStateController>(), IsTargetPanel));
        emptyPanels.AddRange(Array.FindAll(FindObjectsOfType<PanelStateController>(), x => !IsTargetPanel(x)));

        gameIsOver = false;
    }
    private void OnEnable()
    {
        //ShapeMovementManager.OnMovement += GameOverConditionCheck;
        foreach ( var shape in FindObjectsOfType<ShapeOverlapController>() )
        {
            shape.OverlapChanged += CheckInvalidPlacement;
        }
    }
    private void OnDisable() {
        //ShapeMovementManager.OnMovement -= GameOverConditionCheck;
        foreach (var shape in FindObjectsOfType<ShapeOverlapController>())
        {
            shape.OverlapChanged -= CheckInvalidPlacement;
        }
    }

    private static bool IsTargetPanel(PanelStateController panel)
    {
        return (panel.currentState & PanelStateController.State.Target) != 0;
    }

    private static bool IsBlockOn(PanelStateController panel)
    {
        return (panel.currentState & PanelStateController.State.Shadow) != 0;
    }

    private void CheckInvalidPlacement(bool value)
    {
        invalidPlacement = value;
    }

    private void LateUpdate()
    {
        GameOverConditionCheck();
    }
    private void GameOverConditionCheck(OnMovementInfo info)
    {
        List<PanelStateController> corrects = targetPanels.FindAll(IsBlockOn);
        List<PanelStateController> misses = emptyPanels.FindAll(IsBlockOn);
        if (corrects.Count == targetPanels.Count & misses.Count == 0 & !invalidPlacement)
        {
            gameIsOver = true;
            Success?.Invoke();
        }
    }
    private void GameOverConditionCheck()
    {
        GameOverConditionCheck(null);
    }

}