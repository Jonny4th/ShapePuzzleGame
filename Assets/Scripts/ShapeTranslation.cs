using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeTranslation : MonoBehaviour
{
    GameObject shape;
    GameController gameController;
    Camera mainCamera;
    public Vector3 original;
    [SerializeField] float threshold;
    private void Awake() {
        gameController = GetComponent<GameController>();
        mainCamera = Camera.main;
    }


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            original = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            shape = gameController.currentShape?.transform.parent.gameObject;
            if (shape != null)
            {
                Vector3 newPoint = Input.mousePosition;
                Vector3 offset = newPoint - original;
                if (offset.sqrMagnitude >= threshold)
                {
                    var angle = Mathf.Atan2(offset.y,offset.x) * Mathf.Rad2Deg;
                    Debug.Log(angle);
                    //bools
                    bool xp = (angle > 0 && angle < 60);
                    bool yp = (angle > 60 && angle < 120);
                    bool zp = (angle > 120 && angle < 180);
                    bool zm = (angle > -60 && angle < 0);
                    bool ym = (angle > -120 && angle < -60);
                    bool xm = (angle > -180 && angle < -120);
                    if(zp) shape.transform.position += Vector3.forward;
                    else if(zm) shape.transform.position += Vector3.back;
                    else if(yp) shape.transform.position += Vector3.up;
                    else if(ym) shape.transform.position += Vector3.down;
                    else if(xp) shape.transform.position += Vector3.right;
                    else if(xm) shape.transform.position += Vector3.left;
                    original = Input.mousePosition;
                }
            }
        }
    }
}
