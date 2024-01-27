using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class RPS : MonoBehaviour
{

    public int strikeCount = 1;
    public int pointCount = 1;

    // Start is called before the first frame update

    void OnEnable()
    {
        
        for(int i = 1 ; i < 4 ; i++){
            GameObject temp = GameObject.Find("Strike"+Convert.ToString(i));

            temp.SetActive(false);
        }
        for(int i = 1 ; i < 4 ; i++){
            GameObject temp = GameObject.Find("Point"+Convert.ToString(i));

            temp.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(strikeCount == 4){
            SceneManager.UnloadSceneAsync("RockPaperScissors");
            SceneManager.LoadScene("RockPaperScissors",LoadSceneMode.Additive);
        }
        if(pointCount == 4){
            SceneManager.UnloadSceneAsync("RockPaperScissors");
        }
    }
    #region user fucntions

    public void rock(){
        Transform Strike = transform.Find("Point"+Convert.ToString(pointCount));
        Strike.gameObject.SetActive(true);
        pointCount++;
    }
    public void paperOrScissors(){
        Transform Strike = transform.Find("Strike"+Convert.ToString(strikeCount));
        Strike.gameObject.SetActive(true);
        strikeCount++;
    }

    #endregion
}
