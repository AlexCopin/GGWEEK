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
                Buttons[0].GetComponent<Animator>().Play("ButtonAPushed");
                FindObjectOfType<AudioManager>().Play("buttonPressed");
                StartCoroutine(ReleaseButton(letter));
                break;
            case "Z":
                Buttons[1].GetComponent<Animator>().Play("ButtonZPushed");
                FindObjectOfType<AudioManager>().Play("buttonPressed");
                StartCoroutine(ReleaseButton(letter));
                break;
            case "E":
                Buttons[2].GetComponent<Animator>().Play("ButtonEPushed");
                FindObjectOfType<AudioManager>().Play("buttonPressed");
                StartCoroutine(ReleaseButton(letter));
                break;
            case "Q":
                Buttons[3].GetComponent<Animator>().Play("ButtonQPushed");
                FindObjectOfType<AudioManager>().Play("buttonPressed");
                StartCoroutine(ReleaseButton(letter));
                break;
            case "S":
                Buttons[4].GetComponent<Animator>().Play("ButtonSPushed");
                FindObjectOfType<AudioManager>().Play("buttonPressed");
                StartCoroutine(ReleaseButton(letter));
                break;
            case "D":
                Buttons[5].GetComponent<Animator>().Play("ButtonDPushed");
                FindObjectOfType<AudioManager>().Play("buttonPressed");
                StartCoroutine(ReleaseButton(letter));
                break;
            default:
                Debug.Log("Another input");
                break;
        }
    }
    public void LightLED(Color color, int index) 
    {
        Leds[index].GetComponent<SpriteRenderer>().color = color;
        FindObjectOfType<AudioManager>().Play("led");
        for (int i = 0; i < Leds.Length; i++)
        {
            if(i != index)
            {
                Leds[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
    IEnumerator ReleaseButton(string letter)
    {
        switch (letter)
        {
            case "A":
                Buttons[0].GetComponent<Animator>().Play("ButtonAFree");
                break;
            case "Z":
                Buttons[1].GetComponent<Animator>().Play("ButtonZFree");
                break;
            case "E":
                Buttons[2].GetComponent<Animator>().Play("ButtonEFree");
                break;
            case "Q":
                Buttons[3].GetComponent<Animator>().Play("ButtonQFree");
                break;
            case "S":
                Buttons[4].GetComponent<Animator>().Play("ButtonSFree");
                break;
            case "D":
                Buttons[5].GetComponent<Animator>().Play("ButtonDFree");
                break;
            default:
                Debug.Log("Another input");
                break;
        }
        yield return new WaitForSeconds(0.2f);
    }
}
