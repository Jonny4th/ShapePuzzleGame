using UnityEngine;

public class ClickSelect : MonoBehaviour, ISelectable
{
    [SerializeField] GameObject shape;
    ISelectable[] blocks;
    private void Awake()
    {
        blocks = shape.GetComponentsInChildren<ISelectable>();
    }
    public void OnDeselect()
    {
        foreach (var item in blocks)
        {
            item.OnDeselect();
        }
    }

    public void OnSelect()
    {
        foreach (var item in blocks)
        {
            item.OnSelect();
        }
    }
}

