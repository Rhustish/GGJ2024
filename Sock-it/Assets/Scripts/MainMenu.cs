using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void onPlay()
    {
        Button obj = gameObject.GetComponent<Button> ();
        obj.interactable = false;
        TextMeshProUGUI child = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        child.SetText("Already Began");
    }

    public void onContinue(){
        SceneManager.LoadScene("Level-Main",LoadSceneMode.Single);
    }

    public void onQuit()
    {
        // Debug.Log("I'm OUt");
        Application.Quit();
    }
}