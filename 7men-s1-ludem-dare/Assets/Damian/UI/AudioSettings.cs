using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public SoundManager soundManager;
    public Slider masterVolumeSlider, musicVolumeSlider, sfxVolumeSlider;

    void Start()
    {
        masterVolumeSlider.value = AudioListener.volume;
        musicVolumeSlider.value = soundManager.musicSource.volume;
        sfxVolumeSlider.value = soundManager.sfxSource.volume;

        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume; 
    }

    public void SetMusicVolume(float volume)
    {
        soundManager.musicSource.volume = volume; 
    }

    public void SetSFXVolume(float volume)
    {
        soundManager.sfxSource.volume = volume; 
    }
}
