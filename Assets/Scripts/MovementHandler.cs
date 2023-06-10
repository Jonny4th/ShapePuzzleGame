using UnityEngine;
using Command;
using System.Collections;
using System;

namespace Shape.Movement
{
    public class MovementHandler : MonoBehaviour, IMotionManagable, IMotionCommandHandler
    {
        public event Action<bool> IsRotating;

        public Vector3 GetMoveDestination(Vector3 direction)
        {
            return transform.position + direction;
        }

        public Quaternion GetRotateDestination(Vector3 axis)
        {
            return Quaternion.AngleAxis(90, axis) * transform.rotation;
        }

        public void MoveTo(Vector3 destination)
        {
            transform.position = destination;
        }

        public void RotateTo(Quaternion destination)
        {
            StopCoroutine(nameof(Rotate));
            StartCoroutine(nameof(Rotate), destination);
        }

        IEnumerator Rotate(Quaternion target)
        {
            IsRotating?.Invoke(true);
            
            while(Quaternion.Angle(transform.rotation, target) > 0.05f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, target, 0.8f);
                yield return null;
            }

            transform.rotation = target;
            IsRotating?.Invoke(false);
        }
    }

    public abstract class MovementCommand : ICommand
    {
        public IMotionCommandHandler Handler { get; protected set; }
        public abstract void Execute();
        public abstract void Undo();
    }

    public class MoveCommand : MovementCommand
    {
        Vector3 origin;
        Vector3 destination;

        public MoveCommand(IMotionCommandHandler handler, Vector3 destination)
        {
            Handler = handler;
            origin = handler.transform.position;
            this.destination = destination;
        }

        public override void Execute()
        {
            Handler.MoveTo(destination);
        }

        public override void Undo()
        {
            Handler.MoveTo(origin);
        }
    }

    public class RotateCommand : MovementCommand
    {
        private Quaternion origin;
        private Quaternion destination;

        public RotateCommand(IMotionCommandHandler handler, Quaternion destination)
        {
            Handler = handler;
            origin = handler.transform.rotation;
            this.destination = destination;
        }

        public override void Execute()
        {
            Handler.RotateTo(destination);
        }

        public override void Undo()
        {
            Handler.RotateTo(origin);
        }
    }
}
