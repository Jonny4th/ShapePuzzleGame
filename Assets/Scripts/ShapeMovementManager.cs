using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class ShapeMovementManager : MonoBehaviour
{
    [SerializeField] float gridSize;
    [SerializeField] bool gridStartAtZero;
    public static event Action OnTranslate;
    public static event Action OnRotate;
    [SerializeField] ShapeController shape;
    bool isRotating;
    private void OnEnable() {
        ObjectSelect.OnShapeSelect += AssignSelectedShape;
        ObjectSelect.OnShapeDeselect += ClearSelectedShape;
    }
    private void OnDisable() {
        ObjectSelect.OnShapeSelect -= AssignSelectedShape;
        ObjectSelect.OnShapeDeselect -= ClearSelectedShape;
    }

    private void AssignSelectedShape(ShapeController s)
    {
        shape = s;
    }
    private void ClearSelectedShape()
    {
        shape = null;
    }


    public void OnMovementKeyDown(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            Vector3 inputMovement = value.ReadValue<Vector3>();
            if (shape != null)
            {
                OnTranslate?.Invoke();
                MoveShape(inputMovement);
            }
        }
    }

    private void MoveShape(Vector3 direction)
    {
        shape.transform.position += direction;
        shape.transform.position = AlignToGrid(shape.transform.position);
    }

    public void OnRotationKeyDown(InputAction.CallbackContext value)
    {
        if (value.performed && !isRotating)
        {
            Vector3 inputAxis = value.ReadValue<Vector3>();
            if (shape != null)
            {
                OnRotate?.Invoke();
                RotateShape(inputAxis);
            }
        }
    }

    private void RotateShape(Vector3 axis)
    {
        Quaternion targetAngle = Quaternion.AngleAxis(90, axis) * shape.transform.rotation;
        StartCoroutine(nameof(Rotate), targetAngle);
    }
    IEnumerator Rotate(Quaternion target)
    {
        while(Quaternion.Angle(shape.transform.rotation, target) > 0.05f)
        {
            shape.transform.rotation = Quaternion.Slerp(shape.transform.rotation, target, 0.9f);
            isRotating = true;
            yield return null;
        }
        shape.transform.rotation = target;
        isRotating = false;
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
