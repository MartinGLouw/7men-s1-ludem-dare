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

    public void FadeInCanvas(GameObject panel, float duration)
    {
        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup not found on " + panel.name);
            return;
        }
        canvasGroup.alpha = 0;
        panel.SetActive(true);
        canvasGroup.DOFade(1, duration).OnComplete(() => {
            canvasGroup.interactable = true;
        });
    }

    public void FadeOutCanvas(GameObject panel, float duration)
    {
        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup not found on " + panel.name);
            return;
        }
        canvasGroup.interactable = false;
        canvasGroup.DOFade(0, duration).OnComplete(() => {
            panel.SetActive(false);
        });
    }

    public void SlideInCanvas(GameObject panel, float duration, Vector3 endPosition)
    {
        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup not found on " + panel.name);
            return;
        }
        panel.SetActive(true);
        panel.transform.localPosition = endPosition - new Vector3(1000, 0, 0); // start off-screen
        panel.transform.DOLocalMove(endPosition, duration).SetEase(Ease.OutQuint);
    }

    public void SlideOutCanvas(GameObject panel, float duration, Vector3 endPosition)
    {
        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup not found on " + panel.name);
            return;
        }
        panel.transform.DOLocalMove(endPosition + new Vector3(1000, 0, 0), duration).SetEase(Ease.InQuint).OnComplete(() => {
            panel.SetActive(false);
        });
    }
}
