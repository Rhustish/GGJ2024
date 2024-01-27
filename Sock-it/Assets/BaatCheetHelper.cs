using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaatCheetHelper : MonoBehaviour
{

    public BaatScriptableObject baatScriptableObject;

    // Start is called before the first frame update
    void Start()
    {
        Baatcheet.Add(baatScriptableObject);
        Baatcheet.Activate();
    }
}
