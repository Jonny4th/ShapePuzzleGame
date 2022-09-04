using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardYAxis : MonoBehaviour
{
    GameController gameController;
    public Vector3 moveDirection;
    public PlayerInput control;
    Vector3 inputMovement;
    private void Awake() {
        control = new PlayerInput();
    }
    private void OnEnable() {
        gameController = GetComponent<GameController>();
    }
    private void OnDisable() {
    }
    void Update()
    {
        // if (gameController.currentShape != null)
        // {
        //     if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.PageUp))
        //     {
        //         gameController.currentShape.transform.position += Vector3.up;
        //     }
        //     if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.PageDown))
        //     {
        //         gameController.currentShape.transform.position -= Vector3.up;
        //     }
        //     if (Input.GetKeyDown(KeyCode.A))
        //     {
        //         gameController.currentShape.transform.Rotate(Vector3.up, 90f, Space.World);
        //     }
        //     if (Input.GetKeyDown(KeyCode.D))
        //     {
        //         gameController.currentShape.transform.Rotate(Vector3.up, -90f, Space.World);
        //     }
        //     if (Input.GetKeyDown(KeyCode.E))
        //     {
        //         gameController.currentShape.transform.Rotate(Vector3.right, -90f, Space.World);
        //     }
        //     if (Input.GetKeyDown(KeyCode.Q))
        //     {
        //         gameController.currentShape.transform.Rotate(Vector3.right, 90f, Space.World);
        //     }
        //     if (Input.GetKeyDown(KeyCode.R))
        //     {
        //         gameController.currentShape.transform.Rotate(Vector3.forward, -90f, Space.World);
        //     }
        //     if (Input.GetKeyDown(KeyCode.F))
        //     {
        //         gameController.currentShape.transform.Rotate(Vector3.forward, 90f, Space.World);
        //     }
        //     if (Input.GetKeyDown(KeyCode.UpArrow))
        //     {
        //         gameController.currentShape.transform.position += Vector3.right;
        //     }
        //     if (Input.GetKeyDown(KeyCode.DownArrow))
        //     {
        //         gameController.currentShape.transform.position -= Vector3.right;
        //     }
        //     if (Input.GetKeyDown(KeyCode.LeftArrow))
        //     {
        //         gameController.currentShape.transform.position += Vector3.forward;
        //     }
        //     if (Input.GetKeyDown(KeyCode.RightArrow))
        //     {
        //         gameController.currentShape.transform.position -= Vector3.forward;
        //     }
        // }
    }

    public void OnMovementDown(InputAction.CallbackContext value)
    {
        inputMovement = value.ReadValue<Vector3>();
        ShapeController shape = gameController.currentShape;
        if (shape != null)
        {
            shape.transform.position += inputMovement;
        }
    }
}
