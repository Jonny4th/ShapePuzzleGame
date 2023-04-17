using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Runtime.ConstrainedExecution;

public class ObjectSelect : MonoBehaviour
{
    public List<ShapeSelectionController> ShapeInScene;
    public ShapeSelectionController CurrentSelectedShape { get; private set; }
    public int SelectedIndex {get; private set;}
    public static event Action<ShapeSelectionController> ShapeSelected;
    public static event Action<GameObject> BlockSelected;
    public static event Action ShapeDeselected;
    
    Camera mainCamera;
    [SerializeField] LayerMask selectable;
    [SerializeField] GameObject CurrentSelectedBlock;

    private void OnEnable()
    {
        BlockSelected += OnShapeSelect;
        ShapeDeselected += Clear;
    }

    private void OnDisable()
    {
        BlockSelected -= OnShapeSelect;
        ShapeDeselected -= Clear;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        ShapeInScene.AddRange(FindObjectsOfType<ShapeSelectionController>());
    }

    private void OnShapeSelect(GameObject seleted)
    {
        CurrentSelectedBlock = seleted;
        CurrentSelectedShape = CurrentSelectedBlock.GetComponentInParent<ShapeSelectionController>();
        ShapeSelected?.Invoke(CurrentSelectedShape);
    }

    private void Clear()
    {
        CurrentSelectedBlock = null;
        CurrentSelectedShape = null;
    }

    public void ClickSelect()
    {
        GameObject current = CurrentSelectedBlock;
        if (current != null)
        {
            current.GetComponent<ISelectable>()?.OnDeselect();
            ShapeDeselected?.Invoke();
        }
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, selectable, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.gameObject.TryGetComponent<ISelectable>(out var seleted))
            {
                CurrentSelectedBlock = hit.collider.gameObject;
                seleted.OnSelect();
                BlockSelected?.Invoke(hit.collider.gameObject);
            }
        }
    }

    public void TabSelect(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ShapeSelectionController current = CurrentSelectedShape;
            if (current != null)
            {
                var index = ShapeInScene.IndexOf(current);
                current.OnDeselect();
                ShapeDeselected?.Invoke();
                index = (index+1) % ShapeInScene.Count;
                ShapeSelectionController selected = ShapeInScene[index];
                selected.OnSelect();
                BlockSelected?.Invoke(selected.gameObject);
            }
            else
            {
                ShapeSelectionController selected = ShapeInScene[0];
                selected.OnSelect();
                BlockSelected?.Invoke(selected.gameObject);
            }
        }
    }

    public void OnDeselect()
    {
        ShapeSelectionController current = CurrentSelectedShape;
        if (current != null)
        {
            current.OnDeselect();
            ShapeDeselected?.Invoke();
        }
    }
}
 