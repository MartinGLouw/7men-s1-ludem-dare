using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAndLoseCanvases : MonoBehaviour
{
    public GameObject winCanvas;
    public GameObject loseCanvas;

    public void ActivateLoseCanvas()
    {
        loseCanvas.SetActive(true);
    }

    public void ActivateWinCanvas()
    {
        winCanvas.SetActive(true);
    }


}
