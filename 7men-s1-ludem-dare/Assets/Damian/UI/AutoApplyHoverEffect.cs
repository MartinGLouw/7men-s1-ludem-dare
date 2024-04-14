using UnityEngine;
using UnityEngine.UI;

public class AutoApplyHoverEffect : MonoBehaviour
{
    public Color defaultColor = Color.white;
    public Color hoverColor = Color.gray;
    public float defaultScaleAmount = 1.1f;
    public float defaultShakeDuration = 0.5f;
    public float defaultShakeStrength = 90f;
    public int defaultShakeVibrato = 10;

    void Start()
    {
       
        Button[] buttons = Resources.FindObjectsOfTypeAll<Button>();

       
        foreach (Button button in buttons)
        {
           
            if (button.gameObject.scene.name == null)
                continue; 

            HoverEffect hoverEffect = button.gameObject.AddComponent<HoverEffect>();
            hoverEffect.defaultColor = defaultColor;
            hoverEffect.hoverColor = hoverColor;
            hoverEffect.scaleAmount = defaultScaleAmount;
            hoverEffect.shakeDuration = defaultShakeDuration;
            hoverEffect.shakeStrength = defaultShakeStrength;
            hoverEffect.shakeVibrato = defaultShakeVibrato;
        }
    }
}
