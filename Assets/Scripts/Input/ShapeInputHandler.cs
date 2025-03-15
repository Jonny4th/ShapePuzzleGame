using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Shape.Inputs
{
    public class ShapeInputHandler : MonoBehaviour, IMovementInputHandler
    {
        private IShapeMovementController m_MovementController;

        bool toggleRotationOn = false;
        public UnityEvent<bool> OnRotationToggleChanged;

        public void SetShapeMovementController(IShapeMovementController controller)
        {
            m_MovementController = controller;
        }

        #region Toggle
        public void ToggleRotateControl(InputAction.CallbackContext context)
        {
            if(context.started) return;

            toggleRotationOn = context.performed;
            OnRotationToggleChanged.Invoke(toggleRotationOn);

            Debug.Log("Toggle phase: " + toggleRotationOn);
        }

        public void ToggleRotationOn()
        {
            toggleRotationOn = true;
            OnRotationToggleChanged.Invoke(true);
            Debug.Log("Toggle phase: " + toggleRotationOn);
        }
        public void ToggleRotationOff()
        {
            toggleRotationOn = false;
            OnRotationToggleChanged.Invoke(false);
            Debug.Log("Toggle phase: " + toggleRotationOn);
        }
        #endregion

        #region Translation
        public void OnMoveAxis(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
            Vector3 direction = context.ReadValue<Vector3>();
            if(toggleRotationOn) m_MovementController.OnRotateAxis(direction);
            else m_MovementController.OnMoveAxis(direction);
        }

        public void OnMovePosX(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
            m_MovementController.OnMovePosX();
        }

        public void OnMovePosY(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
            m_MovementController.OnMovePosY();
        }

        public void OnMovePosZ(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
            m_MovementController.OnMovePosZ();
        }

        public void OnMoveNegX(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
            m_MovementController.OnRotateNegX();
        }

        public void OnMoveNegY(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
            m_MovementController.OnRotateNegY();
        }

        public void OnMoveNegZ(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
            m_MovementController.OnRotateNegZ();
        }
        #endregion

        #region Rotation
        public void OnRotateAxis(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
            Vector3 rotationVector = context.ReadValue<Vector3>();
            m_MovementController.OnRotateAxis(rotationVector);
        }

        public void OnRotateNegX(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
            m_MovementController.OnRotateNegX();
        }

        public void OnRotateNegY(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
            m_MovementController.OnRotateNegY();
        }

        public void OnRotateNegZ(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
            m_MovementController.OnRotateNegZ();
        }

        public void OnRotatePosX(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
            m_MovementController.OnRotatePosX();
        }

        public void OnRotatePosY(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
            m_MovementController.OnRotatePosY();
        }

        public void OnRotatePosZ(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
            m_MovementController.OnRotatePosZ();
        }
        #endregion

        #region Touch
        public void HandleStraightDetected(Vector2 direction)
        {
            m_MovementController.OnRotateAxis(HandleRotateTouch2Axis(direction));
        }

        public void HandleArcDetected(float direction)
        {
            m_MovementController.OnRotateAxis(Vector3.right * direction);
        }

        private Vector3 HandleRotateTouch2Axis(Vector2 vector)
        {
            var axis = Vector3.zero; // axis of rotation to be return.
            var angle = Vector2.Angle(Vector2.right, vector); // angle of input vector.

            float angleMargin = 20f;

            if(angle < angleMargin) axis = Vector3.down;
            else if(angle > (180f - angleMargin)) axis = Vector3.up;
            else if(angle > 90f - angleMargin && angle < 90f + angleMargin)
            {
                if(vector.y > 0) axis = Vector3.back;
                else axis = Vector3.forward;
            }

            return axis;
        }
        #endregion
    }
}