using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashCooldown : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera cam;

    float timeLeft, cooldownTime;

    public void UpdateDash(float cooldownTime)
    {
        this.cooldownTime = cooldownTime;

        timeLeft = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft += Time.deltaTime;

        transform.parent.rotation = cam.transform.rotation;

        slider.value = (cooldownTime - timeLeft)/cooldownTime;
    }
}

