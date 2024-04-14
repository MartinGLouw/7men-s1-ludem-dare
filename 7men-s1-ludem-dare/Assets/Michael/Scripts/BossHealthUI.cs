using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BossHealthUI : MonoBehaviour
{
    public Gradient bossHealthGradient;
    public Image bossHealthImage;
    [Range(0, 1)] 
    public float gradientValue;

    private void Update()
    {
        
        Color colour = bossHealthGradient.Evaluate(gradientValue);
        bossHealthImage.fillAmount = gradientValue;
        bossHealthImage.color = colour;
    }
}
