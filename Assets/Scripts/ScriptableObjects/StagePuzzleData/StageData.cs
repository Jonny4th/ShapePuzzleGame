using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Stage Data")]

[System.Serializable]
public class StageData : ScriptableObject
{
    public string StageName;
    public Vector3[] SavedData;
    public GameObject[] Shapes;
}
