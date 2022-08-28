using UnityEngine;
using System.Collections.Generic;
using System;

public class BlockPiece : MonoBehaviour, ISelectable
{
    Color original;
    public event Action Selected;
    public event Action Deselected;
    public void OnDeselect()
    {
        Deselected?.Invoke();
    }

    public void OnSelect()
    {
        Selected?.Invoke();
    }

    public void SelectResponse()
    {
        original = GetComponent<MeshRenderer>().material.color;
        GetComponent<MeshRenderer>().material.color = Color.yellow;
    }

    public void DeselectResponse()
    {
        GetComponent<MeshRenderer>().material.color = original;
    }
}
