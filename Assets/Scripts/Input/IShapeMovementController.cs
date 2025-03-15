using UnityEngine;

namespace Shape.Inputs
{
    public interface IShapeMovementController
    {
        void OnMoveAxis(Vector3 direction);
        void OnMovePosX();
        void OnMoveNegX();
        void OnMovePosY();
        void OnMoveNegY();
        void OnMovePosZ();
        void OnMoveNegZ();

        void OnRotateAxis(Vector3 rotation);
        void OnRotatePosX();
        void OnRotateNegX();
        void OnRotatePosY();
        void OnRotateNegY();
        void OnRotatePosZ();
        void OnRotateNegZ();
    }
}