using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Dialog : MonoBehaviour
{
    enum Inputs {
        A,
        Z,
        E,
        R,
        Q,
        S
    }
    Inputs inputNeeded;
    int numberOfInputs = Inputs.GetValues(typeof(Inputs)).Length;
    CameraScript cam;
    public GameObject Player;
    public GameObject Boss;
    bool answered = false;
    float delayAnswers = 2.0f;

    public AnimationCurve animCurve;
    public string[] textsQuestions;
    public string[] textsAngry;
    int currentIndexDialog = 0;
    public float showTextSpeed = 0.1f;
    PopDialog currentDialog;

    public GameObject dialogPrefab;

    void Start()
    {
        inputNeeded = (Inputs)Random.Range(0, numberOfInputs);
        Player = GameObject.Find("Player");
        Boss = GameObject.Find("Boss");

        Debug.Log(Inputs.GetName(typeof(Inputs), inputNeeded));
        cam = GameObject.Find("MainCamera").GetComponent<CameraScript>();
        currentDialog = Instantiate<GameObject>(dialogPrefab).GetComponentInChildren<PopDialog>();
        currentDialog.showTextDelay = showTextSpeed;
        currentDialog.firstText = textsQuestions[0];

    }


    void Update()
    {
        if (answered) 
        {
            delayAnswers -= Time.deltaTime;
        }
        if(delayAnswers <= 0)
        {
            answered = false;
            delayAnswers = 2.0f;
        }
    }
    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            if ( Inputs.GetName(typeof(Inputs), inputNeeded) == e.keyCode.ToString()) 
            {
                TestInput(true);
            }
            else 
            {
                TestInput(false);
            }
            //Debug.Log("Detected key code: " + e.keyCode);
        }
    }
    public void TestInput(bool goodInput)
    {

        if (goodInput && !answered)
        {
            answered = true;
            currentIndexDialog++;
            cam.CamZoom(Player, 2, 4);
            currentDialog.ChangeText(textsQuestions[currentIndexDialog]);
            inputNeeded = (Inputs)Random.Range(0, numberOfInputs);
            Debug.Log(Inputs.GetName(typeof(Inputs), inputNeeded));
        }
        else if(!goodInput && !answered)
        {
            answered = true;
            cam.CamShake(0.5f, 0.2f);
            currentDialog.ChangeText(textsAngry[Random.Range(0, textsAngry.Length)]);
        }
    }
}
