using ScriptableObjectEvent;
using Shape.Controller;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] List<ClueTile> targetPanels;
    [SerializeField] List<ClueTile> emptyPanels;
    [SerializeField] List<ShapeOverlapController> shapeInScene;
    [SerializeField] bool invalidPlacement;

    public static bool gameIsOver;

    public SOGameEvent Success;

    private void Start()
    {
        Setup();
    }

    public void Setup()
    {
        targetPanels.AddRange(Array.FindAll(FindObjectsByType<ClueTile>( FindObjectsSortMode.None), IsTargetPanel));
        emptyPanels.AddRange(Array.FindAll(FindObjectsByType<ClueTile>( FindObjectsSortMode.None), x => !IsTargetPanel(x)));
        shapeInScene.AddRange(FindObjectsByType<ShapeOverlapController>( FindObjectsSortMode.None));
        gameIsOver = false;
    }

    public void OnReset()
    {
        Debug.Log("Reset GameOverManager");
        targetPanels.Clear();
        emptyPanels.Clear();
        shapeInScene.Clear();
        Setup();
    }

    private bool IsTargetPanel(ClueTile panel)
    {
        return (panel.CurrentState & ClueTile.State.Target) != 0;
    }

    private bool IsBlockOn(ClueTile panel)
    {
        return (panel.CurrentState & ClueTile.State.Shadow) != 0;
    }

    private void LateUpdate()
    {
        if(gameIsOver) return;
        GameOverConditionCheck();
    }

    private void GameOverConditionCheck()
    {
        if(shapeInScene.Exists(x => x.IsOverlap)) return;

        List<ClueTile> misses = emptyPanels.FindAll(IsBlockOn);
        if(misses.Count > 0) return;

        List<ClueTile> corrects = targetPanels.FindAll(IsBlockOn);
        if(corrects.Count != targetPanels.Count) return;

        gameIsOver = true;

        Success.Raise(this, true);
    }
}