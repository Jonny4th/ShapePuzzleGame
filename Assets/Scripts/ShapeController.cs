using UnityEngine;

public class ShapeController : MonoBehaviour, ISelectable
{
    [SerializeField] BlockPiece[] blocks;
    public GameObject xzplane;
    private void OnEnable()
    {
        blocks = GetComponentsInChildren<BlockPiece>();
        foreach (var item in blocks)
        {
            item.Deselected += OnDeselect;
            item.Selected += OnSelect;
        }
    }
    private void OnDisable()
    {
        foreach (var item in blocks)
        {
            item.Deselected -= OnDeselect;
            item.Selected -= OnSelect;
        }
    }

    public void OnSelect()
    {
        foreach (var item in blocks)
        {
            item.SelectResponse();
        }
    }
    public void OnDeselect()
    {
        foreach (var item in blocks)
        {
            item.DeselectResponse();
        }
    }
}
