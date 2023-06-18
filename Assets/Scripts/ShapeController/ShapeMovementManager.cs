using Command;
using Shape.Movement;
using System;
using Touch;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class ShapeMovementManager : MonoBehaviour
{
    [SerializeField]
    float gridSize;

    [SerializeField]
    bool gridStartAtZero;

    [SerializeField]
    Vector3 minimumLimit;

    [SerializeField]
    Vector3 maximumLimit;

    float minX { get => Math.Min(minimumLimit.x, maximumLimit.x); }
    float maxX { get => Math.Max(minimumLimit.x, maximumLimit.x); }
    float minY { get => Math.Min(minimumLimit.y, maximumLimit.y); }
    float maxY { get => Math.Max(minimumLimit.y, maximumLimit.y); }
    float minZ { get => Math.Min(minimumLimit.z, maximumLimit.z); }
    float maxZ { get => Math.Max(minimumLimit.z, maximumLimit.z); }

    bool isBusy;

    private IMotionManagable CurrentMovementHandler;

    [SerializeField]
    UnityEngine.Object _swipeProcessor;

    ITouchDetection swipeProcessor => _swipeProcessor as ITouchDetection;

    public static event Action<MovementInfo> Moved;

    private void OnEnable()
    {
        ObjectSelect.MovementHandlerSelected += AssignSelectedShape;
        ObjectSelect.ShapeDeselected += ClearSelectedShape;
        swipeProcessor.ArcDetected += OnArcDetected;
        swipeProcessor.StraightDetected += OnStraightDetect;
    }

    private void OnDisable()
    {
        ObjectSelect.MovementHandlerSelected -= AssignSelectedShape;
        ObjectSelect.ShapeDeselected -= ClearSelectedShape;
        swipeProcessor.ArcDetected -= OnArcDetected;
        swipeProcessor.StraightDetected -= OnStraightDetect;
    }

    private void ProcessMove(Vector3 direction)
    {
        if(CurrentMovementHandler == null) return;

        var destination = AlignToGrid(CurrentMovementHandler.GetMoveDestination(direction));

        if(IsAtLimit(destination)) return;

        var command = new MoveCommand(CurrentMovementHandler as IMotionCommandHandler, destination);
        CommandManager.Instance.AddCommand(command);
        Moved?.Invoke(new MovementInfo(command));
    }

    private void ProcessRotate(Vector3 axis)
    {
        if(CurrentMovementHandler == null) return;
        if(isBusy) return;
        if(axis == Vector3.zero) return;

        var destination = CurrentMovementHandler.GetRotateDestination(axis);

        var command = new RotateCommand(CurrentMovementHandler as IMotionCommandHandler, destination);
        CommandManager.Instance.AddCommand(command);
        Moved?.Invoke(new MovementInfo(command));
    }

    public void OnMoveAxis(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        var direction = context.ReadValue<Vector3>();
        if(direction == Vector3.zero) return;

        ProcessMove(direction);
    }

    public void OnRotateAxis(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        var rotation = context.ReadValue<Vector3>();
        if(rotation == Vector3.zero) return;

        ProcessRotate(rotation);
    }

    public void OnRotateAxis(Vector3 rotation)
    {
        if(rotation == Vector3.zero) return;

        ProcessRotate(rotation);
    }

    #region Separate Move Input Actions 
    public void OnMovePosX(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        ProcessMove(Vector3.right);
    }

    public void OnMoveNegX(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        ProcessMove(Vector3.left);
    }

    public void OnMovePosZ(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        ProcessMove(Vector3.forward);
    }

    public void OnMoveNegZ(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        ProcessMove(Vector3.back);
    }

    public void OnMovePosY(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        ProcessMove(Vector3.up);
    }

    public void OnMoveNegY(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        ProcessMove(Vector3.down);
    }
    #endregion

    #region Separate Rotation Input Actions
    public void OnRotatePosX(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        ProcessRotate(Vector3.right);
    }

    public void OnRotateNegX(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        ProcessRotate(Vector3.left);
    }

    public void OnRotatePosZ(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        ProcessRotate(Vector3.forward);
    }

    public void OnRotateNegZ(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        ProcessRotate(Vector3.back);
    }

    public void OnRotatePosY(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        ProcessRotate(Vector3.up);
    }

    public void OnRotateNegY(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        ProcessRotate(Vector3.down);
    }
    #endregion

    #region Spiwe Action Handler

    private void OnStraightDetect(Vector2 direction)
    {
        ProcessRotate(HandleRotateTouch2Axis(direction));
    }

    private void OnArcDetected(float direction)
    {
        ProcessRotate(Vector3.right * direction);
    }

    #endregion

    #region Touch Screen Handler
    //deplicated
    public void OnRotateTouch(InputAction.CallbackContext context)
    {
        var touch = context.ReadValue<TouchState>();
        var start = touch.startPosition;

        if(touch.phase != UnityEngine.InputSystem.TouchPhase.Ended) return;
        if(touch.delta != Vector2.zero) return;
        //Ended phase sometimes trigger twice: with none zero vector and with zero vector.
        //This happens when you swipe fast.

        var vector = touch.position - start;
        if(vector.sqrMagnitude < 10000) return;
        ProcessRotate(HandleRotateTouch2Axis(vector));
    }

    private Vector3 HandleRotateTouch3Axis(Vector2 vector)
    {
        var axis = Vector3.zero;
        var angle = Vector2.Angle(Vector2.right, vector);

        if(angle < 10f) axis = Vector3.down;
        else if(angle > 170f) axis = Vector3.up;
        else if(angle > 10f && angle < 70f)
        {
            if(vector.y > 0) axis = Vector3.back;
            else axis = Vector3.left;
        }
        else if(angle > 110f && angle < 170f)
        {
            if(vector.y > 0) axis = Vector3.right;
            else axis = Vector3.forward;
        }

        return axis;
    }

    private Vector3 HandleRotateTouch2Axis(Vector2 vector)
    {
        var axis = Vector3.zero;
        var angle = Vector2.Angle(Vector2.right, vector);

        float angleMargin = 20f;

        if(angle < angleMargin) axis = Vector3.down;
        else if(angle > (180f - angleMargin)) axis = Vector3.up;
        else if(angle > 90f - angleMargin && angle < 90f + angleMargin)
        {
            if(vector.y > 0) axis = Vector3.back;
            else axis = Vector3.forward;
        }

        return axis;
    }

    #endregion

    #region Auxilary Fucntions

    private void AssignSelectedShape(IMotionManagable movementHandler)
    {
        CurrentMovementHandler = movementHandler;
        CurrentMovementHandler.IsRotating += SetBusy;
    }

    private void ClearSelectedShape()
    {
        CurrentMovementHandler.IsRotating -= SetBusy;
        CurrentMovementHandler = null;
    }

    private void SetBusy(bool value)
    {
        isBusy = value;
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
    #endregion
}

public class MovementInfo
{
    public MovementInfo(MovementCommand movementCommand)
    {
        command = movementCommand;
    }

    public MovementCommand command { get; }
}