using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySimon : MonoBehaviour
{
    public Dialog dialogGO;
    public float showSimonDelay = 1.0f;
    public ButtonDisplay buttonDisplayScript;
    // Start is called before the first frame update
    void Start()
    {
        buttonDisplayScript = GameObject.Find("Buttons").GetComponent<ButtonDisplay>();
        dialogGO = GameObject.Find("Dialog").GetComponent<Dialog>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void ShowTextSimon(string[] inputArray, Dialog.Inputs[] inputs)
    {
        StartCoroutine(ShowInputsSimon(inputArray, inputs));
    }
    IEnumerator ShowInputsSimon(string[] inputArray, Dialog.Inputs[] inputs)
    {
        for (int i = 0; i < inputArray.Length; i++)
        {
            buttonDisplayScript.LightLED(Color.red, (int)inputs[i]);
            yield return new WaitForSeconds(showSimonDelay);
        }
    }
}
