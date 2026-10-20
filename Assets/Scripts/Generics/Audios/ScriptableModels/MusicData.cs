using System;
using UnityEngine;
using UnityEngine.UI;

namespace General.Audio
{
    [CreateAssetMenu(fileName = "MusicData", menuName = "Scriptable Objects/MusicData")]
    public class MusicData : ScriptableObject
    {
        public AudioClip AudioClip;
        public MusicMetaData MetaData;

        public static implicit operator AudioClip(MusicData m) => m.AudioClip;
    }

    [Serializable]
    public struct MusicMetaData
    {
        public string Name;
        public string Artist;
        public Image Cover;
        public string Reference;
    }
}
