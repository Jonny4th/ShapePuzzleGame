using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(StageController))]
public class StageDataSaveUI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        StageController controller = (StageController)target;
        if (GUILayout.Button("Save Puzzle to Scriptable Obj"))
        {
            controller.SaveStageData();
        }
        if (GUILayout.Button("Save to JSON"))
        {
            controller.SaveToJSON();
        }

    }
}
