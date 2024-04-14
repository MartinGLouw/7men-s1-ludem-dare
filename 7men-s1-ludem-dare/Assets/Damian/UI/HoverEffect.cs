using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;  

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color defaultColor = Color.white;
    public Color hoverColor = Color.gray;
    public float scaleAmount = 1.1f;    
    public float shakeDuration = 0.5f;  
    public float shakeStrength = 90f;   
    public int shakeVibrato = 10;       

    private Button button;
    private Vector3 originalScale;      

    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.image.color = defaultColor;
            originalScale = transform.localScale;  
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button != null)
        {
            button.image.color = hoverColor;
            
            transform.DOScale(originalScale * scaleAmount, 0.1f);  // Scale up
            transform.DOShakeRotation(shakeDuration, new Vector3(0, 0, shakeStrength), shakeVibrato);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (button != null)
        {
            button.image.color = defaultColor;
            
            transform.DOScale(originalScale, 0.1f);
        }
    }
}
