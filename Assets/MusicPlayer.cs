using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum PlayMode
{
    Shuffled,
    Randomized,
    Ordered,
}

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    //This is singleton.
    public PlayMode CurrentPlayMode;
    public bool IsPlaying {get; private set;}

    [SerializeField] private AudioClip[] m_playlist;

    private AudioSource m_audioSource;
    private int m_playingIndex = 0;

    private Coroutine m_musicPlayingProcess;

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();

        PlayMusic(CurrentPlayMode);
    }

    public void PlayMusic(PlayMode playMode)
    {
        StopMusic();

        IEnumerator loopingMusicProcess = playMode switch
        {
            PlayMode.Shuffled => StartLoopingMusic_Shuffled(),
            PlayMode.Randomized => StartPlayMusicPrecess_Randomized(),
            PlayMode.Ordered => StartLoopingMusic_Ordered(),
            _ => StartLoopingMusic_Shuffled()
        };
        
        m_musicPlayingProcess = StartCoroutine(loopingMusicProcess);
    }

    public void StopMusic()
    {
        IsPlaying = false;

        if(m_musicPlayingProcess != null) StopCoroutine(m_musicPlayingProcess);

        m_audioSource.Stop();
    }

    public void SetVolume(float value)
    {
        m_audioSource.volume = value;
    }

    #region Coroutines
    private IEnumerator StartPlayMusicPrecess_Randomized()
    {
        IsPlaying = true;

        PlayAtIndex(Random.Range(0, m_playlist.Length));

        while(IsPlaying)
        {
            if (m_audioSource.isPlaying)
            {
                yield return null;
                continue;
            }

            PlayAtIndex(Random.Range(0, m_playlist.Length));
            
            yield return null;
        }
    }
    
    private IEnumerator StartLoopingMusic_Shuffled()
    {
        IsPlaying = true;

        int[] playingOrder = GetShuffledList(m_playlist.Length);
        int currentOrder = 0;
        PlayAtIndex(playingOrder[currentOrder]);

        while(IsPlaying)
        {
            if (m_audioSource.isPlaying)
            {
                yield return null;
                continue;
            }
            
            currentOrder++;
            
            if(currentOrder == playingOrder.Length)
            {
                playingOrder = GetShuffledList(m_playlist.Length);
                currentOrder = 0;
            }

            PlayAtIndex(playingOrder[currentOrder]);
            yield return null;
        }
    }

    private IEnumerator StartLoopingMusic_Ordered()
    {
        IsPlaying = true;

        int m_playingIndex = -1;
        PlayAtIndex(m_playingIndex);

        while(IsPlaying)
        {
            if (m_audioSource.isPlaying)
            {
                yield return null;
                continue;
            }
            
            m_playingIndex++;
            m_playingIndex %= m_playlist.Length;
            PlayAtIndex(m_playingIndex);
            yield return null;
        }
    }
    #endregion

    #region Auxilary Methods
    private void PlayAtIndex(int index)
    {
        if(index >= m_playlist.Length) throw new IndexOutOfRangeException();

        m_playingIndex = index;
        m_audioSource.clip = m_playlist[m_playingIndex];
        m_audioSource.Play();
    }

    private int[] GetShuffledList(int length)
    {
        int[] shuffledList = GetOrderedList(length);

        for (int i = 0; i < shuffledList.Length - 2; i++)
        {
            int swapPosition = Random.Range(i, length - 1);
            (shuffledList[swapPosition], shuffledList[i]) = (shuffledList[i], shuffledList[swapPosition]);
        }

        return shuffledList;

        static int[] GetOrderedList(int length)
        {
            int[] orderedList = new int[length];

            for (int i = 0; i < orderedList.Length; i++)
            {
                orderedList[i] = i;
            }

            return orderedList;
        }
    }
    #endregion
}
