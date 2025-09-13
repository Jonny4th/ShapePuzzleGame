using Scripts.Models;
using System;
using UnityEngine;
using UnityEngine.Events;
using Scripts.Walls;
using System.Linq;
using System.Collections.Generic;

public class StageController : MonoBehaviour
{
    [SerializeField] private WallCreatable _wallCreator;
    [SerializeField] private StageDataSO _levelData;
    [SerializeField] private Mesh _blockTheme;

    public PieceData[] pieceData;
    
    [Space]
    public UnityEvent OnClueSet;
    public UnityEvent OnPieceSet;

    private void OnEnable()
    {
        LoadStageData();
    }

    public void LoadStageData()
    {
        LoadClue();
        LoadShapePieces();
    }

    private void LoadClue()
    {
        IEnumerable<PanelEntity> panels = _wallCreator
        .SetDimention(_levelData.Data.StageSize)
        .Build()
        .Select(x => x.GetComponent<PanelEntity>());

        PanelIdentifier[] activePanels = _levelData.Data.PanelData;

        foreach (var panel in panels)
        {
            if (Array.Exists(activePanels, x => x == panel.Identifier))
            {
                panel.PanelState.SetAsTarget(true);
            }
            else
            {
                panel.PanelState.currentState = PanelStateController.State.None;
            }
        }
    }

    private void LoadShapePieces()
    {
        foreach (var piece in FindObjectsOfType<ShapeModel>())
        {
            DestroyImmediate(piece.gameObject);
        }

        pieceData = _levelData.Data.piece;

        foreach (PieceData piece in pieceData)
        {
            GameObject go = GetShape(piece.shapeIndex);
            Vector3 pos = piece.position;
            Quaternion rot = piece.rotation;
            var shape = Instantiate(go, pos, rot);

            if (_blockTheme != null)
            {
                shape.GetComponent<ShapeModel>().SetMesh(_blockTheme);
            }
        }
    }

    private GameObject GetShape(int index)
    {
        GameObject shape = Array.Find(_levelData.shapeDataCollection.shapeDataList, x => x.ShapeIndex == index).PlainShape;
        return shape;
    }

    //public void Load()
    //{
    //    if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
    //    {
    //        BinaryFormatter bf = new BinaryFormatter();
    //        FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
    //        levelData = (LevelData)bf.Deserialize(file);
    //        file.Close();
    //    }
    //}
}
