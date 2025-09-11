using UnityEngine;

namespace Shape.Movement
{
    public interface IMotionCommandHandler
    {
        Transform transform { get; }
        void MoveTo(Vector3 destination);
        void RotateTo(Quaternion destination);
    }
}