using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BossHealthUI : MonoBehaviour
{
    public Gradient bossHealthGradient;
    public Image bossHealthImage;

    /// <summary>
    /// Gradient value between 0 and 1
    /// </summary>
    /// <param name="gradientValue"></param>
    public void UpdateBossHealth(float gradientValue)
    {
        Color colour = bossHealthGradient.Evaluate(gradientValue);
        bossHealthImage.fillAmount = gradientValue;
        bossHealthImage.color = colour;
    }
}
