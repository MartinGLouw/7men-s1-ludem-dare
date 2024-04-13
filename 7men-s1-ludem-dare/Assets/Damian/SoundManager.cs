using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

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
        musicSource.volume = startVolume;
    }

    private IEnumerator FadeInMusic(float time)
    {
        
        musicSource.volume = 0;
        musicSource.Play();

        while (musicSource.volume < 1.0f)
        {
            musicSource.volume += Time.deltaTime / time;
            yield return null;
        }
    }

    public void PlaySFX(int index, float volume = 1.0f)
    {
        if (index < 0 || index >= sfxClips.Length) return;
        sfxSource.PlayOneShot(sfxClips[index], volume);
    }
}


//Use this in another script like so
//SoundManager.Instance.PlaySFX(0, 1); Sfx volume too low? Increase the volume : SoundManager.Instance.PlaySFX(0, 2);

//Queue music like so
//SoundManager.Instance.QueueMusic(0);
//SoundManager.Instance.QueueMusic(1); etc
