using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopDialog : MonoBehaviour
{
    public AnimationCurve animCurve;
    public string firstText;
    private string currentText = "";
    string previousText = "";
    public float showTextDelay;
    public GameObject bubbleBar;
    public Text textBubble;

    float lerp = 0.0f;
    public float lerpValue = 2.0f;

    bool showingText = false;
    bool hidingText = false;
    bool nextChangeText = false;
    void Start()
    {
        bubbleBar = GameObject.Find("BubbleBar");
        textBubble = GameObject.Find("BubbleText").GetComponent<Text>();
        previousText = firstText;
    }


    void Update()
    {
        if(showingText) 
        {
            bubbleBar.transform.localScale = new Vector3(Mathf.Lerp(0, 1, lerp), Mathf.Lerp(0, 1, lerp), 1);
            textBubble.transform.localScale = new Vector3(Mathf.Lerp(0, 1, lerp), Mathf.Lerp(0, 1, lerp), 1);
            if (lerp > lerpValue)
            {
                lerp = 0.0f;
                showingText = false;
            }
        }
        if (hidingText) 
        {
            bubbleBar.transform.localScale = new Vector3(Mathf.Lerp(1,0, lerp), Mathf.Lerp(1,0, lerp), 1);
            textBubble.transform.localScale = new Vector3(Mathf.Lerp(1, 0, lerp), Mathf.Lerp(1, 0, lerp), 1);
            lerp += Time.deltaTime;
            if (lerp > lerpValue)
            {
                lerp = 0.0f;
                hidingText = false;
            }
        }
        lerp += Time.deltaTime;
    }

    public void ChangeText(string text)
    {
        StartCoroutine(HideText(text));
    }
    public IEnumerator ShowText(string text)
    {
        Debug.Log("prout");
        previousText = text;
        showingText = true;
        hidingText = false;
        for (int i = 0; i < text.Length; i++) 
        {

            currentText = text.Substring(0, i);
            textBubble.text = currentText;
            yield return new WaitForSeconds(showTextDelay);
        }
        nextChangeText = false;
    }

    public IEnumerator HideText(string text)
    {
        showingText = false;
        hidingText = true;
        for (int j = previousText.Length - 1; j >= 0; j--)
        {
            currentText = previousText.Substring(0, j);
            textBubble.text = currentText;
            yield return new WaitForSeconds(showTextDelay);
        }
        StartCoroutine(ShowText(text));
    }
}

