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
                ActivateCanvas(mainMenuCanvas);
                DeactivateCanvas(settingsCanvas);
                DeactivateCanvas(controlsCanvas);
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
        canvas.SetActive(true);
    }

    public void DeactivateCanvas(GameObject canvas)
    {
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
        SceneManager.LoadScene("MainGame");
    }

    public void ExitToDesktop()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        //invoke deactivate pauseCanvas
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Damian_UI");

    }
}
