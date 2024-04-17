using System;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameData gameData;
    public bool isPaused = false;

    private AudioSettings _audioSettings;
    
    [Header("UI Toolkit")] 
    private VisualElement _ui;
    
    private Button _resumeButton;
    private Button _settingsButton;
    private Button _quitButton;
    private Button _returnButton;

    private Slider _masterSlider;
    private Slider _musicSlider;
    private Slider _sfxSlider;

    private Label _timeTakenLabel;
    
    private VisualElement _settingsContainer;
    private VisualElement _menuContainer;
    private float _time;

    private bool uiSetup;

    // This script is responsible for pausing the game and displaying the pause menu.

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //SoundManager.Instance.PlaySFX(0);

            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        //SoundManager.Instance.PlaySFX(0);
        _menuContainer.RemoveFromClassList("hide");
        _settingsContainer.AddToClassList("hide");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        

    }

    void SetupUI()
    {
        
        _ui = pauseMenuUI.GetComponent<UIDocument>().rootVisualElement;
        
        _resumeButton = _ui.Q<Button>("ResumeButton");
        _resumeButton.clicked += Resume;

        _settingsContainer = _ui.Q<VisualElement>("SettingsContainer");
        _menuContainer = _ui.Q<VisualElement>("Container");

        _settingsButton = _ui.Q<Button>("SettingsButton");
        _settingsButton.clicked += OpenGameSettings;
        
        _quitButton = _ui.Q<Button>("QuitButton");
        _quitButton.clicked += QuitGame;

        _returnButton = _ui.Q<Button>("BackButton");
        _returnButton.clicked += () =>
        {
            _menuContainer.RemoveFromClassList("hide");
            _settingsContainer.AddToClassList("hide");

        };

        _timeTakenLabel = _ui.Q<Label>("TimeTakenLabel");
        _timeTakenLabel.text = $"Time: {Mathf.Ceil(gameData.TimeTaken).ToString()}s";
        
        //Slider
        _masterSlider = _ui.Q<Slider>("MasterSlider");
        _masterSlider.value = 1;
        _musicSlider = _ui.Q<Slider>("MusicSlider");
        _musicSlider.value = 1;
        _sfxSlider = _ui.Q<Slider>("SFXSlider");
        _sfxSlider.value = 1;
        
        _masterSlider.RegisterCallback<ChangeEvent<float>>(SliderValueChange);
        _musicSlider.RegisterCallback<ChangeEvent<float>>(SliderValueChange);
        _sfxSlider.RegisterCallback<ChangeEvent<float>>(SliderValueChange);

    }

    void SliderValueChange(ChangeEvent<float> value)
    {
        AudioListener.volume = _masterSlider.value;
        SoundManager.Instance.musicSource.volume = _musicSlider.value;
        SoundManager.Instance.sfxSource.volume = _sfxSlider.value;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        SetupUI();
        
        Time.timeScale = 0f;
        isPaused = true;

    }

    private void OpenGameSettings()
    {
        _settingsContainer.RemoveFromClassList("hide");
        _menuContainer.AddToClassList("hide");
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}