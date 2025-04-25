using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowGlow : MonoBehaviour
{
    public float speed = 0.05f;
    private Material mat;
    private float hue;

    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        mat = rend.material; // Instantiates a copy of the material
        mat.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        hue += Time.deltaTime * speed;
        hue %= 1f;
        Color rainbow = Color.HSVToRGB(hue, 1, 1);
        mat.SetColor("_EmissionColor", rainbow);
    }
}
