using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace PuzzleData
{
    public class PuzzleCreator : MonoBehaviour
    {
        PanelStateController[] allPanels;
        [SerializeField] StageData levelData;
        [SerializeField] int stageSize;
        [SerializeField] string stageName;
        public PieceData[] pieceData;

        [Serializable]
        public struct PieceData
        {
            public GameObject shape { get; set; }
            public int shapeIndex { get; set; }
            public Vector3 position { get; set; }
            public Quaternion rotation { get; set; }
        }

        public void SaveStageData()
        {
            SaveShapes();
        }

        public void ImprintShadowAsPuzzle()
        {
            allPanels = FindObjectsOfType<PanelStateController>();
            foreach (PanelStateController panel in allPanels)
            {
                panel.SetAsTarget(false);
            }
            PanelStateController[] activePanels = Array.FindAll(allPanels, x => (x.currentState & PanelStateController.State.Shadow) != 0);
            foreach (PanelStateController panel in activePanels)
            {
                panel.SetAsTarget(true);
            }
            levelData.PanelData = new Vector3[activePanels.Length];
            for (int i = 0; i < activePanels.Length; i++)
            {
                levelData.PanelData[i] = Vector3Int.RoundToInt(activePanels[i].transform.position);
            }
            levelData.StageName = stageName;
            levelData.StageSize = stageSize;
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
                    position = shape.transform.position,
                    rotation = shape.transform.rotation,
                };
                i++;
            }
            levelData.piece = pieceData;
        }

        public void LoadStageData()
        {
            LoadPuzzle();
            LoadShapePieces();
        }

        private void LoadPuzzle()
        {
            ResetPanelState();
            Vector3[] activePanelCoordinates = levelData.PanelData;
            PanelStateController[] panels = FindObjectsOfType<PanelStateController>();
            foreach (PanelStateController panel in panels)
            {
                if (Array.Exists(activePanelCoordinates, x => x == panel.transform.position))
                {
                    panel.SetAsTarget(true);
                }
            }
        }

        private void LoadShapePieces()
        {
            ClearShape();

            pieceData = levelData.piece;
            foreach (PieceData piece in pieceData)
            {
                GameObject go = piece.shape;
                Vector3 pos = piece.position;
                Quaternion rot = piece.rotation;
                Instantiate(go, pos, rot);
            }
        }


        public void SaveToJSON()
        {
            string fileName = levelData.StageName;
            string data = JsonUtility.ToJson(levelData);
            string path = Application.dataPath + "/Data/StagePuzzles/" + fileName + ".json";
            System.IO.File.WriteAllText(path, data);
            Debug.Log("massage: a file is saved to " + path);
        }

        public void Save()
        {
            BinaryFormatter bf = new();
            //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
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
                levelData = (StageData)bf.Deserialize(file);
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
            allPanels = FindObjectsOfType<PanelStateController>();
            foreach (var panel in allPanels)
            {
                panel.SetAsTarget(false);
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