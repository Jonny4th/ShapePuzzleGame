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

        public void MoveTo(Vector3 destination)
        {
            transform.position = destination;
        }

        public Quaternion GetRotateDestination(Vector3 axis)
        {
            return Quaternion.AngleAxis(90, axis) * transform.rotation;
        }

        public void RotateTo(Quaternion destination)
        {
            StartCoroutine(nameof(Rotate), destination);
        }

        IEnumerator Rotate(Quaternion target)
        {
            IsBusy?.Invoke(true);
            while(Quaternion.Angle(transform.rotation, target) > 0.05f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, target, 0.9f);
                yield return null;
            }
            transform.rotation = target;
            IsBusy?.Invoke(false);
        }
    }

    public class MoveCommand : ICommand
    {
        MovementHandler handler;
        Vector3 origin;
        Vector3 destination;

        public MoveCommand(MovementHandler handler, Vector3 destination)
        {
            this.handler = handler;
            origin = handler.transform.position;
            this.destination = destination;
        }

        public void Execute()
        {
            handler.MoveTo(destination);
        }

        public void Undo()
        {
            handler.MoveTo(origin);
        }
    }

    public class RotateCommand : ICommand
    {
        MovementHandler handler;
        Quaternion origin;
        Quaternion destination;

        public RotateCommand(MovementHandler handler, Quaternion destination)
        {
            this.handler = handler;
            origin = handler.transform.rotation;
            this.destination = destination;
        }

        public void Execute()
        {
            handler.RotateTo(destination);
        }

        public void Undo()
        {
            handler.RotateTo(origin);
        }
    }
}
