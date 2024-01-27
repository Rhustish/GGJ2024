using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Baat
{
    private float timer = 0;
    private int charIndex = 0;
    private float timerPerChar = 0.05f;

    [SerializeField]
    private string currentBaat = null;
    private string displayBaat = null;

    private Action onActionCallback = null;

    public Baat(string baat, Action callback = null)
    {
        onActionCallback = callback;
        currentBaat = baat;
    }

    public void Callback()
    {
        if(onActionCallback != null)
        {
            onActionCallback();
        }
    }

    public string GetFullBaatAndCallback()
    {
        if(onActionCallback != null)
        {
            onActionCallback();
        }
        return currentBaat;
    }

    public string GetFullbaat()
    {
        return currentBaat;
    }

    public string GetDisplayBaat()
    {
        return displayBaat;
    }

    public void Update()
    {
        if(string.IsNullOrEmpty(currentBaat))
        {
            return;
        }

        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer += timerPerChar;
            charIndex++;
            if(charIndex <= currentBaat.Length)
            {
                displayBaat = currentBaat[..charIndex];
                displayBaat += "<color=black>" + displayBaat[charIndex..] + "</color>";
            }
            if(charIndex >= currentBaat.Length)
            {
                Callback();
                currentBaat = null;
            }
        }

    }

    public bool IsActive()
    {
        if(string.IsNullOrEmpty(currentBaat))
        {
            return false;
        }
        return charIndex < currentBaat.Length;
    }

}

public class Baatcheet : MonoBehaviour
{

    public TextMeshProUGUI baatText;

    private static Baatcheet instance = null;
    private List<Baat> baatein = new();

    private Baat currentBaat = null;
    private int baatIndex = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(baatein.Count > 0 && currentBaat != null)
        {
            currentBaat.Update();
            baatText.text = currentBaat.GetDisplayBaat();
        }
    }


    public void WriteNextBaatInQueue()
    {
        if(currentBaat != null && currentBaat.IsActive())
        {
            baatText.text = currentBaat.GetFullBaatAndCallback();
            currentBaat = null;
            return;
        }

        baatIndex++;

        if(baatIndex >= baatein.Count)
        {
            currentBaat = null;
            baatText.text = "";
            return;
        }

        currentBaat = baatein[baatIndex];

    }

    public static void Add(BaatScriptableObject scrBaat)
    {
        for(int i = 0; i < scrBaat.baatein.Count; i++)
        {
            Baat typeBaat = new Baat(scrBaat.baatein[i].GetFullbaat());
            instance.baatein.Add(typeBaat);
        }
    }

    public static void Activate()
    {
        instance.currentBaat = instance.baatein[0];
    }

}
