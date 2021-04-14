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
        D,
        Q,
        S
    }
    Inputs inputNeeded;
    int numberOfInputs = Inputs.GetValues(typeof(Inputs)).Length;
    CameraScript cam;
    public GameObject Boss;
    bool answered = false;
    float delayAnswers = 2.0f;

    public AnimationCurve animCurve;
    public string[] textsStep1; //Linéaire-Tuto
    public string[] textsStep2;//Simon
    public string[] textsStep3;//Questions Meta
    public string[] textsAngry;

    bool step1 = false;
    bool step2 = false;
    bool step3 = false;
    int currentIndexDialog = 0;
    public float showTextSpeed = 0.1f;
    public float hideTextSpeed = 0.05f;
    PopDialog currentDialog;

    public GameObject dialogPrefab;

    void Start()
    {
        step1 = true;
        inputNeeded = (Inputs)Random.Range(0, numberOfInputs);
        Boss = GameObject.Find("Boss");

        Debug.Log(Inputs.GetName(typeof(Inputs), inputNeeded));
        cam = GameObject.Find("MainCamera").GetComponent<CameraScript>();
        currentDialog = Instantiate<GameObject>(dialogPrefab).GetComponentInChildren<PopDialog>();
        currentDialog.showTextDelay = showTextSpeed;
        currentDialog.hideTextDelay = hideTextSpeed;
        currentDialog.firstText = textsStep1[0];
        currentDialog.ChangeText(textsStep1[currentIndexDialog]);

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
            Boss.GetComponent<Animator>().SetBool("angry", false);
            Boss.GetComponent<Animator>().SetBool("smug", false);
            delayAnswers = 3.0f;
        }
    }
    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            if ( Inputs.GetName(typeof(Inputs), inputNeeded) == e.keyCode.ToString()) 
            {
                if(step1)
                    TestInputStep1(true);
                if (step2) 
                {
                    TestInputStep2(true);
                }
                if (step3)
                {

                }
            }
            else
            {
                if(step1)
                    TestInputStep1(false);
                if (step2)
                {
                    TestInputStep2(false);
                }
                if (step3)
                {

                }
            }
            //Debug.Log("Detected key code: " + e.keyCode);
        }
    }
    public void TestInputStep1(bool goodInput)
    {
        if (goodInput && !answered)
        {
            answered = true;
            currentIndexDialog++;
            cam.CamZoom(Boss, 4, 2);
            currentDialog.ChangeText(textsStep1[currentIndexDialog]);
            inputNeeded = (Inputs)Random.Range(0, numberOfInputs);
            Debug.Log(Inputs.GetName(typeof(Inputs), inputNeeded));
            Boss.GetComponent<Animator>().SetBool("angry", true);
        }
        else if(!goodInput && !answered)
        {
            answered = true;
            cam.CamShake(0.5f, 0.2f);
            currentDialog.ChangeText(textsAngry[Random.Range(0, textsAngry.Length)]);
            Boss.GetComponent<Animator>().SetBool("smug", true);
        }
    }

    public void TestInputStep2(bool goodInput)
    {
        if (goodInput && !answered)
        {
            answered = true;
            currentIndexDialog++;
            cam.CamZoom(Boss, 4, 2);
            currentDialog.ChangeText(textsStep1[currentIndexDialog]);
            inputNeeded = (Inputs)Random.Range(0, numberOfInputs);
            Debug.Log(Inputs.GetName(typeof(Inputs), inputNeeded));
            Boss.GetComponent<Animator>().SetBool("angry", true);
        }
        else if (!goodInput && !answered)
        {
            answered = true;
            cam.CamShake(0.5f, 0.2f);
            currentDialog.ChangeText(textsAngry[Random.Range(0, textsAngry.Length)]);
            Boss.GetComponent<Animator>().SetBool("smug", true);
        }
    }
}
