using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class RainbowText : MonoBehaviour
{
    public float speed = 1f; // Speed of color shifting
    private TMP_Text tmpText;
    private float hueOffset;

    void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
        tmpText.enableVertexGradient = true;
    }

    void Update()
    {
        hueOffset += Time.deltaTime * speed;
        hueOffset %= 1f;

        tmpText.colorGradient = new VertexGradient(
            Color.HSVToRGB((hueOffset + 0.00f) % 1f, 1f, 1f),
            Color.HSVToRGB((hueOffset + 0.25f) % 1f, 1f, 1f),
            Color.HSVToRGB((hueOffset + 0.50f) % 1f, 1f, 1f),
            Color.HSVToRGB((hueOffset + 0.75f) % 1f, 1f, 1f)
        );
    }
}

