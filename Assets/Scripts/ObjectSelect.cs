using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelect : MonoBehaviour
{
    private GameController gameController;
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
        gameController = GetComponent<GameController>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 50f))
            {
                GameObject current = gameController.currentGameobject;
                if (current != null) current.GetComponent<ISelectable>().OnDeselect();
                ISelectable block = hit.collider.gameObject.GetComponent<ISelectable>();
                if (block != null)
                {
                    gameController.currentGameobject = hit.collider.gameObject;
                    block.OnSelect();
                }
                else gameController.currentGameobject = null;
            }
        }
        // gameController.currentGameobject = this.gameObject;
    }
}
