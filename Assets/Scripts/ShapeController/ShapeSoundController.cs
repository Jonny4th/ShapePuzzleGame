using UnityEngine;
using Shape.Movement;

namespace Shape.Controller
{
    public class ShapeSoundController : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;

        private void OnEnable()
        {
            ShapeMovementManager.Moved += OnMove;
        }

        private void OnDisable()
        {
            ShapeMovementManager.Moved -= OnMove;
        }

        private void OnMove(MovementInfo obj)
        {
            audioSource.Play();
        }

        public void SetVolume(float volume)
        {
            audioSource.volume = volume;
        }
    }
}
