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
    
    public List<ShapeController> ShapeInScene;
    public GameObject currentSelectedBlock { get; private set; }
    public ShapeController currentSelectedShape { get; private set; }

    public static event Action<GameObject> OnShapeSelect;
    public static event Action OnShapeDeselect;
    public int SelectedIndex {get; set;}

    private void OnEnable()
    {
        OnShapeSelect += ShapeSelectResponse;
        OnShapeDeselect += Clear;
    }

    private void OnDisable()
    {
        OnShapeSelect -= ShapeSelectResponse;
        OnShapeDeselect -= Clear;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        ShapeInScene.AddRange(FindObjectsOfType<ShapeController>());
    }

    private void ShapeSelectResponse(GameObject seleted)
    {
        currentSelectedBlock = seleted;
        currentSelectedShape = currentSelectedBlock.GetComponentInParent<ShapeController>();
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
                OnShapeSelect?.Invoke(hit.collider.gameObject);
            }
        }
    }

    public void TabSelect(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ShapeController current = currentSelectedShape;
            if (current != null)
            {
                var index = ShapeInScene.IndexOf(current);
                current.OnDeselect();
                OnShapeDeselect?.Invoke();
                index = (index+1) % ShapeInScene.Count;
                ShapeController selected = ShapeInScene[index];
                selected.OnSelect();
                OnShapeSelect?.Invoke(selected.gameObject);
            }
            else
            {
                ShapeController selected = ShapeInScene[0];
                selected.OnSelect();
                OnShapeSelect?.Invoke(selected.gameObject);
            }
        }
    }

    public void Deselect()
    {
        ShapeController current = currentSelectedShape;
        if (current != null)
        {
            current.OnDeselect();
            OnShapeDeselect?.Invoke();
        }
    }
}
 