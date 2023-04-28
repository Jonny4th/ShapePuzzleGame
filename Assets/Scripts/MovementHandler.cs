using UnityEngine;
using Command;
using System.Collections;
using System;

namespace Shape.Movement
{
    public class MovementHandler : MonoBehaviour
    {
        public event Action<bool> IsBusy;

        public Vector3 GetMoveDestination(Vector3 direction)
        {
            return transform.position + direction;
        }

        internal void MoveTo(Vector3 destination)
        {
            transform.position = destination;
        }

        public Quaternion GetRotateDestination(Vector3 axis)
        {
            return Quaternion.AngleAxis(90, axis) * transform.rotation;
        }

        internal void RotateTo(Quaternion destination)
        {
            StopCoroutine(nameof(Rotate));
            StartCoroutine(nameof(Rotate), destination);
        }

        IEnumerator Rotate(Quaternion target)
        {
            IsBusy?.Invoke(true);
            
            while(Quaternion.Angle(transform.rotation, target) > 0.05f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, target, 0.8f);
                yield return null;
            }

            transform.rotation = target;
            IsBusy?.Invoke(false);
        }
    }

    public abstract class MovementCommend : ICommand
    {
        public MovementHandler Handler { get; protected set; }
        public abstract void Execute();
        public abstract void Undo();
    }

    public class MoveCommand : MovementCommend
    {
        Vector3 origin;
        Vector3 destination;

        public MoveCommand(MovementHandler handler, Vector3 destination)
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

    public class RotateCommand : MovementCommend
    {
        private Quaternion origin;
        private Quaternion destination;

        public RotateCommand(MovementHandler handler, Quaternion destination)
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
