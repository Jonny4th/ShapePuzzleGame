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
    [SerializeField] Vector3 minimumLimit;
    [SerializeField] Vector3 maximumLimit;
    float minX { get => Math.Min(minimumLimit.x,maximumLimit.x); }
    float maxX { get => Math.Max(minimumLimit.x,maximumLimit.x); }
    float minY { get => Math.Min(minimumLimit.y,maximumLimit.y); }
    float maxY { get => Math.Max(minimumLimit.y,maximumLimit.y); }
    float minZ { get => Math.Min(minimumLimit.z,maximumLimit.z); }
    float maxZ { get => Math.Max(minimumLimit.z,maximumLimit.z); }


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
        Vector3 input = context.ReadValue<Vector3>();
        if (context.performed && input.sqrMagnitude > 0.1f)
        {
            if (shape != null)
            {
                MoveShape(input);
                OnMove();
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
                Debug.Log(inputAxis);
                RotateShape(inputAxis);
                OnMove();
            }
        }
    }

    public void OnYRotationKeyDown(InputAction.CallbackContext context)
    {
        float inputValue = context.ReadValue<float>();
        Debug.Log(inputValue);
    }

    public void OnZRotationKeyDown(InputAction.CallbackContext context)
    {
        float inputValue = context.ReadValue<float>();
        Debug.Log(inputValue);
    }

    private void MoveShape(Vector3 direction)
    {
        Vector3 newPos = shapeTransform.position + direction;
        if (IsAtLimit(newPos)) { return; }
        shapeTransform.position = AlignToGrid(newPos);
    }


    private void RotateShape(Vector3 axis)
    {
        Quaternion targetAngle = Quaternion.AngleAxis(90, axis) * shapeTransform.rotation;
        StartCoroutine(nameof(Rotate), targetAngle);
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
        //OnMove();
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

    private bool IsAtLimit(Vector3 pos)
    {
        bool isLimit;

        if (pos.x < minX || pos.x > maxX) { isLimit = true; }
        else if (pos.y < minY || pos.y > maxY) { isLimit = true; }
        else if (pos.z < minZ || pos.z > maxZ) { isLimit = true; }
        else { isLimit = false; }

        return isLimit;
    }
}
