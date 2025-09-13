using Scripts.Models;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

namespace PuzzleData
{
    public partial class PuzzleCreator : MonoBehaviour
    {
        PanelEntity[] allPanels;
        [SerializeField] PanelEntity[] activePanels;
        [SerializeField] StageDataSO levelData;
        [SerializeField] Vector3Int stageSize;
        [SerializeField] string stageName;
        public PieceData[] pieceData;

        [SerializeField] ShapeDataList shapeDataCollection;

        public void ImprintShadowAsPuzzle()
        {

            allPanels = FindObjectsOfType<PanelEntity>();

            foreach (var panel in allPanels)
            {
                panel.PanelState.SetAsTarget(false);
            }

            activePanels = Array.FindAll(allPanels, x => (x.PanelState.currentState & PanelStateController.State.Shadow) != 0);

            foreach (var panel in activePanels)
            {
                panel.PanelState.SetAsTarget(true);
            }
        }

        public void SaveStageData()
        {
            if (levelData.Data.PanelData.Length!=0)
            {
                throw new Exception("Cannot overwrite. Please use new LevelData file.");
            }
            SavePanelData();
            SaveShapes();
#if UNITY_EDITOR            
            EditorUtility.SetDirty(levelData);
#endif
        }

        private void SavePanelData()
        {
            if (activePanels.Length == 0)
            {
                activePanels = Array.FindAll(FindObjectsOfType<PanelEntity>(), x => (x.PanelState.currentState & PanelStateController.State.Target) != 0);
            }
            
            levelData.Data.PanelData = new PanelIdentifier[activePanels.Length];

            for (int i = 0; i < activePanels.Length; i++)
            {
                levelData.Data.PanelData[i] = activePanels[i].Identifier;
            }
        }

        private void SaveShapes()
        {
            ShapeModel[] shapes = FindObjectsOfType<ShapeModel>();
            pieceData = new PieceData[shapes.Length];
            int i = 0;
            foreach (var shape in shapes)
            {
                pieceData[i] = new PieceData
                {
                    shapeIndex = shape.shapeIndex,
                    position = Vector3Int.RoundToInt(shape.transform.position),
                    rotation = shape.transform.rotation,
                };
                i++;
            }
            levelData.Data.piece = pieceData;
        }

        public void LoadStageData()
        {
            LoadPuzzle();
            LoadShapePieces();
        }

        private void LoadPuzzle()
        {
            ResetPanelState();
            var panelIdendifier = levelData.Data.PanelData;
            var panels = FindObjectsOfType<PanelEntity>();
            foreach (var panel in panels)
            {
                if (Array.Exists(panelIdendifier, x => x == panel.Identifier))
                {
                    panel.PanelState.SetAsTarget(true);
                }
            }
        }

        private void LoadShapePieces()
        {
            ClearShape();

            pieceData = levelData.Data.piece;
            foreach (PieceData piece in pieceData)
            {
                GameObject go = Array.Find(shapeDataCollection.shapeDataList, x => x.ShapeIndex == piece.shapeIndex).PlainShape;
                Vector3 pos = piece.position;
                Quaternion rot = piece.rotation;
                Instantiate(go, pos, rot);
            }
        }


        public void SaveToJSON()
        {
            levelData.Data.StageName = stageName;
            levelData.Data.StageSize = stageSize;
            levelData.Data.PanelData = new PanelIdentifier[activePanels.Length];

            for (int i = 0; i < activePanels.Length; i++)
            {
                levelData.Data.PanelData[i] = activePanels[i].Identifier;
            }

            levelData.Data.piece = pieceData;
            string fileName = levelData.Data.StageName;
            string data = JsonUtility.ToJson(levelData);
            string path = Application.dataPath + "/Data/StagePuzzles/" + fileName + "_" + DateTime.Now.ToString("dd''MM''yyyy''HH''mm''ss") +".json";
            File.WriteAllText(path, data);
            Debug.Log("massage: a file is saved to " + path);
        }

        public void Save()
        {
            BinaryFormatter bf = new();
            //Application.persistentDataPath is a string, so if you want you can put that into debug.log if you want to know where save games are located
            FileStream file = File.Create(Application.streamingAssetsPath + "/" + stageName + ".gd"); //you can call it anything you want
            bf.Serialize(file, levelData);
            file.Close();
        }

        public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
                levelData = (StageDataSO)bf.Deserialize(file);
                file.Close();
            }
        }

        public void ClearScene()
        {
            ResetPanelState();
            ClearShape();
        }
        public void ResetPanelState()
        {
            allPanels = FindObjectsOfType<PanelEntity>();
            foreach (var panel in allPanels)
            {
                panel.PanelState.SetAsTarget(false);
            }
        }

        void ClearShape()
        {
            ShapeModel[] shapes = FindObjectsOfType<ShapeModel>();
            foreach (var shape in shapes)
            {
                DestroyImmediate(shape.gameObject);
            }
        }
    }
}