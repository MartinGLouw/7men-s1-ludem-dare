using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAndLoseCanvases : MonoBehaviour
{
    public GameObject winCanvas;
    public GameObject loseCanvas;

    private void OnEnable()
    {
        EventManager.Instance.GameManagerEvents.OnLoseGame += ActivateLoseCanvas;
        EventManager.Instance.GameManagerEvents.OnEndGame += ActivateWinCanvas;

    }

    private void OnDisable()
    {
        EventManager.Instance.GameManagerEvents.OnLoseGame -= ActivateLoseCanvas;
        EventManager.Instance.GameManagerEvents.OnEndGame -= ActivateWinCanvas;
    }

    public void ActivateLoseCanvas()
    {
        // Disable enemy movement scripts?
        loseCanvas.SetActive(true);
    }

    public void ActivateWinCanvas()
    {
        // Disable enemy movement scripts?
        winCanvas.SetActive(true);
    }


}
