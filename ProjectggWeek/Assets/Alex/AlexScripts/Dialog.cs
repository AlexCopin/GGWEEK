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
    GameObject background;
    GameObject Boss;
    PopDialog currentDialog;
    ButtonDisplay buttonDisplayScript;

    int numberOfInputs = Inputs.GetValues(typeof(Inputs)).Length;
    CameraScript cam;
    public AnimationCurve animCurve;
    [Header("Texts")]
    string[][] allStoryArrays;
    public string[] textsIntro;
    public string[] textsStep1; //Linéaire-Tuto
    public string[] textsInterStep0102;
    public string[] textsStep2;//Simon
    public string[] textsInterStep0203;
    public string[] textsAngry;

    bool step1 = false;
    bool step2 = false;

    bool gamePlaying = false; //Game Playing for dialog
    [Header("Dialog values")]
    bool answered = false;
    float delayAnswers;
    public float delayAnswersValue = 3.0f;
    public float delayDialogStoryValue = 2.0f;
    int currentIndexDialog = 0;
    int countStoryDialog = 0;
    int countStoryArray = 0;
    int currentStepGame = 0;
    float delayDialogStory = 0;
    int countSimonStep2 = 0;
    public int countSimonStep2Value = 3;

    public float showTextSpeed = 0.1f;
    public float hideTextSpeed = 0.05f;

    void Start()
    {
        background = GameObject.Find("Background");
        allStoryArrays = new string[3][];
        allStoryArrays[0] = textsIntro;
        allStoryArrays[1] = textsInterStep0102;
        allStoryArrays[2] = textsInterStep0203;
        inputsNeededSimon = new Inputs[countSimonStep2Value];
        inputsLetterSimon = new string[countSimonStep2Value];
        delayAnswers = delayAnswersValue;
        delayDialogStory = delayDialogStoryValue;

        buttonDisplayScript = GameObject.Find("Buttons").GetComponent<ButtonDisplay>();
        buttonDisplayScript.gameObject.SetActive(false);

        step1 = true;

        inputNeeded = (Inputs)Random.Range(0, numberOfInputs);
        buttonDisplayScript.LightLED(Color.red, (int)inputNeeded);

        Boss = GameObject.Find("Boss");
        canvasUI = GameObject.Find("UI");

        cam = GameObject.Find("MainCamera").GetComponent<CameraScript>();
        currentDialog = GameObject.Find("BubbleDialog").GetComponentInChildren<PopDialog>();
        currentDialog.showTextDelay = showTextSpeed;
        currentDialog.hideTextDelay = hideTextSpeed;
        currentDialog.ChangeText(allStoryArrays[countStoryArray][countStoryDialog]);

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
            Boss.GetComponent<Animator>().SetBool("smile", false);
            Boss.GetComponent<Animator>().SetBool("bigsmile", false);
            Boss.GetComponent<Animator>().SetBool("cry", false);
            delayAnswers = delayAnswersValue;
        }

        if(delayDialogStory < 0 && !gamePlaying)
        {
            Boss.GetComponent<Animator>().SetBool("angry", false);
            Boss.GetComponent<Animator>().SetBool("smug", false);
            Boss.GetComponent<Animator>().SetBool("smile", false);
            Boss.GetComponent<Animator>().SetBool("bigsmile", false);
            Boss.GetComponent<Animator>().SetBool("cry", false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StoryText();
            }
        }
        delayDialogStory -= Time.deltaTime;
    }
    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            if(buttonDisplayScript.gameObject.activeSelf)
                buttonDisplayScript.ButtonPushAnim(e.keyCode.ToString());
            //INPUT FOR STEP1
            if (step1)
            {
                switch (inputNeeded)
                {
                    case Inputs.A:
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
                if (Inputs.GetName(typeof(Inputs), inputsNeededSimon[countSimonStep2]) == e.keyCode.ToString() && gamePlaying)
                {
                    TestInputStep2(true);
                }
                else if(Inputs.GetName(typeof(Inputs), inputsNeededSimon[countSimonStep2]) != e.keyCode.ToString() && gamePlaying)
                {
                    TestInputStep2(false);
                }
            }
        }
    }


    public void StoryText() 
    {
        countStoryDialog++;
        delayDialogStory = 1.5f;
        float rand = Random.Range(0, 100);
        if(rand < 33)
        {
            Boss.GetComponent<Animator>().SetBool("angry",true);
            FindObjectOfType<AudioManager>().Play("moodyAngry");
        }
        else if(rand > 66)
        {
            Boss.GetComponent<Animator>().SetBool("bigsmile",true);
            FindObjectOfType<AudioManager>().Play("moodyLaugh2");
        }
        else
        {
            Boss.GetComponent<Animator>().SetBool("smile", true);
            FindObjectOfType<AudioManager>().Play("moodyLaugh");
        }

        if (countStoryDialog >= allStoryArrays[countStoryArray].Length && !answered)
        {
            countStoryArray++;
            currentStepGame++;
            if (currentStepGame == 1 && !gamePlaying)
            {
                step1 = true;
                step2 = false;
                currentDialog.ChangeText(textsStep1[currentIndexDialog]);
                delayAnswersValue = 1.0f;
                buttonDisplayScript.gameObject.SetActive(true);
                buttonDisplayScript.LightLED(Color.red, (int)inputNeeded);
                gamePlaying = true;
            }
            else if (currentStepGame == 2 && !gamePlaying)
            {
                step1 = false;
                step2 = true;
                buttonDisplayScript.gameObject.SetActive(true);
                for (int i = 0; i < inputsNeededSimon.Length; i++)
                {
                    inputsNeededSimon[i] = (Inputs)Random.Range(0, numberOfInputs);
                    inputsLetterSimon[i] = Inputs.GetName(typeof(Inputs), inputsNeededSimon[i]);
                    buttonDisplayScript.LightLED(Color.red, (int)inputsNeededSimon[i]);
                    Debug.Log("Input Simon : " + inputsLetterSimon[i]);
                }
                currentDialog.ChangeText(textsStep2[currentIndexDialog]);
                canvasUI.GetComponent<DisplaySimon>().ShowTextSimon(inputsLetterSimon, inputsNeededSimon);
                gamePlaying = true;
            }
            countStoryDialog = 0;
            return;
        }
        
        currentDialog.ChangeText(allStoryArrays[countStoryArray][countStoryDialog]);
    }
    public void ActivateStory() 
    {
        gamePlaying = false;
        Boss.GetComponent<Animator>().SetBool("bigsmile", true);
        cam.CamZoom(this.gameObject, 7, 2);
        currentIndexDialog = 0;
        Debug.Log(countStoryArray + "       -       " + countStoryDialog);
        currentDialog.ChangeText(allStoryArrays[countStoryArray][countStoryDialog]);
        buttonDisplayScript.gameObject.SetActive(false);
    }
    public void TestInputStep1(bool goodInput)
    {
        if (goodInput && !answered)
        {
            answered = true;
            currentIndexDialog++;
            if (currentIndexDialog >= textsStep1.Length)//GO TO STORY2
            {
                ActivateStory();
                background.GetComponent<Animator>().SetBool("scene2", true);
                step1 = false;
                return;
            }
            FindObjectOfType<AudioManager>().Play("moodyLaugh");
            cam.CamZoom(Boss,4.5f, 1);
            currentDialog.ChangeText(textsStep1[currentIndexDialog]);
            inputNeeded = (Inputs)Random.Range(0, numberOfInputs);
            buttonDisplayScript.LightLED(Color.red, (int)inputNeeded);
            Boss.GetComponent<Animator>().SetBool("smug", true);
        }
        else if(!goodInput && !answered)
        {
            answered = true;
            FindObjectOfType<AudioManager>().Play("buttonError");
            FindObjectOfType<AudioManager>().Play("moodyAngry");
            cam.CamShake(0.3f, 0.1f);
            currentDialog.ChangeText(textsAngry[Random.Range(0, textsAngry.Length)]);
            Boss.GetComponent<Animator>().SetBool("angry", true);
        }
    }

    public void TestInputStep2(bool goodInput)
    {
        if (goodInput && !answered)
        {
            answered = true;
            countSimonStep2++;
            //cam.CamShake(0.4f, 0.1f);
            if (countSimonStep2 >= countSimonStep2Value)
            {
                currentIndexDialog++;
                FindObjectOfType<AudioManager>().Play("moodyLaugh");
                if (currentIndexDialog >= textsStep2.Length)//GO TO STORY3
                {
                    ActivateStory();
                    background.GetComponent<Animator>().SetBool("scene3", true);
                    step2 = false;
                    return;
                }
                cam.CamShake(0.3f, 0.1f);
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
            Boss.GetComponent<Animator>().SetBool("smug", true);
        }
        else if (!goodInput && !answered)
        {
            answered = true;
            FindObjectOfType<AudioManager>().Play("buttonError");
            FindObjectOfType<AudioManager>().Play("moodyAngry");
            countSimonStep2 = 0;
            canvasUI.GetComponent<DisplaySimon>().ShowTextSimon(inputsLetterSimon, inputsNeededSimon);
            cam.CamShake(0.3f, 0.1f);
            currentDialog.ChangeText(textsAngry[Random.Range(0, textsAngry.Length)]);
            Boss.GetComponent<Animator>().SetBool("angry", true);
        }
    }
}
