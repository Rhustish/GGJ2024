using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BaatScriptable" + "Object", order = 1)]
public class BaatScriptableObject : ScriptableObject
{
    public List<Baat> baatein = new List<Baat>();
}
