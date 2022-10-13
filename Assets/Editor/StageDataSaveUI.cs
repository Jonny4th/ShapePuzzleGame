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
        if (GUILayout.Button("Save Stage Puzzle"))
        {
            controller.SaveStageData();
        }
        if (GUILayout.Button("Save to JSON"))
        {
            controller.SaveToJSON();
        }

    }
}
