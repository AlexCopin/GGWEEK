using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopDialog : MonoBehaviour
{
    public AnimationCurve animCurve;
    public string firstText;
    private string currentText = "";
    public float showTextDelay;
    public GameObject bubbleBar;
    public Text textBubble;
    void Start()
    {
        bubbleBar.transform.localScale = new Vector3(0, 0, 1);
        ChangeText(firstText);
        bubbleBar = GameObject.Find("BubbleBar");
        textBubble = GameObject.Find("BubbleText").GetComponent<Text>();
    }


    void Update()
    {
        bubbleBar.transform.localScale = new Vector3(Mathf.Lerp(0, 1, animCurve.Evaluate(Time.time)), Mathf.Lerp(0, 1, animCurve.Evaluate(Time.time)), 1);
    }

    public void ChangeText(string text)
    {
        StartCoroutine(ShowText(text));
    }
    public IEnumerator ShowText(string text) 
    {
        currentText = "";
        for (int i = 0; i < text.Length; i++) 
        {

            currentText += text[i];
            textBubble.text = currentText;
            yield return new WaitForSeconds(showTextDelay);
        }
    }


}

