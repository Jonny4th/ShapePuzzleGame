using UnityEngine;
using System.Collections.Generic;

public class BlockPiece : MonoBehaviour, ISelectable
{
    Color original;
    public List<LineRenderer> positionLines;
    public GameObject visual;
    public void OnDeselect()
    {
        Debug.Log("On Deselect.");
        visual.GetComponent<MeshRenderer>().material.color = original;
        foreach (var item in positionLines)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void OnSelect()
    {
        original = visual.GetComponent<MeshRenderer>().material.color;
        visual.GetComponent<MeshRenderer>().material.color = Color.yellow;
        foreach (var item in positionLines)
        {
            item.gameObject.SetActive(true);
        }
    }
}
