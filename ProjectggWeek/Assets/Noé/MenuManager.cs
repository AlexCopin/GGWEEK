using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Game");
        print("launch game");
    }

    public void Quit()
    {
        Application.Quit();
        print("quit game");
    }
}
