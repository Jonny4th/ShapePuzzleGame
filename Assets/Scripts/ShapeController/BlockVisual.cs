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
        defaultMaterial = GetComponent<MeshRenderer>().material;
        overlapController = GetComponentInParent<ShapeOverlapController>();
        selectionController = GetComponentInParent<ShapeSelectionController>();
    }

    private void OnEnable()
    {
        overlapController.OverlapChanged += UpdateState;
        selectionController.ShapeSelected += UpdateState;
    }

    private void OnDisable()
    {
        overlapController.OverlapChanged -= UpdateState;
        selectionController.ShapeSelected -= UpdateState;
    }

    private void UpdateVisual()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();

        switch(state)
        {
            case State.Default: mesh.material = defaultMaterial; break;
            case State.Selected:
                {
                    if(selectMaterial == null) break;
                    mesh.material = selectMaterial; break;
                }
            case State.Invalid:
                {
                    if(invalidMaterial == null) break;
                    mesh.material = invalidMaterial; break;
                }
        }
    }

    private void UpdateState(bool _)
    {
        if(overlapController.IsOverlap)
        {
            state = State.Invalid;
        }
        else if(selectionController.IsSelected)
        {
            state = State.Selected;
        }
        else
        {
            state = State.Default;
        }
        UpdateVisual();
    }

    public void SetMesh()
    {
        GetComponent<MeshFilter>().mesh = model.mesh;
    }
}
