﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
namespace ScriptableObjectVariable
{
    [CustomEditor(typeof(SOVector3))]
    [CanEditMultipleObjects]
    public class SOVector3Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            //Draw the defualt inspector options
            DrawDefaultInspector();

            SOVector3 script = (SOVector3)target;

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.LabelField("Debugging Options", EditorStyles.centeredGreyMiniLabel);

            EditorGUILayout.LabelField("Current value: " + script.value, EditorStyles.boldLabel);

            //Display button that resets the value to the starting value
            if(GUILayout.Button("Reset Value"))
            {
                if(EditorApplication.isPlaying)
                {
                    script.ResetValue();
                }
            }
            EditorGUILayout.EndVertical();
        }
    }
}
#endif
