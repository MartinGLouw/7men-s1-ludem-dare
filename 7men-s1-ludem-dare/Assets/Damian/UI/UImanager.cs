using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public enum GameState
    {
        MainMenu,
        TutorialScene,
        MainGame,
        UnknownScene
    }

    // Canvas references
    public GameObject mainMenuCanvas;
    public GameObject settingsCanvas;
    public GameObject controlsCanvas;

    public GameObject howToPlayCanvas1;
    public GameObject howToPlayCanvas2;
    public GameObject howToPlayCanvas3;

    // Canvas panels
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;

    public GameObject controlsPanel;
    public GameObject howToPlayPanel1;
    public GameObject howToPlayPanel2;
    public GameObject howToPlayPanel3;

    // Default canvas setup
    private GameObject currentActiveCanvas;

    void Awake()
    {
        CheckSceneAndAdjustCanvases();
    }

    private void CheckSceneAndAdjustCanvases()
    {
        GameState currentState = GetCurrentGameState();
        switch (currentState)
        {
            case GameState.MainMenu:
                if (mainMenuPanel != null)
                {
                    SoundManager.Instance.ClearMusicQueue();
                    SoundManager.Instance.QueueMusic(1);
                    UIAnimatorManager.Instance.FadeInCanvas(mainMenuPanel, 0.5f);
                    UIAnimatorManager.Instance.SlideInCanvas(mainMenuPanel, 0.5f, mainMenuPanel.transform.localPosition);
                    currentActiveCanvas = mainMenuCanvas;
                }
                else
                {
                    Debug.LogError("MainMenuPanel is null in UIManager.");
                }
                break;
            case GameState.TutorialScene:
                // Activate relevant canvases here, check for null as necessary
                break;
            case GameState.MainGame:
                SoundManager.Instance.ClearMusicQueue();
                SoundManager.Instance.QueueMusic(0);
                Debug.Log("Main Game scene detected");
                break;
            case GameState.UnknownScene:
                Debug.LogError("Unknown scene detected");
                break;
        }
    }

    public void ActivateCanvas(GameObject canvas)
    {
        if (canvas != null)
        {
            canvas.SetActive(true);
        }
        else
        {
            Debug.LogError("Attempted to activate a null canvas in UIManager.");
        }
    }

    public void DeactivateCanvas(GameObject canvas)
    {
        if (canvas != null)
        {
            canvas.SetActive(false);
            UIAnimatorManager.Instance.SlideOutCanvas(canvas, -0.5f, canvas.transform.localPosition);
            UIAnimatorManager.Instance.FadeOutCanvas(canvas, 0.5f);
        }
        else
        {
            Debug.LogError("Attempted to deactivate a null canvas in UIManager.");
        }
    }

    public void ActivatePanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(true);
            SoundManager.Instance.PlaySFX(0);
            UIAnimatorManager.Instance.SlideInCanvas(panel, 0.5f, panel.transform.localPosition);
            UIAnimatorManager.Instance.FadeInCanvas(panel, 0.5f);
        }
        else
        {
            Debug.LogError("Panel reference not set in UIManager.");
        }
    }

    public void DeactivatePanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(false);
            SoundManager.Instance.PlaySFX(0);
            UIAnimatorManager.Instance.SlideOutCanvas(panel, 0.5f, panel.transform.localPosition);
            UIAnimatorManager.Instance.FadeOutCanvas(panel, 0.5f);
        }
        else
        {
            Debug.LogError("Panel reference not set in UIManager.");
        }
    }

    private GameState GetCurrentGameState()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "MainMenu")
            return GameState.MainMenu;
        else if (sceneName == "TutorialScene")
            return GameState.TutorialScene;
        else if (sceneName == "MainGame")
            return GameState.MainGame;
        else
            return GameState.UnknownScene;
    }

    public void PlayGame()
    {
        SoundManager.Instance.PlaySFX(0);
        SceneManager.LoadScene("MainGame");
    }

    public void ExitToDesktop()
    {
        SoundManager.Instance.PlaySFX(0);
        Application.Quit();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        SoundManager.Instance.PlaySFX(0);
        // Invoke deactivate pauseCanvas with appropriate checks
    }

    public void RestartGame()
    {
        SoundManager.Instance.PlaySFX(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        SoundManager.Instance.PlaySFX(0);
        SceneManager.LoadScene("MainMenu");
    }
}
