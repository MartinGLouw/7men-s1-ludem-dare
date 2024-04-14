using System;
using System.Collections;
using UnityEngine;

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
            var renderer = meshRenderers[i];
            renderer.material = new Material(renderer.material);
            if (renderer.material.HasProperty("_Color"))
                originalColors[i] = renderer.material.color;
            else if (renderer.material.HasProperty("_BaseColor"))
                originalColors[i] = renderer.material.GetColor("_BaseColor");
            else
                originalColors[i] = Color.white;
        }
    }

    public void TestFlashEffect()
    {
        StartCoroutine(FlashColor(0.5f));
    }

    IEnumerator FlashColor(float flashDuration)
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            var renderer = meshRenderers[i];
            renderer.material.shader = unlitColorShader;
            renderer.material.color = Color.white;
        }
        yield return new WaitForSeconds(flashDuration);
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            var renderer = meshRenderers[i];
            renderer.material.shader = standardShader;
            renderer.material.color = originalColors[i];
        }
    }

}
