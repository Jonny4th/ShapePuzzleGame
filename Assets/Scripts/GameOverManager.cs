using System;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] List<PanelStateController> targetPanels;
    [SerializeField] List<PanelStateController> emptyPanels;
    [SerializeField] List<ShapeOverlapController> overlapShape;
    [SerializeField] bool invalidPlacement;

    public static event Action Success;
    public static bool gameIsOver;

    private void Start()
    {
        targetPanels.AddRange(Array.FindAll(FindObjectsOfType<PanelStateController>(), IsTargetPanel));
        emptyPanels.AddRange(Array.FindAll(FindObjectsOfType<PanelStateController>(), x => !IsTargetPanel(x)));
        overlapShape.AddRange(FindObjectsOfType<ShapeOverlapController>());

        gameIsOver = false;
    }

    private static bool IsTargetPanel(PanelStateController panel)
    {
        return (panel.currentState & PanelStateController.State.Target) != 0;
    }

    private static bool IsBlockOn(PanelStateController panel)
    {
        return (panel.currentState & PanelStateController.State.Shadow) != 0;
    }

    private void LateUpdate()
    {
        GameOverConditionCheck(null);
    }
    private void GameOverConditionCheck(MovementInfo info)
    {
        List<PanelStateController> corrects = targetPanels.FindAll(IsBlockOn);
        List<PanelStateController> misses = emptyPanels.FindAll(IsBlockOn);
        invalidPlacement = overlapShape.Exists(x => x.IsOverlap);
        if(invalidPlacement) return;
        if(corrects.Count == targetPanels.Count & misses.Count == 0)
        {
            gameIsOver = true;
            Success?.Invoke();
        }
    }

}