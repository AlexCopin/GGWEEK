using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button
{
    public KeyCode key;
    public Color color;
    public bool canBePressed;

    public Button()
    {
        color = new Color(255, 255, 255);
    }
    public Button(KeyCode _key, Color _color)
    {
        key = _key;
        color = _color;
        canBePressed = false;
    }
}
