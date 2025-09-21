using Scripts.Models;
using System;
using UnityEngine;
using UnityEngine.Events;
using Scripts.Walls;
using System.Linq;
using System.Collections.Generic;

public class StageController : MonoBehaviour
{
    [SerializeField] private StageDataCollection _stageDataCollection;
    [SerializeField] private WallCreatable _wallCreator;
    [SerializeField] private Transform _shapeParent;
    [SerializeField] private StageBlueprint _levelData;
    [SerializeField] private Mesh _blockTheme;

    [SerializeField] private SceneChange _endSceneChanger;
    public PieceData[] pieceData;

    [Space]
    public UnityEvent OnClueSet;
    public UnityEvent OnPieceSet;
    public UnityEvent<StageBlueprint> OnBeginConstruction;

    public int currentIndex = 0;
    private GameObject[] _shapesInScene;

    private void OnEnable()
    {
        _levelData = _stageDataCollection.StageBlueprints[currentIndex];
        BuildStage();
    }

    #region Builder Methods
    public StageController SetStageBlueprint(StageBlueprint blueprint)
    {
        _levelData = blueprint;
        return this;
    }

    public StageController SetTheme(Mesh theme)
    {
        _blockTheme = theme;
        return this;
    }

    public void BuildStage()
    {
        OnBeginConstruction?.Invoke(_levelData);
        LoadClue();
        LoadShapePieces();
    }
    #endregion

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
                panel.PanelState.SetAsClue(true);
            }
            else
            {
                panel.PanelState.currentState = PanelStateController.State.None;
            }
        }

        OnClueSet?.Invoke();
    }

    private void LoadShapePieces()
    {
        pieceData = _levelData.Data.piece;
        List<GameObject> shapeList = new();

        foreach (PieceData piece in pieceData)
        {
            GameObject go = GetShape(piece.shapeIndex);
            Vector3 pos = piece.position;
            Quaternion rot = piece.rotation;
            var shape = Instantiate(go, pos, rot, _shapeParent);

            if (_blockTheme != null)
            {
                shape.GetComponent<ShapeModel>().SetMesh(_blockTheme);
            }

            shapeList.Add(shape);
        }

        _shapesInScene = shapeList.ToArray();

        OnPieceSet?.Invoke();
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

    public void NextStage()
    {
        currentIndex++;
        if(currentIndex >= _stageDataCollection.StageBlueprints.Length)
        {
            currentIndex = 0;
            _endSceneChanger.ChangeScene();
            return;
        }

        _levelData = _stageDataCollection.StageBlueprints[currentIndex];
        _wallCreator.Clear();

        foreach (var piece in _shapesInScene)
        {
            DestroyImmediate(piece);
        }

        BuildStage();
        
        BroadcastMessage("OnReset", SendMessageOptions.DontRequireReceiver);
    }
}
