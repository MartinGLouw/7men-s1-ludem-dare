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

    // Default canvas setup
    private GameObject currentActiveCanvas;

    void Awake()
    {
        CheckSceneAndAdjustCanvases();
    }


    private void CheckSceneAndAdjustCanvases()
    {
        switch (GetCurrentGameState())
        {
            case GameState.MainMenu:
                SoundManager.Instance.QueueMusic(0); //maybe change this to trigger the relevant method in the events manager later.
                currentActiveCanvas = mainMenuCanvas;
                break;
            case GameState.TutorialScene:
            //Activate relevant canvases here
            case GameState.MainGame:
            //Activate relevant canvases here if needed
            case GameState.UnknownScene:
                Debug.LogError("Unknown scene detected");
                break;
        }
    }

    public void ActivateCanvas(GameObject canvas)
    {
        SoundManager.Instance.PlaySFX(0);
        canvas.SetActive(true);
    }

    public void DeactivateCanvas(GameObject canvas)
    {
        SoundManager.Instance.PlaySFX(0);
        canvas.SetActive(false);
    }

    // Get current game state based on the scene name
    private GameState GetCurrentGameState()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Damian_UI")
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
        //invoke deactivate pauseCanvas
    }

    public void RestartGame()
    {
        SoundManager.Instance.PlaySFX(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        SoundManager.Instance.PlaySFX(0);
        SceneManager.LoadScene("Damian_UI");

    }
}