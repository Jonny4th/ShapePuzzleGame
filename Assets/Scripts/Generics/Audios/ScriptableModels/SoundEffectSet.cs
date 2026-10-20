using UnityEngine;

namespace General.Audio
{
    [CreateAssetMenu(fileName = "SoundEffectSet", menuName = "Scriptable Objects/SoundEffectSet")]
    public class SoundEffectSet : ScriptableObject
    {
        [SerializeField] private AudioClip[] m_Sounds;

        public int Length => m_Sounds.Length;

        public AudioClip GetClip()
        {
            int rand = Random.Range(0, Length);
            return m_Sounds[rand];
        }
    }
}