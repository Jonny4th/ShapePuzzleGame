using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    //This is singleton.

    private AudioSource _audioSource;
    [SerializeField] AudioClip[] playlist;
    [SerializeField] int playingIndex;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        playingIndex = Random.Range(0, playlist.Length);
        _audioSource.clip = playlist[playingIndex];
        _audioSource.Play();
        //playingIndex++;
        //if (playingIndex >= playlist.Length) playingIndex = 0;
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

    public void SetVolume(float value)
    {
        _audioSource.volume = value;
    }

}
