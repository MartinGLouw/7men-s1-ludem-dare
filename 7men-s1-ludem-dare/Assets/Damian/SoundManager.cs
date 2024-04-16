using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public float targetMusicVolume = 1.0f; 
    public float targetSFXVolume = 1.0f;  

    public AudioSource sfxSource;
    public AudioSource musicSource;
    public AudioClip[] sfxClips;
    public AudioClip[] musicClips;
    private Queue<int> musicQueue = new Queue<int>(); 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else if (Instance != this)
        {
            Destroy(gameObject); 
        }
    }

    private void Update()
    {
        if (!musicSource.isPlaying && musicQueue.Count > 0)
        {
            StartCoroutine(PlayMusicWithFade(musicQueue.Dequeue(), 4.0f));
        }
    }

    public void QueueMusic(int index)
    {
        if (index >= 0 && index < musicClips.Length)
        {
            musicQueue.Enqueue(index); 
        }
    }

    public IEnumerator PlayMusicWithFade(int index, float fadeTime)
    {
        if (musicSource.isPlaying)
        {
            yield return StartCoroutine(FadeOutMusic(fadeTime));
        }

        musicSource.clip = musicClips[index]; 
        musicSource.Play();
        yield return StartCoroutine(FadeInMusic(fadeTime)); 
    }

    private IEnumerator FadeOutMusic(float time)
    {
        float startVolume = musicSource.volume;
        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / time; 
            yield return null;
        }
        musicSource.Stop();
        musicSource.volume = targetMusicVolume; 
    }

    private IEnumerator FadeInMusic(float time)
    {
        float initialVolume = 0;
        musicSource.volume = initialVolume;
        musicSource.Play();

        while (musicSource.volume < targetMusicVolume)
        {
            musicSource.volume += Time.deltaTime / time * targetMusicVolume; 
            yield return null;
        }
    }

    public void PlaySFX(int index, float volume = 1.0f)
    {
        if (index < 0 || index >= sfxClips.Length) return; 
        sfxSource.PlayOneShot(sfxClips[index], targetSFXVolume * volume); 
    }

    public void ClearMusicQueue()
    {
        musicQueue.Clear();
        musicSource.Stop(); 
    }

    public void PlayWinMusic()
    {
        ClearMusicQueue();
        int winMusicIndex = Array.IndexOf(musicClips, 4);
        if (winMusicIndex != -1)
        {
            QueueMusic(winMusicIndex);
        }
    }

    public void PlayLoseMusic()
    {
        ClearMusicQueue();
        int loseMusicIndex = Array.IndexOf(musicClips, 5); 
        if (loseMusicIndex != -1)
        {
            QueueMusic(loseMusicIndex);
        }
    }
}
