using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDisplay : MonoBehaviour
{

    public GameObject[] Buttons;
    public GameObject[] Leds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonPushAnim(string letter)
    {
        switch (letter) 
        {
            case "A":
                Buttons[0].GetComponent<Animation>().Play("ButtonAPushed");
                break;
            case "Z":
                Buttons[1].GetComponent<Animation>().Play("ButtonZPushed");
                break;
            case "E":
                Buttons[2].GetComponent<Animation>().Play("ButtonEPushed");
                break;
            case "Q":
                Buttons[3].GetComponent<Animation>().Play("ButtonQPushed");
                break;
            case "S":
                Buttons[4].GetComponent<Animation>().Play("ButtonSPushed");
                break;
            case "D":
                Buttons[5].GetComponent<Animation>().Play("ButtonDPushed");
                break;
            default:
                Debug.Log("Another input");
                break;
        }
    }
    public void LightLED(Color color, int index) 
    {
        Leds[index].GetComponent<SpriteRenderer>().color = color;
        for(int i = 0; i < Leds.Length; i++)
        {
            if(i != index)
            {
                Leds[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
}
