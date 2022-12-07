using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] AudioClip[] playlist;
    int playingIndex;
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
        _audioSource.clip = playlist[playingIndex];
        _audioSource.Play();
        playingIndex++;
        if (playingIndex >= playlist.Length) playingIndex = 0;
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

}
