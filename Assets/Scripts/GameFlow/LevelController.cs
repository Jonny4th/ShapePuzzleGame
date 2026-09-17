using Scripts.Models;
using System;
using UnityEngine;
using UnityEngine.Events;
using Scripts.Walls;
using System.Linq;
using System.Collections.Generic;

public class LevelController : MonoBehaviour
{
    [SerializeField] private WallCreatable _wallCreator;
    [SerializeField] private Transform _shapeParent;
    [SerializeField] private LevelBlueprint _levelData;
    [SerializeField] private Mesh _blockTheme;

    [SerializeField] private SceneChange _endSceneChanger;
    public PieceData[] pieceData;

    [Space]
    public UnityEvent OnClueSet;
    public UnityEvent OnPieceSet;
    public UnityEvent<LevelBlueprint> OnBeginConstruction;

    private LevelDataCollection _stageDataCollection = null;
    public int currentIndex = 0;
    private GameObject[] _shapesInScene;

    private void OnEnable()
    {
        if(LevelDataStorage.Instance != null)
        {
            _stageDataCollection = LevelDataStorage.Instance.StageDataCollection;
            _levelData = _stageDataCollection.StageBlueprints[currentIndex];
        }
        
        if (_levelData == null) throw new Exception("Level is not assigned. Add it in the inspector.");

        BuildStage();
    }

    #region Builder Methods
    public LevelController SetStageBlueprint(LevelBlueprint blueprint)
    {
        _levelData = blueprint;
        return this;
    }

    public LevelController SetTheme(Mesh theme)
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
        if(_stageDataCollection == null) throw new Exception("No data in level list. You probably start the level from Puzzle Scene.");
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
