using System;
using UnityEngine;

public class ShapeController : MonoBehaviour, ISelectable
{
    [SerializeField] BlockPieceSelect[] blocks;

    public event Action OnShapeSelect;
    public event Action OnShapeDeselect;

    private void Awake()
    {
        blocks = GetComponentsInChildren<BlockPieceSelect>();
    }

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
