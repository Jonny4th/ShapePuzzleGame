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
        // Screen space angle devider and +1 increment.
        // if(Input.GetMouseButtonDown(0))
        // {
        //     original = Input.mousePosition;
        // }

        // if (Input.GetMouseButton(0))
        // {
        //     shape = gameController.currentShape?.transform.parent.gameObject;
        //     if (shape != null)
        //     {
        //         Vector3 newPoint = Input.mousePosition;
        //         Vector3 offset = newPoint - original;
        //         if (offset.sqrMagnitude >= threshold)
        //         {
        //             var angle = Mathf.Atan2(offset.y,offset.x) * Mathf.Rad2Deg;
        //             Debug.Log(angle);
        //             //bools
        //             bool xp = (angle > 0 && angle < 60);
        //             bool yp = (angle > 60 && angle < 120);
        //             bool zp = (angle > 120 && angle < 180);
        //             bool zm = (angle > -60 && angle < 0);
        //             bool ym = (angle > -120 && angle < -60);
        //             bool xm = (angle > -180 && angle < -120);
        //             if(zp) shape.transform.position += Vector3.forward;
        //             else if(zm) shape.transform.position += Vector3.back;
        //             else if(yp) shape.transform.position += Vector3.up;
        //             else if(ym) shape.transform.position += Vector3.down;
        //             else if(xp) shape.transform.position += Vector3.right;
        //             else if(xm) shape.transform.position += Vector3.left;
        //             original = Input.mousePosition;
        //         }
        //     }
        // }

        // Manually constrain other axis.
        // if (Input.GetMouseButton(0))
        // {
        //     shape = gameController.currentShape?.transform.parent.gameObject;
        //     if (shape!=null)
        //     {
        //         var newPos = shape.transform.position;
        //         newPos.x = mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
        //         shape.transform.position = newPos;
        //     }
        // }

        // Raycast
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

    // enum direction
    // {x,y,z}
    // private void SingleAxisTranslate(direction d)
    // {
    //     var newPos = shape.transform.position;
    //     switch (d)
    //     {
    //         case(direction.x):
    //         {
    //             newPos.x = mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
    //             break;
    //         }
    //         case(direction.y):
    //         {
    //             newPos.y = mainCamera.ScreenToWorldPoint(Input.mousePosition).y;
    //             break;
    //         }
    //         case(direction.z):
    //         {
    //             newPos.z = mainCamera.ScreenToWorldPoint(Input.mousePosition).z;
    //             break;
    //         }
    //         default:{break;}
    //     }
    //     shape.transform.position = newPos;
    // }
}
