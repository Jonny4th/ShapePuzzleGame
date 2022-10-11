using UnityEngine;

public class BlockSelect : MonoBehaviour, ISelectable
{
    ShapeSelectionController selectionController;
    [SerializeField] Material selectedMaterial;
    
    #region MonoBehaviors
    private void Awake()
    {
        selectionController = GetComponentInParent<ShapeSelectionController>();
    }
    private void OnEnable()
    {
        //selectionController.OnShapeSelect += SelectResponse;
    }
    private void OnDisable()
    {
        //selectionController.OnShapeSelect -= SelectResponse;
    }
    #endregion

    #region OnShapeSelect/Deselect Responses
    public void SelectResponse(bool isSelected)
    {
        if(isSelected) GetComponent<MeshRenderer>().material = selectedMaterial;
        else GetComponent<MeshRenderer>().material = GetComponentInParent<ShapeModel>().Data.OriginalMaterial;
    }
    #endregion

    #region ISelectable
    public void OnDeselect()
    {
        selectionController.OnDeselect();
    }

    public void OnSelect()
    {
        selectionController.OnSelect();
    }
    #endregion
}