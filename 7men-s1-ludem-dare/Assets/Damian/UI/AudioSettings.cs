using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public SoundManager soundManager;
    public Slider masterVolumeSlider, musicVolumeSlider, sfxVolumeSlider;

    void Start()
    {
        LoadSettings();
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void LoadSettings()
    {
      
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);

        UpdateVolumes();  
    }

    void UpdateVolumes()
    {
        SetMasterVolume(masterVolumeSlider.value);
        SetMusicVolume(musicVolumeSlider.value);
        SetSFXVolume(sfxVolumeSlider.value);
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        soundManager.musicSource.volume = volume;
        soundManager.targetMusicVolume = volume; 
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        soundManager.sfxSource.volume = volume;
        soundManager.targetSFXVolume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    void OnDisable()
    {
        PlayerPrefs.Save(); 
    }
}
