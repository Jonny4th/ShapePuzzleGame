using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShapeData", menuName = "Shape Data")]
public class ShapeData : ScriptableObject
{
    public int ShapeIndex;
    public Material OriginalMaterial;
    public GameObject PlainShape;
}
