using UnityEngine;
using System.Collections;
using System;

public class DamageFlash : MonoBehaviour
{
    private MeshRenderer[] meshRenderers;
    private Color[] originalColors;
    private Shader unlitColorShader;
    private Shader standardShader;

    void Awake()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        originalColors = new Color[meshRenderers.Length];
        unlitColorShader = Shader.Find("Unlit/Color");
        standardShader = Shader.Find("Standard");

        for (int i = 0; i < meshRenderers.Length; i++)
        {
            originalColors[i] = meshRenderers[i].material.color;
        }
    }

    public void FlashEffect()
    {
        // this will be the function that will be called when the enemy is hit

        StartCoroutine(FlashColor(0.5f));
    }

    IEnumerator FlashColor(float duration)
    {
        // Switch to Unlit/Color shader for the duration of the flashing
        foreach (var renderer in meshRenderers)
        {
            renderer.material.shader = unlitColorShader;
        }

        // Lerp to white
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            foreach (var renderer in meshRenderers)
            {
                renderer.material.color = Color.Lerp(renderer.material.color, Color.white, t);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Lerp back to original color
        elapsedTime = 0;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            foreach (var renderer in meshRenderers)
            {
                renderer.material.color = Color.Lerp(Color.white, originalColors[Array.IndexOf(meshRenderers, renderer)], t);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Switch back to Standard shader after color transition is complete
        foreach (var renderer in meshRenderers)
        {
            renderer.material.shader = standardShader;
            renderer.material.color = originalColors[Array.IndexOf(meshRenderers, renderer)];
        }
    }
}
