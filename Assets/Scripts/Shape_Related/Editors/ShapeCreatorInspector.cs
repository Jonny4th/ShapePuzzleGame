using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ShapeCreator))]
public class ShapeCreatorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ShapeCreator shapeCreator = (ShapeCreator)target;

        if(GUILayout.Button("Create"))
        {
            shapeCreator.Create();
        }
    }
}
