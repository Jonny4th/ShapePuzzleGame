using System;
using System.Collections.Generic;
using System.Linq;
using Touch;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using Visualization;

public class SwipeProcessor : MonoBehaviour, ITouchDetection, IPointsVisualizable
{
    //Touch Detection Events
    public event Action<float> ArcDetected;
    public event Action<Vector2> StraightDetected;

    //Touch Visualization Events
    public event Action<List<Vector2>> LineUpdated;
    public event Action<Vector2> PointUpdated;

    List<Vector2> points = new();

    bool isTouchRegistered = false;

    bool m_IsArc = true;
    bool m_IsStraight = true;
    float firstSign = 0;

    [SerializeField]
    private float angleLimit;
    [SerializeField]
    private float minSrtMagnitude;


    private float ArcAngle
    {
        get
        {
            if(points.Count < 2) return 0;
            var start = points.First();
            var second = points[1];

            var end = points.Last();
            var nextToLast = points[^2];

            var vectorA = second - start;
            var vectorB = end - nextToLast;

            return Vector2.SignedAngle(vectorA, vectorB);
        }
    }

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
            isTouchRegistered = !EventSystem.current.IsPointerOverGameObject();
        }

        if(!isTouchRegistered) return;
        
        if(touch.phase == UnityEngine.InputSystem.TouchPhase.Ended)
        {
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
        IsStraight();
        IsArc();
    }

    private void ResetParameters()
    {
        points.Clear();
        firstSign = 0;
        m_IsArc = true;
        m_IsStraight = true;
        isTouchRegistered = false;
    }
    #endregion

    #region Logics
    private void IsArc()
    {
        if(!m_IsArc) return;
        if(points.Count < 3) return;

        var direction = Mathf.Sign(ArcAngle);

        if(points.Count == 3)
        {
            firstSign = direction;
            return;
        }

        m_IsArc = firstSign == direction;
    }

    private void IsStraight()
    {
        if(!m_IsStraight) return;

        if(points.Count < 3) return;
        m_IsStraight = Mathf.Abs(ArcAngle) < angleLimit;
    }

    private void DecideDragEvent()
    {
        if(Vector2.SqrMagnitude(DragDirection) < minSrtMagnitude) return;
        if(m_IsStraight)
        {
            StraightDetected?.Invoke(DragDirection);
        }
        else if(m_IsArc)
        {
            ArcDetected?.Invoke(firstSign);
        }
    }

    private void OnTouchEnd()
    {
        DecideDragEvent();
        ResetParameters();
    }
    #endregion
}
