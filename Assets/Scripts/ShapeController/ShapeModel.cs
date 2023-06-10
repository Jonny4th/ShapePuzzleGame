using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeModel : MonoBehaviour
{
    public ShapeData shapeData;
    public int shapeIndex
    {
        get { return shapeData.ShapeIndex; }
    }
    public GameObject plainShape
    {
        get
        {
            return shapeData.PlainShape;
        }
    }
    public Mesh mesh
    {
        get; private set;
    }

    public Material material
    {
        get
        {
            return shapeData.OriginalMaterial;
        }
    }

    public void SetMesh(Mesh m)
    {
        mesh = m;
        foreach (var block in GetComponentsInChildren<BlockVisual>())
        {
            block.SetMesh();
        }
    }
    
}
