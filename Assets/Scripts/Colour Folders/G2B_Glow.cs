using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G2B_Glow : MonoBehaviour
{
    private float colorSpeed = 0.25f;     // Speed of color transition
    private float pulseSpeed = 0.75f;     // Speed of pulse (transparency/brightness)
    [SerializeField] 
    private float minAlpha = 0.95f;
    [SerializeField]
    private float maxAlpha = 1.0f;

    private Material mat;
    private float lerpTime;

    public Color myGreen = new Color(0.047f, 0.937f, 0.118f); // #0cef1e
    public Color myBlue = new Color(0.063f, 0.243f, 0.969f); // #103ef7

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

    }

    void Update()
    {
        // Lerp back and forth between red and purple
        lerpTime += Time.deltaTime * colorSpeed;
        float t = Mathf.PingPong(lerpTime, 1f);
        Color baseColor = Color.Lerp(myGreen, myBlue, t);

        // Pulse with transparency
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f);
        Color glowColor = new Color(baseColor.r * alpha, baseColor.g * alpha, baseColor.b * alpha, alpha);

        mat.SetColor("_EmissionColor", glowColor);
        mat.SetColor("_Color", glowColor);
    }
}
