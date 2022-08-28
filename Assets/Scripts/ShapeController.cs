using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    [SerializeField] BlockPiece[] blocks;
    private void OnEnable()
    {
        blocks = GetComponentsInChildren<BlockPiece>();
        foreach (var item in blocks)
        {
            item.Deselected += DeselectResponse;
            item.Selected += SelectResponse;
        }
    }
    private void OnDisable()
    {
        foreach (var item in blocks)
        {
            item.Deselected -= DeselectResponse;
            item.Selected -= SelectResponse;
        }
    }

    private void SelectResponse()
    {
        foreach (var item in blocks)
        {
            item.SelectResponse();
        }
    }
    private void DeselectResponse()
    {
        foreach (var item in blocks)
        {
            item.DeselectResponse();
        }
    }
}
