using System;
using System.Collections.Generic;
using System.Linq;
using Touch;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using Visualization;

public class SimpleSwipeProcessor : MonoBehaviour, ITouchDetection, IPointsVisualizable
{
    //Touch Detection Events
    public event Action<float> ArcDetected;
    public event Action<Vector2> StraightDetected;

    //Touch Visualization Events
    public event Action<List<Vector2>> LineUpdated;
    public event Action<Vector2> PointUpdated;

    List<Vector2> points = new();

    bool isTouchRegistered = false;

    private Vector2 DragDirection
    {
        get
        {
            var start = points.First();
            var end = points.Last();
            var direction = end - start;
            return direction;
        }
    }

    #region Mouse

    /// <summary>
    /// Attach to Input System [Pointer] > [Press].
    /// </summary>
    /// <param name="context"></param>
    public void OnPointerPressed(InputAction.CallbackContext context)
    {
        var isTouching = context.ReadValue<float>() > 0.5;

        if(isTouching)
        {
            isTouchRegistered = !EventSystem.current.IsPointerOverGameObject();
        }
        else
        {
            if(isTouchRegistered)
            {
                OnTouchEnd();
            }
        }
    }

    /// <summary>
    /// Attach to Input System [Pointer] > [Position].
    /// </summary>
    /// <param name="context"></param>
    public void PointerPosition(InputAction.CallbackContext context)
    {
        if(!isTouchRegistered) return;
        var pos = context.ReadValue<Vector2>();
        UpdateLine(pos);
    }

    #endregion

    #region Touch
    /// <summary>
    /// Attach to Input System [TouchScreen] > [Position].
    /// </summary>
    /// <param name="context"></param>
    public void OnTouch(InputAction.CallbackContext context)
    {
        var touch = context.ReadValue<TouchState>();

        if(touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
        {
            isTouchRegistered = !EventSystem.current.IsPointerOverGameObject(touch.touchId);
            Debug.Log($"Begin : touch = {isTouchRegistered}");
        }

        if(!isTouchRegistered) return;

        if(touch.phase == UnityEngine.InputSystem.TouchPhase.Ended)
        {
            Debug.Log($"End");
            OnTouchEnd();
        }

        if(touch.delta == Vector2.zero) return;

        var pos = touch.position;
        UpdateLine(pos);
    }

    #endregion

    #region Aux
    private void UpdateLine(Vector2 pos)
    {
        points.Add(pos);
        LineUpdated?.Invoke(points);
    }

    private void ResetParameters()
    {
        points.Clear();
        isTouchRegistered = false;
    }
    #endregion

    #region Logics

    private void OnTouchEnd()
    {
        StraightDetected?.Invoke(DragDirection);
        ResetParameters();
    }
    #endregion
}
