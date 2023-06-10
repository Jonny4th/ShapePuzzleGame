using System;
using UnityEngine;

namespace Shape.Movement
{
    public interface IMotionManagable
    {
        Vector3 GetMoveDestination(Vector3 direction);
        Quaternion GetRotateDestination(Vector3 axis);
        event Action<bool> IsRotating;
    }
}