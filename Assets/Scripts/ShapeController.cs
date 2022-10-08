using System;
using UnityEngine;

public class ShapeController : MonoBehaviour, ISelectable
{
    [SerializeField] BlockPieceSelect[] blocks;

    public event Action OnShapeSelect;
    public event Action OnShapeDeselect;
    public event Action<bool> OnBlockOverlap;

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

    public void BlockOverlap(bool value) => OnBlockOverlap?.Invoke(value);
}
