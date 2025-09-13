using UnityEditor;
using UnityEngine;
using Scripts.Walls;

[CustomEditor(typeof(WallManager))]
public class WallManagerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        WallManager manager = (WallManager)target;
        
        if(GUILayout.Button("Set"))
        {
            manager.Awake();
        }
        
        if(GUILayout.Button("Build"))
        {
            manager.Build();
        }

        if(GUILayout.Button("Clear"))
        {
            manager.Clear();
        }
    }
}
