using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Runtime.ConstrainedExecution;

public class ObjectSelect : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] LayerMask selectable;
    
    public List<ShapeSelectionController> ShapeInScene;
    public GameObject currentSelectedBlock { get; private set; }
    public ShapeSelectionController currentSelectedShape { get; private set; }
    public int SelectedIndex {get; private set;}

    public static event Action<ShapeSelectionController> OnShapeSelect;
    public static event Action<GameObject> OnBlockSelect;
    public static event Action OnShapeDeselect;

    private void OnEnable()
    {
        OnBlockSelect += ShapeSelectResponse;
        OnShapeDeselect += Clear;
    }

    private void OnDisable()
    {
        OnBlockSelect -= ShapeSelectResponse;
        OnShapeDeselect -= Clear;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        ShapeInScene.AddRange(FindObjectsOfType<ShapeSelectionController>());
    }

    private void ShapeSelectResponse(GameObject seleted)
    {
        currentSelectedBlock = seleted;
        currentSelectedShape = currentSelectedBlock.GetComponentInParent<ShapeSelectionController>();
        OnShapeSelect?.Invoke(currentSelectedShape);
    }

    private void Clear()
    {
        currentSelectedBlock = null;
        currentSelectedShape = null;
    }

    public void ClickSelect()
    {
        GameObject current = currentSelectedBlock;
        if (current != null)
        {
            current.GetComponent<ISelectable>()?.OnDeselect();
            OnShapeDeselect?.Invoke();
        }
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, selectable, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.gameObject.TryGetComponent<ISelectable>(out var seleted))
            {
                currentSelectedBlock = hit.collider.gameObject;
                seleted.OnSelect();
                OnBlockSelect?.Invoke(hit.collider.gameObject);
            }
        }
    }

    public void TabSelect(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ShapeSelectionController current = currentSelectedShape;
            if (current != null)
            {
                var index = ShapeInScene.IndexOf(current);
                current.OnDeselect();
                OnShapeDeselect?.Invoke();
                index = (index+1) % ShapeInScene.Count;
                ShapeSelectionController selected = ShapeInScene[index];
                selected.OnSelect();
                OnBlockSelect?.Invoke(selected.gameObject);
            }
            else
            {
                ShapeSelectionController selected = ShapeInScene[0];
                selected.OnSelect();
                OnBlockSelect?.Invoke(selected.gameObject);
            }
        }
    }

    public void Deselect()
    {
        ShapeSelectionController current = currentSelectedShape;
        if (current != null)
        {
            current.OnDeselect();
            OnShapeDeselect?.Invoke();
        }
    }
}
 