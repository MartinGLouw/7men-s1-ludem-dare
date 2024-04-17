using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera cam;

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        slider.value = currentHealth/maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.rotation = cam.transform.rotation;
    }
}
