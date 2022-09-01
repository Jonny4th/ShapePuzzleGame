using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeTranslation : MonoBehaviour
{
    [SerializeField] ShapeController shape;
    GameController gameController;
    Camera mainCamera;
    public Vector3 original;
    [SerializeField] GameObject plane;
    [SerializeField] float threshold;
    [SerializeField] LayerMask zxPlane;
    [SerializeField] LayerMask yPlane;

    private void Awake() {
        gameController = GetComponent<GameController>();
        mainCamera = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            shape = gameController.currentShape;
            plane = shape?.xzplane;
            Vector3 newPos;
            Vector3 offset;
            if (plane!=null)
            {
                plane.SetActive(true); Debug.Log("plane active");
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 20f, LayerMask.GetMask("xzPlane")))
                {
                    Debug.Log(hit.point);
                    newPos = shape.transform.position;
                    offset = newPos - hit.point;
                    newPos.x = hit.point.x;
                    shape.transform.position = newPos;
                }
            }
        } 
        else if (Input.GetMouseButtonUp(0))
        {
            plane.SetActive(false);
            plane = null;
            shape = null;
        }
    }
}
