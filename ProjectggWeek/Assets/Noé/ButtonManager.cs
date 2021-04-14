using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    char a;
    Color color;
    Button B1, B2, B3, B4;
    
    void Start()
    {
        
        B1 = new Button(KeyCode.A, color);
        B1.color = new Color(255, 255, 255);
        print(B1.color);
        ChangeColor(B1, new Color(255, 5, 5));
        print(B1.color);
    }

    // Update is called once per frame
    void Update()
    {
        //B1.color = new Color(255, 0, 0);
    }

    public void ChangeColor(Button button, Color color)
    {
        button.color = color;
    }
}
