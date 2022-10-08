using UnityEngine;

public class BlockPieceSelect : MonoBehaviour, ISelectable
{
    #region MonoBehaviors
    ShapeController shapeController;
    private void Awake()
    {
        shapeController = GetComponentInParent<ShapeController>();
        
    }
    private void OnEnable()
    {
        shapeController.OnShapeSelect += SelectResponse;
        shapeController.OnShapeDeselect += DeselectResponse;
    }
    private void OnDisable()
    {
        shapeController.OnShapeSelect -= SelectResponse;
        shapeController.OnShapeDeselect -= DeselectResponse;
    }
    #endregion

    #region OnShapeSelect/Deselect Responses

    Material originalMaterial;
    [SerializeField] Material selectedMaterial;

    public void SelectResponse()
    {
        originalMaterial = GetComponent<MeshRenderer>().material;
        GetComponent<MeshRenderer>().material = selectedMaterial;
    }
    public void DeselectResponse()
    {
        GetComponent<MeshRenderer>().material = originalMaterial;
    }
    #endregion

    #region ISelectable
    public void OnDeselect()
    {
        GetComponentInParent<ShapeController>().OnDeselect();
    }

    public void OnSelect()
    {
        GetComponentInParent<ShapeController>().OnSelect();
    }
    #endregion
}