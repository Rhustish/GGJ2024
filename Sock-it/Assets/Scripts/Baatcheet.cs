using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    private Action DeactivateCall = null;
    private AudioClip voiceLine = null;

    private Sprite speaker = null;

    public Baat(string baat, Sprite bolnewaala, AudioClip voiceAudio, Action deactivateCallBack, Action callback = null)
    {
        onActionCallback = callback;
        currentBaat = baat;
        speaker = bolnewaala;
        DeactivateCall = deactivateCallBack;
        voiceLine = voiceAudio;
    }

    public void Callback()
    {
        if(onActionCallback != null)
        {
            onActionCallback();
        }
    }

    public void DeactivateCallBack()
    {
        if (DeactivateCall != null)
        {
            DeactivateCall();
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

    public Sprite GetSpeaker()
    {
           return speaker;
    }

    public AudioClip GetVoiceLine()
    {
        return voiceLine;
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer += timerPerChar;
            charIndex++;
            if (currentBaat != null)
            {
                if (charIndex <= currentBaat.Length)
                {
                    displayBaat = currentBaat[..charIndex];
                    displayBaat += "<color=black>" + displayBaat[charIndex..] + "</color>";
                }
            }
            else if(charIndex >= currentBaat.Length)
            {
                Callback();
                currentBaat = null;
                charIndex = 0;
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
    public Image speakerIcon;

    private static Baatcheet instance = null;
    private List<Baat> baatein = new();

    private Baat currentBaat = null;
    private int baatIndex = 0;

    //private Sprite speaker = null;

    private void OnEnable()
    {
        EventManager.onEndInteraction += Deactivate;
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
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

    private void OnDisable()
    {
        EventManager.onEndInteraction -= Deactivate;
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
            EventManager.OnEndInteraction();
            return;
        }

        // display speaker
        speakerIcon.sprite = baatein[baatIndex].GetSpeaker();
        currentBaat = baatein[baatIndex];
        GameManager.Instance.ChangeVoiceClip(currentBaat.GetVoiceLine());

        // check what baat is going on
        switch (currentBaat.GetFullbaat())
        {
            case "Are you ready?":
                GameManager.Instance.ChangeAudio(GameManager.Instance.audioClips[2]);
                break;
            case "places rock":
                GameManager.Instance.ChangeAudio(GameManager.Instance.audioClips[7]);
                break;

            case "Nothing…nothing…":
                //yield return new WaitForSeconds(2f);
                StartCoroutine(stopAndPlayEndScene());
                break;
        }
    }

    IEnumerator stopAndPlayEndScene()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.comicAnimator.SetBool("isEnd", true);
        //GameManager.Instance.ChangeAudio(GameManager.Instance.audioClips[8]);
    }

    public static void Add(BaatScriptableObject scrBaat)
    {
        for(int i = 0; i < scrBaat.baatList.Count; i++)
        {
            Baat typeBaat = new Baat(scrBaat.baatList[i].baat, scrBaat.baatList[i].speaker,scrBaat.baatList[i].audioClip, Deactivate);
            instance.baatein.Add(typeBaat);
        }
    }

    public static void Deactivate()
    {
        instance.baatein.Clear();
        instance.transform.GetChild(0).gameObject.SetActive(false);
        instance.transform.GetChild(1).gameObject.SetActive(false);
        instance.transform.GetChild(2).gameObject.SetActive(false);
        instance.currentBaat = null;
        instance.speakerIcon.sprite = null;
        instance.baatIndex = 0;
        GameManager.Instance.ChangeAudioToDefault();
        GameManager.Instance.RemoveVoiceClip();
    }

    public static void Activate()
    {
        instance.transform.GetChild(0).gameObject.SetActive(true);
        instance.transform.GetChild(1).gameObject.SetActive(true);
        instance.transform.GetChild(2).gameObject.SetActive(true);
        instance.currentBaat = instance.baatein[0];
        instance.speakerIcon.sprite = instance.baatein[0].GetSpeaker();
        GameManager.Instance.ChangeVoiceClip(instance.baatein[0].GetVoiceLine());
    }

}
