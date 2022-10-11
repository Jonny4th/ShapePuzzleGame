using UnityEngine;

public class BlockSelect : MonoBehaviour, ISelectable
{
    ShapeSelectionController selectionController;
    
    #region MonoBehaviors
    private void Awake()
    {
        selectionController = GetComponentInParent<ShapeSelectionController>();
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