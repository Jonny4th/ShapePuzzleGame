using PuzzleData;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PuzzleData.PuzzleCreator))]
public class PuzzleCreatorInspectorUI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PuzzleCreator controller = (PuzzleCreator)target;
        if (GUILayout.Button("Save Puzzle to Scriptable Obj"))
        {
            controller.SaveStageData();
        }
        if (GUILayout.Button("Save to JSON"))
        {
            controller.SaveToJSON();
        }
        if (GUILayout.Button("Load Stage Data from Scriptable Object"))
        {
            controller.LoadStageData();
        }

    }
}
