using Shape.Movement;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectSelect : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] LayerMask selectable;

    public List<ShapeSelectionController> ShapeInScene;
    public GameObject CurrentSelectedBlock { get; private set; }

    [SerializeField] ShapeSelectionController m_CurrentSelectedShape;
    public ShapeSelectionController CurrentSelectedShape => m_CurrentSelectedShape;

    public int SelectedIndex { get; private set; }

    public static event Action<ShapeSelectionController> ShapeSelected;
    public static event Action<IMotionManagable> MovementHandlerSelected;
    public static event Action<GameObject> BlockSelected;
    public static event Action ShapeDeselected;

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
        m_CurrentSelectedShape = CurrentSelectedBlock.GetComponentInParent<ShapeSelectionController>();
        ShapeSelected?.Invoke(m_CurrentSelectedShape);
        MovementHandlerSelected?.Invoke(m_CurrentSelectedShape.GetComponent<IMotionManagable>());
    }

    private void Clear()
    {
        CurrentSelectedBlock = null;
        m_CurrentSelectedShape = null;
    }

    //public void ClickSelect()
    //{
    //    GameObject current = CurrentSelectedBlock;
    //    if(current != null)
    //    {
    //        current.GetComponent<ISelectable>()?.OnDeselect();
    //        ShapeDeselected?.Invoke();
    //    }
    //    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
    //    if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, selectable, QueryTriggerInteraction.Ignore))
    //    {
    //        if(hit.collider.gameObject.TryGetComponent<ISelectable>(out var seleted))
    //        {
    //            CurrentSelectedBlock = hit.collider.gameObject;
    //            seleted.OnSelect();
    //            BlockSelected?.Invoke(hit.collider.gameObject);
    //        }
    //    }
    //}

    public void TabSelect(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            ShapeSelectionController current = m_CurrentSelectedShape;
            if(current != null)
            {
                var index = ShapeInScene.IndexOf(current);
                current.OnDeselect();
                ShapeDeselected?.Invoke();
                index = (index + 1) % ShapeInScene.Count;
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
        ShapeSelectionController current = m_CurrentSelectedShape;
        if(current != null)
        {
            current.OnDeselect();
            ShapeDeselected?.Invoke();
        }
    }
}
