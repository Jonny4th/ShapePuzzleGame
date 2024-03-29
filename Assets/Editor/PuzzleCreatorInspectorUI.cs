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
        if (GUILayout.Button("Imprint Panels State"))
        {
            controller.ImprintShadowAsPuzzle();
        }
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
        if (GUILayout.Button("Reset Panels State"))
        {
            controller.ResetPanelState();
        }
        if (GUILayout.Button("Clear Scene"))
        {
            controller.ClearScene();
        }

    }
}
