using UnityEngine;
using System;

public class BlockPieceSelect : MonoBehaviour, ISelectable
{
    Material originalMaterial;
    [SerializeField] Material selectedMaterial;
    public event Action Selected;
    public event Action Deselected;
    public void OnDeselect() => Deselected?.Invoke();

    public void OnSelect() => Selected?.Invoke();
    
    public void SelectResponse()
    {
        originalMaterial = GetComponent<MeshRenderer>().material;
        GetComponent<MeshRenderer>().material = selectedMaterial;

    }

    public void DeselectResponse()
    {
        GetComponent<MeshRenderer>().material = originalMaterial;
    }
}
