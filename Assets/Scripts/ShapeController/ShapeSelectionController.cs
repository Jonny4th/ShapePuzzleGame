using System;
using UnityEngine;

public class ShapeSelectionController : MonoBehaviour, ISelectable
{
    public bool IsSelected { get; private set; }
    public event Action<bool> ShapeSelected;

    #region ISelectable
    public void OnSelect()
    {
        IsSelected = true;
        ShapeSelected?.Invoke(true);
    }
    public void OnDeselect()
    {
        IsSelected = false;
        ShapeSelected?.Invoke(false);
    }
    #endregion
}
