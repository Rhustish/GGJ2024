using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public delegate void TakeDamage(int damage);
    public static event TakeDamage onTakeDamage;

    public delegate void Heal(int heal);
    public static event Heal onHeal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void OnTakeDamage(int damage)
    {
        if(onTakeDamage != null)
        {
            onTakeDamage(damage);
        }
    }

    public static void OnHeal(int heal)
    {
        if(onHeal != null)
        {
            onHeal(heal);
        }
    }

}
