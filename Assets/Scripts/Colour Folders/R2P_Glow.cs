using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R2P_Glow : MonoBehaviour
{
    private float colorSpeed = 0.25f;     // Speed of color transition
    private float pulseSpeed = 0.75f;     // Speed of pulse (transparency/brightness)
    private float minAlpha = 0.95f;
    private float maxAlpha = 1.0f;

    private Material mat;
    private float lerpTime;

    Color purple = new Color(0.192f, 0.106f, 0.573f); // Cyan

    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        mat = rend.material;
        mat.EnableKeyword("_EMISSION");

        // Optional: Ensure transparency works
        mat.SetFloat("_Mode", 3);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;

        Color purple = GetComponent<Renderer>().material.color = new Color(0.192f, 0.106f, 0.573f); // Cyan

    }

    void Update()
    {
        // Lerp back and forth between red and purple
        lerpTime += Time.deltaTime * colorSpeed;
        float t = Mathf.PingPong(lerpTime, 1f);
        Color baseColor = Color.Lerp(Color.red, purple, t);

        // Pulse with transparency
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f);
        Color glowColor = new Color(baseColor.r * alpha, baseColor.g * alpha, baseColor.b * alpha, alpha);

        mat.SetColor("_EmissionColor", glowColor);
        mat.SetColor("_Color", glowColor);
    }
}
