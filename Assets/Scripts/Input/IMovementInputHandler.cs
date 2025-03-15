using UnityEngine.InputSystem;

namespace Shape.Inputs
{
    public interface IMovementInputHandler
    {
        public void SetShapeMovementController(IShapeMovementController controller);
    }
}