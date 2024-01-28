using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BaatScriptable" + "Object", order = 1)]
public class BaatScriptableObject : ScriptableObject
{
    [System.Serializable]
    public struct baatStruct
    {
        public Sprite speaker;
        public string baat;
    }

    public List<baatStruct> baatList = new List<baatStruct>();

}
