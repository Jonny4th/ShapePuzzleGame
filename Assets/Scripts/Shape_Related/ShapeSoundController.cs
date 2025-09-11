using UnityEngine;
using Shape.Movement;

namespace Shape.Controller
{
    public class ShapeSoundController : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;

        public void SetVolume(float volume)
        {
            audioSource.volume = volume;
        }
    }
}
