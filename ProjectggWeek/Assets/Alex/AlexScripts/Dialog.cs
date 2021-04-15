using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Dialog : MonoBehaviour
{
    public enum Inputs {
        A,
        Z,
        E,
        Q,
        S,
        D
    }
    Inputs inputNeeded;
    Inputs[] inputsNeededSimon;
    public string[] inputsLetterSimon;
    GameObject canvasUI;
    GameObject Boss;
    PopDialog currentDialog;
    ButtonDisplay buttonDisplayScript;

    int numberOfInputs = Inputs.GetValues(typeof(Inputs)).Length;
    CameraScript cam;
    public AnimationCurve animCurve;
    [Header("Texts")]
    public string[] textsIntro;
    public string[] textsStep1; //Linéaire-Tuto
    public string[] textsInterStep0102;
    public string[] textsStep2;//Simon
    public string[] textsInterStep0203;
    public string[] textsStep3;//Questions Meta
    public string[] textsAngry;
    string[] currentTextsStory;

    bool step1 = false;
    bool step2 = false;
    bool step3 = false;

    bool gamePlaying = false; //Game Playing for dialog
    [Header("Dialog values")]
    bool answered = false;
    float delayAnswers;
    public float delayAnswersValue = 3.0f;
    int currentIndexDialog = 0;
    int countStoryDialog = 0;
    float delayDialogStory = 1.5f;
    int countSimonStep2 = 0;
    public int countSimonStep2Value = 3;

    public float showTextSpeed = 0.1f;
    public float hideTextSpeed = 0.05f;

    void Start()
    {
        currentTextsStory = textsIntro;
        inputsNeededSimon = new Inputs[countSimonStep2Value];
        inputsLetterSimon = new string[countSimonStep2Value];
        delayAnswers = delayAnswersValue;

        buttonDisplayScript = GameObject.Find("Buttons").GetComponent<ButtonDisplay>();

        step1 = true;

        inputNeeded = (Inputs)Random.Range(0, numberOfInputs);
        buttonDisplayScript.LightLED(Color.red, (int)inputNeeded);

        Boss = GameObject.Find("Boss");
        canvasUI = GameObject.Find("UI");

        cam = GameObject.Find("MainCamera").GetComponent<CameraScript>();
        currentDialog = GameObject.Find("BubbleDialog").GetComponentInChildren<PopDialog>();
        currentDialog.showTextDelay = showTextSpeed;
        currentDialog.hideTextDelay = hideTextSpeed;
        currentDialog.ChangeText(currentTextsStory[countStoryDialog]);

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
            delayAnswers = delayAnswersValue;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !gamePlaying && delayDialogStory < 0) 
        {
            countStoryDialog++;
            delayDialogStory = 1.5f;
        }
        delayDialogStory -= Time.deltaTime;
    }
    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            
            //INPUT FOR STEP1
            if (step1)
            {
                switch (inputNeeded)
                {
                    case Inputs.A:
                        if (step1) 
                        {

                        }
                            break;
                    case Inputs.Z:
                        break;
                    case Inputs.E:
                        break;
                    case Inputs.Q:
                        break;
                    case Inputs.S:
                        break;
                    case Inputs.D:
                        break;
                }
                if (Inputs.GetName(typeof(Inputs), inputNeeded) == e.keyCode.ToString() && gamePlaying)
                {
                    TestInputStep1(true);

                }
                else if(Inputs.GetName(typeof(Inputs), inputNeeded) != e.keyCode.ToString() && gamePlaying)
                {
                    TestInputStep1(false);
                }
            }

            //INPUT FOR STEP2
            if (step2)
            {
                switch (inputsNeededSimon[countSimonStep2])
                {
                    case Inputs.A:
                        if (step1)
                        {

                        }
                        break;
                    case Inputs.Z:
                        break;
                    case Inputs.E:
                        break;
                    case Inputs.Q:
                        break;
                    case Inputs.S:
                        break;
                    case Inputs.D:
                        break;
                }
                if (Inputs.GetName(typeof(Inputs), inputsNeededSimon[countSimonStep2]) == e.keyCode.ToString())
                {
                    TestInputStep2(true);
                }
                else
                {
                    TestInputStep2(false);
                }
            }

            //INPUT FOR STEP3
            if (step3)
            {
                if (Inputs.GetName(typeof(Inputs), inputsNeededSimon[countSimonStep2]) == e.keyCode.ToString())
                {
                    TestInputStep3(true);
                }
                else
                {
                    TestInputStep3(false);
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
            if (currentIndexDialog >= textsStep1.Length)//GO TO STEP2
            {
                cam.CamZoom(this.gameObject, 7, 2);
                step1 = false;
                step2 = true;
                currentIndexDialog = 0;
                currentDialog.ChangeText(textsStep2[currentIndexDialog]);
                for(int i = 0; i < inputsNeededSimon.Length; i++)
                {
                    inputsNeededSimon[i] = (Inputs)Random.Range(0, numberOfInputs);
                    inputsLetterSimon[i] = Inputs.GetName(typeof(Inputs), inputsNeededSimon[i]);
                    buttonDisplayScript.LightLED(Color.red, (int)inputsNeededSimon[i]);
                    Debug.Log("Input Simon : " + inputsLetterSimon[i]);
                }
                delayAnswersValue = 1.0f;
                canvasUI.GetComponent<DisplaySimon>().ShowTextSimon(inputsLetterSimon, inputsNeededSimon);
                return;
            }
            cam.CamZoom(Boss,4.5f, 1);
            currentDialog.ChangeText(textsStep1[currentIndexDialog]);
            inputNeeded = (Inputs)Random.Range(0, numberOfInputs);
            buttonDisplayScript.LightLED(Color.red, (int)inputNeeded);
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
            countSimonStep2++;
            cam.CamShake(0.4f, 0.1f);
            if (countSimonStep2 >= countSimonStep2Value)
            {
                currentIndexDialog++;
                cam.CamShake(0.3f, 0.1f);
                //cam.CamZoom(buttonDisplayScript.gameObject, 4, 2);
                for (int i = 0; i < inputsNeededSimon.Length; i++)
                {
                    inputsNeededSimon[i] = (Inputs)Random.Range(0, numberOfInputs);
                    inputsLetterSimon[i] = Inputs.GetName(typeof(Inputs), inputsNeededSimon[i]);
                    Debug.Log("Input Simon : " + inputsLetterSimon[i]);
                }
                countSimonStep2 = 0;
                currentDialog.ChangeText(textsStep2[currentIndexDialog]);
                canvasUI.GetComponent<DisplaySimon>().ShowTextSimon(inputsLetterSimon, inputsNeededSimon);
            }
            Boss.GetComponent<Animator>().SetBool("angry", true);
        }
        else if (!goodInput && !answered)
        {
            answered = true;
            countSimonStep2 = 0;
            canvasUI.GetComponent<DisplaySimon>().ShowTextSimon(inputsLetterSimon, inputsNeededSimon);
            cam.CamShake(0.5f, 0.2f);
            currentDialog.ChangeText(textsAngry[Random.Range(0, textsAngry.Length)]);
            Boss.GetComponent<Animator>().SetBool("smug", true);
        }
    }

    public void TestInputStep3(bool goodInput)
    {
        if (goodInput && !answered)
        {
            answered = true;
            if (countSimonStep2 >= countSimonStep2Value)
            {
                currentIndexDialog++;
                cam.CamZoom(Boss, 4, 2);
                currentDialog.ChangeText(textsStep2[currentIndexDialog]);
            }
            else
            {
                countSimonStep2++;
            }
            inputNeeded = (Inputs)Random.Range(0, numberOfInputs);
            Debug.Log(Inputs.GetName(typeof(Inputs), inputNeeded));
            Boss.GetComponent<Animator>().SetBool("angry", true);
        }
        else if (!goodInput && !answered)
        {
            answered = true;
            countSimonStep2 = 0;
            cam.CamShake(0.5f, 0.2f);
            currentDialog.ChangeText(textsAngry[Random.Range(0, textsAngry.Length)]);
            Boss.GetComponent<Animator>().SetBool("smug", true);
        }
    }
}
