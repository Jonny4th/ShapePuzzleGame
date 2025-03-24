using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WallCreator))]
public class WallCreatorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        WallCreator creator = (WallCreator)target;

        if(GUILayout.Button("Build"))
        {
            creator.Create();
        }
    }
}
