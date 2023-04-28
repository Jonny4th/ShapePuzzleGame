using Command;
using Shape.Movement;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementInfo
{
    public MovementCommend movementHandler;
}

public class ShapeMovementManager : MonoBehaviour
{
    [SerializeField] float gridSize;
    [SerializeField] bool gridStartAtZero;
    [SerializeField] Vector3 minimumLimit;
    [SerializeField] Vector3 maximumLimit;
    float minX { get => Math.Min(minimumLimit.x, maximumLimit.x); }
    float maxX { get => Math.Max(minimumLimit.x, maximumLimit.x); }
    float minY { get => Math.Min(minimumLimit.y, maximumLimit.y); }
    float maxY { get => Math.Max(minimumLimit.y, maximumLimit.y); }
    float minZ { get => Math.Min(minimumLimit.z, maximumLimit.z); }
    float maxZ { get => Math.Max(minimumLimit.z, maximumLimit.z); }

    bool isBusy;

    public MovementHandler CurrentMovementHandler { get; private set; }

    public static event Action<MovementInfo> Moved;

    private void OnEnable()
    {
        ObjectSelect.MovementHandlerSelected += AssignSelectedShape;
        ObjectSelect.ShapeDeselected += ClearSelectedShape;
    }

    private void OnDisable()
    {
        ObjectSelect.MovementHandlerSelected -= AssignSelectedShape;
        ObjectSelect.ShapeDeselected -= ClearSelectedShape;
    }

    private void AssignSelectedShape(MovementHandler movementHandler)
    {
        CurrentMovementHandler = movementHandler;
        CurrentMovementHandler.IsBusy += SetBusy;
    }

    private void ClearSelectedShape()
    {
        CurrentMovementHandler.IsBusy -= SetBusy;
        CurrentMovementHandler = null;
    }

    private void SetBusy(bool value)
    {
        isBusy = value;
    }

    private void Move(Vector3 direction)
    {
        var destination = AlignToGrid(CurrentMovementHandler.GetMoveDestination(direction));

        if(IsAtLimit(destination)) return;

        CommandManager.Instance.AddCommand(new MoveCommand(CurrentMovementHandler, destination));
    }

    private void Rotate(Vector3 axis)
    {
        if(isBusy) return;

        var destination = CurrentMovementHandler.GetRotateDestination(axis);
        CommandManager.Instance.AddCommand(new RotateCommand(CurrentMovementHandler, destination));
    }

    public void OnMoveAxis(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<Vector3>();
        if(direction == Vector3.zero) return;
        if(!context.performed) return;
        if(CurrentMovementHandler == null) return;

        Move(direction);
    }

    public void OnRotateAxis(InputAction.CallbackContext context)
    {
        if(isBusy) return;
        if(!context.performed) return;
        if(CurrentMovementHandler == null) return;

        var rotation = context.ReadValue<Vector3>();

        if(rotation == Vector3.zero) return;

        Rotate(rotation);
    }

    public void OnMovePosX(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        Move(Vector3.right);
    }

    public void OnMoveNegX(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        Move(Vector3.left);
    }

    public void OnMovePosZ(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        Move(Vector3.forward);
    }

    public void OnMoveNegZ(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        Move(Vector3.back);
    }

    public void OnMovePosY(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        Move(Vector3.up);
    }

    public void OnMoveNegY(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        Move(Vector3.down);
    }

    Vector3 AlignToGrid(Vector3 pos)
    {
        var x = AlignToGrid(pos.x);
        var y = AlignToGrid(pos.y);
        var z = AlignToGrid(pos.z);
        pos = new Vector3(x, y, z);
        return pos;
    }

    float AlignToGrid(float value)
    {
        float offset = 0f;
        if(gridStartAtZero)
        {
            offset = 0.5f;
        }
        return Mathf.RoundToInt(value / gridSize - offset) * gridSize + offset;
    }

    private bool IsAtLimit(Vector3 pos)
    {
        bool isLimit;

        if(pos.x < minX || pos.x > maxX) { isLimit = true; }
        else if(pos.y < minY || pos.y > maxY) { isLimit = true; }
        else if(pos.z < minZ || pos.z > maxZ) { isLimit = true; }
        else { isLimit = false; }

        return isLimit;
    }
}


