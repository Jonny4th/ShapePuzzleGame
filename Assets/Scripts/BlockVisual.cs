using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockVisual : MonoBehaviour
{
    ShapeModel model;
    ShapeOverlapController overlapController;
    ShapeSelectionController selectionController;

    Material defaultMaterial;
    [SerializeField] Material invalidMaterial;
    [SerializeField] Material selectMaterial;

    public enum State
    {
        Default,
        Selected,
        NonSelected,
        Invalid
    }

    public State state = State.Default;

    private void Awake()
    {
        model = GetComponentInParent<ShapeModel>();
        defaultMaterial = model.Data.OriginalMaterial;
        overlapController = GetComponentInParent<ShapeOverlapController>();
        selectionController = GetComponentInParent<ShapeSelectionController>();
    }

    private void OnEnable()
    {
        overlapController.OverlapChanged += UpdateState;
        selectionController.ShapeSelected  += UpdateState;
    }

    private void OnDisable()
    {
        overlapController.OverlapChanged -= UpdateState;
        selectionController.ShapeSelected  -= UpdateState;
    }

    private void UpdateVisual()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();

        switch(state)
        {
            case State.Default:  mesh.material = defaultMaterial; break;
            case State.Selected: mesh.material = selectMaterial;  break;
            case State.Invalid:  mesh.material = invalidMaterial; break;
        }
    }

    private void UpdateState(bool _)
    { 
        if(overlapController.IsOverlap)
        {
            state = State.Invalid;
        }
        else if (selectionController.IsSelected)
        {
            state = State.Selected;
        }
        else
        {
            state = State.Default;
        }
        UpdateVisual();
    }    
}
