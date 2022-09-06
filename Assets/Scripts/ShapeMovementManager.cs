using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class ShapeMovementManager : MonoBehaviour
{
    GameController gameController;
    Vector3 inputMovement;
    [SerializeField] float gridSize;
    [SerializeField] bool gridStartAtZero;
    public event Action OnTranslate;
    public event Action OnRotate;
    
    private void OnEnable() {
        gameController = GetComponent<GameController>();
    }

    public void OnMovementKeyDown(InputAction.CallbackContext value)
    {
        inputMovement = value.ReadValue<Vector3>();
        ShapeController shape = gameController.currentShape;
        if (shape != null)
        {
            OnTranslate?.Invoke();
            shape.transform.position += inputMovement;
            shape.transform.position = AlignToGrid(shape.transform.position);
        }
    }

    Vector3 AlignToGrid(Vector3 pos)
    {
        var x = AlignToGrid(pos.x);
        var y = AlignToGrid(pos.y);
        var z = AlignToGrid(pos.z);
        pos = new Vector3(x,y,z);
        return pos;
    }

    float AlignToGrid(float value)
    {
        float offset = 0f;
        if (gridStartAtZero)
        {
            offset = 0.5f;
        }
        return Mathf.RoundToInt(value/gridSize - offset)*gridSize + offset;
    }
}
