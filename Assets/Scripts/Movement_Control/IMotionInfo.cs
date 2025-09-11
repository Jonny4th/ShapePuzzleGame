using System;
using UnityEngine;

namespace Shape.Movement
{
    public interface IMotionInfo
    {
        event Action<bool> IsRotating;

        bool isRotating { get; }

        Vector3 GetMoveDestination(Vector3 direction);
        Quaternion GetRotateDestination(Vector3 axis);
    }
}