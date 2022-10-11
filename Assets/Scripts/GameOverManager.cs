using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] List<PanelOnOff> targetPanels;
    [SerializeField] List<PanelOnOff> emptyPanels;
    [SerializeField] bool invalidPlacement;
    
    public static event Action Success;
    public static bool gameIsOver;

    private void Start() {
        targetPanels.AddRange(Array.FindAll(FindObjectsOfType<PanelOnOff>(), IsTargetPanel));
        emptyPanels.AddRange(Array.FindAll(FindObjectsOfType<PanelOnOff>(), x => !IsTargetPanel(x)));

        gameIsOver = false;
    }
    private void OnEnable()
    {
        //ShapeMovementManager.OnMovement += GameOverConditionCheck;
        foreach ( var shape in FindObjectsOfType<ShapeOverlapController>() )
        {
            shape.OnOverlapChanged += CheckInvalidPlacement;
        }
    }
    private void OnDisable() {
        //ShapeMovementManager.OnMovement -= GameOverConditionCheck;
        foreach (var shape in FindObjectsOfType<ShapeOverlapController>())
        {
            shape.OnOverlapChanged -= CheckInvalidPlacement;
        }
    }

    private static bool IsTargetPanel(PanelOnOff panel)
    {
        return panel.PanelOn;
    }

    private static bool IsBlockOn(PanelOnOff panel)
    {
        return panel.BlockOn;
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
        List<PanelOnOff> corrects = targetPanels.FindAll(IsBlockOn);
        List<PanelOnOff> misses = emptyPanels.FindAll(IsBlockOn);
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