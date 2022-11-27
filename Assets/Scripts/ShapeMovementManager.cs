using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEditor;

public class OnMovementInfo
{
    public GameObject shape;
}

public class ShapeMovementManager : MonoBehaviour
{
    [SerializeField] float gridSize;
    [SerializeField] bool gridStartAtZero;

    ShapeSelectionController shape;
    Transform shapeTransform;
    bool isRotating;

    public static event Action<OnMovementInfo> Moved;

    private void OnEnable() {
        ObjectSelect.ShapeSelected += AssignSelectedShape;
        ObjectSelect.ShapeDeselected += ClearSelectedShape;
    }
    private void OnDisable() {
        ObjectSelect.ShapeSelected -= AssignSelectedShape;
        ObjectSelect.ShapeDeselected -= ClearSelectedShape;
    }

    private void AssignSelectedShape(ShapeSelectionController s)
    {
        shape = s;
        shapeTransform = shape.transform;
    }
    private void ClearSelectedShape()
    {
        shape = null;
    }


    public void OnMovementKeyDown(InputAction.CallbackContext context)
    {
        if (context.performed && context.ReadValue<Vector3>().sqrMagnitude > 0.1f)
        {
            Vector3 inputMovement = context.ReadValue<Vector3>();
            if (shape != null)
            {
                MoveShape(inputMovement);
            }
        }
    }
    public void OnRotationKeyDown(InputAction.CallbackContext context)
    {
        if (context.performed && !isRotating)
        {
            Vector3 inputAxis = context.ReadValue<Vector3>();
            if (shape != null)
            {
                RotateShape(inputAxis);
            }
        }
    }

    private void MoveShape(Vector3 direction)
    {
        shapeTransform.position += direction;
        shapeTransform.position = AlignToGrid(shapeTransform.position);
        OnMove();
    }


    private void RotateShape(Vector3 axis)
    {
        Quaternion targetAngle = Quaternion.AngleAxis(90, axis) * shapeTransform.rotation;
        StartCoroutine(nameof(Rotate), targetAngle);
        OnMove();
    }
    IEnumerator Rotate(Quaternion target)
    {
        while(Quaternion.Angle(shape.transform.rotation, target) > 0.05f)
        {
            shapeTransform.rotation = Quaternion.Slerp(shapeTransform.rotation, target, 0.9f);
            isRotating = true;
            yield return null;
        }
        shapeTransform.rotation = target;
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

    private void OnMove()
    {
        OnMovementInfo info = new()
        {
            shape = shape.gameObject
        };
        Moved?.Invoke(info);
    }
}
