using UnityEngine;
using General.Audio;

namespace Shape.Controller
{
    public class ShapeSoundController : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;
        [SerializeField] SoundEffectSet shapeMoveSfx;

        public void Play()
        {
            AudioClip clip = shapeMoveSfx.GetClip();
            audioSource.PlayOneShot(clip);
        }

        public void SetVolume(float volume)
        {
            audioSource.volume = volume;
        }
    }
}
