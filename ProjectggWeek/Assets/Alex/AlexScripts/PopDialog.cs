using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopDialog : MonoBehaviour
{
    public AnimationCurve animCurve;
    private string currentText = "";
    string previousText = "";
    public float showTextDelay;
    public float hideTextDelay;
    public GameObject bubbleBar;
    public Text textBubble;

    float lerp = 0.0f;
    public float lerpValue = 2.0f;

    void Start()
    {
        bubbleBar = GameObject.Find("BubbleBar");
        textBubble = GameObject.Find("BubbleText").GetComponent<Text>();
    }


    void Update()
    {
        bubbleBar.transform.localScale = new Vector3(Mathf.Lerp(0, 1.6f, animCurve.Evaluate(Time.time)), Mathf.Lerp(0, 1.6f, animCurve.Evaluate(Time.time)), 1);
        textBubble.transform.localScale = new Vector3(Mathf.Lerp(0, 1.0f, animCurve.Evaluate(Time.time)), Mathf.Lerp(0, 1, animCurve.Evaluate(Time.time)), 1);
        if (lerp > lerpValue)
        {
            lerp = 0.0f;
        }
        lerp += Time.deltaTime;
    }

    public void ChangeText(string text)
    {
        StartCoroutine(HideText(text));
    }
    public IEnumerator ShowText(string text)
    {
        previousText = text;
        for (int i = 0; i <= text.Length; i++) 
        {

            currentText = text.Substring(0, i);
            textBubble.text = currentText;
            yield return new WaitForSeconds(showTextDelay);
        }
    }

    public IEnumerator HideText(string text)
    {
        for (int j = previousText.Length - 1; j >= 0; j--)
        {
            currentText = previousText.Substring(0, j);
            textBubble.text = currentText;
            yield return new WaitForSeconds(hideTextDelay);
        }
        StartCoroutine(ShowText(text));
    }
}

