using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void onPlay()
    {
        // Debug.Log("ho gya");
        SceneManager.LoadScene("Level");
    }

    public void onQuit()
    {
        // Debug.Log("I'm OUt");
        Application.Quit();
    }
}