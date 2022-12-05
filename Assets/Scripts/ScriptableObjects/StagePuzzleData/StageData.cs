using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Stage Data")]

[System.Serializable]
public class StageData : ScriptableObject
{
    public string StageName;
    public int StageId;
    public int StageSize;
    public Vector3[] PanelData;
    public GameObject[] pieces;
    public Vector3[] piecesPosition;
    public Quaternion[] piecesRotation;
}
