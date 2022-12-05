using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShapeAlignToGrid : MonoBehaviour
{
    public float GridSize;
    private void LateUpdate() {
        AlignToGrid();
    }
    public void AlignToGrid()
    {
        var pos = transform.position; // catch position
        var x = Mathf.RoundToInt(transform.position.x); //snap to nearest int
        var y = Mathf.RoundToInt(transform.position.y); //snap to nearest int
        var z = Mathf.RoundToInt(transform.position.z); //snap to nearest int
        transform.position = new Vector3(x,y,z);
    }
}
