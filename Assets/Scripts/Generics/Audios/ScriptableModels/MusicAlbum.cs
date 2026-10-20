using General.Audio;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicAlbum", menuName = "Scriptable Objects/MusicAlbum")]
public class MusicAlbum : ScriptableObject
{
    [SerializeField] MusicData[] Tracks;

    public int Length => Tracks.Length;

    public MusicData GetTrack(int index)
    {
        return Tracks[index];
    }
}
