using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectSelect : MonoBehaviour
{
    private GameController gameController;
    private Camera mainCamera;
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
                current.GetComponent<ISelectable>().OnDeselect();
                OnShapeDeselect?.Invoke();
            }
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 50f))
            {
                ISelectable block = hit.collider.gameObject.GetComponent<ISelectable>();
                if (block != null)
                {
                    gameController.currentGameobject = hit.collider.gameObject;
                    block.OnSelect();
                    OnShapeSelect?.Invoke(hit.collider.gameObject);
                }
                //gameController.currentGameobject = null;
            }
        }
        // gameController.currentGameobject = this.gameObject;
    }
}
