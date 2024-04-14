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

    public void SlideInCanvas(CanvasGroup canvasGroup, float duration, Vector3 endPosition)
    {
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.transform.localPosition = endPosition - new Vector3(1000, 0, 0); // start off-screen
        canvasGroup.transform.DOLocalMove(endPosition, duration).SetEase(Ease.OutQuint);
    }

    public void SlideOutCanvas(CanvasGroup canvasGroup, float duration, Vector3 endPosition)
    {
        canvasGroup.transform.DOLocalMove(endPosition + new Vector3(1000, 0, 0), duration).SetEase(Ease.InQuint).OnComplete(() => {
            canvasGroup.gameObject.SetActive(false);
        });
    }

}
