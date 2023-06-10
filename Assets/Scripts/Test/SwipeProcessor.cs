using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class SwipeProcessor : MonoBehaviour, ITouchDetection, IPointsVisualizable
{
    //Touch Detection Events
    public event Action<float> ArcDetected;
    public event Action<Vector2> StraightDetected;

    //Touch Visualization Events
    public event Action<List<Vector2>> LineUpdated;
    public event Action<Vector2> PointUpdated;

    List<Vector2> points = new();

    bool m_IsPress = false;
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
            Debug.Log(Vector2.SqrMagnitude(direction));
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
        var press = context.ReadValue<float>();
        SetIsPressing(press > 0.5f);
    }

    /// <summary>
    /// Attach to Input System [Pointer] > [Position].
    /// </summary>
    /// <param name="context"></param>
    public void PointerPosition(InputAction.CallbackContext context)
    {
        if(!m_IsPress) return;
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
        SetIsPressing(touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled);
        Debug.Log(m_IsPress);
        if(!m_IsPress) return;

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

    private void SetIsPressing(bool value)
    {
        m_IsPress = value;
        if(!m_IsPress)
        {
            DecideDragEvent();
            ResetParameters();
        }
    }

    private void ResetParameters()
    {
        points.Clear();
        firstSign = 0;
        m_IsArc = true;
        m_IsStraight = true;
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
    #endregion
}
