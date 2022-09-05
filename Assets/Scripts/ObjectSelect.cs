using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class ObjectSelect : MonoBehaviour
{
    private GameController gameController;
    private Camera mainCamera;
    [SerializeField] LayerMask selectable;
    public static event Action<GameObject> OnShapeSelect;
    public static event Action OnShapeDeselect;
    public List<ShapeController> ShapeInScene;
    private void Start()
    {
        mainCamera = Camera.main;
        gameController = GetComponent<GameController>();
        ShapeInScene.AddRange(GameObject.FindObjectsOfType<ShapeController>());
    }

    public void ClickSelect()
    {
        GameObject current = gameController.currentGameobject;
        if (current != null)
        {
            current.GetComponent<ISelectable>()?.OnDeselect();
            OnShapeDeselect?.Invoke();
        }
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, selectable, QueryTriggerInteraction.Ignore))
        {
            ISelectable seleted = hit.collider.gameObject.GetComponent<ISelectable>();
            if (seleted != null)
            {
                gameController.currentGameobject = hit.collider.gameObject;
                seleted.OnSelect();
                OnShapeSelect?.Invoke(hit.collider.gameObject);
            }
        }
    }
    public void TabSelect(InputAction.CallbackContext context)
    {
        Debug.Log("Exe TabSelect");
        ShapeController current = gameController.currentShape;
        if (current != null)
        {
            Debug.Log("Looping");
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
            Debug.Log("Initiate");
            ShapeController selected = ShapeInScene[0];
            selected.OnSelect();
            OnShapeSelect?.Invoke(selected.gameObject);
        }
    }

    public void Deselect()
    {
        ShapeController current = gameController.currentShape;
        if (current != null)
        {
            current.OnDeselect();
            OnShapeDeselect?.Invoke();
        }
    }
}
 