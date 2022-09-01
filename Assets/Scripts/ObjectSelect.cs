using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectSelect : MonoBehaviour
{
    private GameController gameController;
    private Camera mainCamera;
    [SerializeField] LayerMask selectable;
    public static event Action<GameObject> OnShapeSelect;
    public static event Action OnShapeDeselect;

    private void Start()
    {
        mainCamera = Camera.main;
        gameController = GetComponent<GameController>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
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
                //gameController.currentGameobject = null;
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ShapeController current = gameController.currentShape;
            if (current != null)
            {
                var index = gameController.ShapeInScene.IndexOf(current);
                current.OnDeselect();
                OnShapeDeselect?.Invoke();
                index = (index+1) % gameController.ShapeInScene.Count;
                ShapeController selected = gameController.ShapeInScene[index];
                selected.OnSelect();
                OnShapeSelect?.Invoke(selected.gameObject);
            }
            else
            {
                ShapeController selected = gameController.ShapeInScene[0];
                selected.OnSelect();
                OnShapeSelect?.Invoke(selected.gameObject);
            }
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            ShapeController current = gameController.currentShape;
            if (current != null)
            {
                current.OnDeselect();
                OnShapeDeselect?.Invoke();
            }
        }
        // gameController.currentGameobject = this.gameObject;
    }
}
 