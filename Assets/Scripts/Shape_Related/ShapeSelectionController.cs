using UnityEngine;
using UnityEngine.Events;

public class ShapeSelectionController : MonoBehaviour, ISelectable
{
    [SerializeField]
    private bool isSelected;
    public bool IsSelected => isSelected;
    public UnityEvent<bool> OnShapeSelected;

    #region ISelectable
    public void OnSelect()
    {
        isSelected = true;
        OnSelected();
    }
    public void OnDeselect()
    {
        isSelected = false;
        OnSelected();
    }

    public void OnSelected()
    {
        OnShapeSelected?.Invoke(isSelected);
    }
    #endregion
}

