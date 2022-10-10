using System;
using UnityEngine;

public class ShapeSelectionController : MonoBehaviour, ISelectable
{
    public event Action OnShapeSelect;
    public event Action OnShapeDeselect;

    #region ISelectable
    public void OnSelect()
    {
        OnShapeSelect?.Invoke();
    }
    public void OnDeselect()
    {
        OnShapeDeselect?.Invoke();
    }
    #endregion
}
