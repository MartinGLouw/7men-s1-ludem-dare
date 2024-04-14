using UnityEngine;
using DG.Tweening;

public class UIAnimatorManager : MonoBehaviour
{
    public static UIAnimatorManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FadeInCanvas(CanvasGroup canvasGroup, float duration)
    {
        canvasGroup.alpha = 0;  
        canvasGroup.gameObject.SetActive(true);  
        canvasGroup.DOFade(1, duration).OnComplete(() => {
            canvasGroup.interactable = true;  
        });
    }

    public void FadeOutCanvas(CanvasGroup canvasGroup, float duration)
    {
        canvasGroup.interactable = false;  
        canvasGroup.DOFade(0, duration).OnComplete(() => {
            canvasGroup.gameObject.SetActive(false);  
        });
    }

}
