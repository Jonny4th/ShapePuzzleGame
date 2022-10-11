using System;
using UnityEngine;

public class ShapeSelectionController : MonoBehaviour, ISelectable
{
    public bool IsSelected { get; private set; }
    public event Action<bool> OnShapeSelect;

    #region ISelectable
    public void OnSelect()
    {
        IsSelected = true;
        OnShapeSelect?.Invoke(true);
    }
    public void OnDeselect()
    {
        IsSelected = false;
        OnShapeSelect?.Invoke(false);
    }
    #endregion
}
