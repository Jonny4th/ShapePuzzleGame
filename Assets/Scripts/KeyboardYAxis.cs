using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardYAxis : MonoBehaviour
{
    GameController gameController;
    Vector3 inputMovement;
    private void OnEnable() {
        gameController = GetComponent<GameController>();
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
