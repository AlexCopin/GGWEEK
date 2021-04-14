using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySimon : MonoBehaviour
{
    public Dialog dialogGO;
    public Text textSimon;
    public float showSimonDelay = 1.0f;
    bool displaySimon;
    // Start is called before the first frame update
    void Start()
    {
        dialogGO = GameObject.Find("Dialog").GetComponent<Dialog>();
        textSimon = GameObject.Find("TextSimon").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void ShowTextSimon(string[] inputArray)
    {
        StartCoroutine(ShowInputsSimon(inputArray));
    }
    IEnumerator ShowInputsSimon(string[] inputArray)
    {
        for (int i = 0; i < inputArray.Length; i++)
        {
            textSimon.text = inputArray[i];
            yield return new WaitForSeconds(showSimonDelay);
        }
        textSimon.text = "";
    }
}
