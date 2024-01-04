using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Color DefaultColor { get; set; }

    [SerializeField] private Color[] _colors;

    [SerializeField] private SpriteRenderer _fillRenderer;

    public void SetDefaultColor(Color color)
    {
        DefaultColor = color;
        SetColor(color);
    }

    public void SetColor(Color color)
    {
        _fillRenderer.color = color;
    }

    public void SetRandomColor()
    {
        int randomColorIndex = Random.Range(0,_colors.Length);

        Color selectedColor = _colors[randomColorIndex];

        DefaultColor = selectedColor;

        SetColor(selectedColor);
    }
}
