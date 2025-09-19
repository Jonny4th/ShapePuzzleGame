using UnityEditor;
using UnityEngine;

namespace Scripts.Walls
{
    
    [CustomEditor(typeof(WallCreator))]
    public class WallCreatorInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            WallCreator creator = (WallCreator)target;

            if (GUILayout.Button("Build"))
            {
                creator.Build();
            }
            
            if(GUILayout.Button("Clear"))
            {
                creator.Clear();
            }
        }
    }
}
