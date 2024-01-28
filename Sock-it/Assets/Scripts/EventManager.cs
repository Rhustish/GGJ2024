using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public delegate void TakeDamage(int damage);
    public static event TakeDamage onTakeDamage;

    public delegate void Heal(int heal);
    public static event Heal onHeal;

    public delegate void PlayerDeath();
    public static event PlayerDeath onPlayerDeath;

    public delegate void Interact(GameManager.Interaction interaction);
    public static event Interact onInteract;

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
        if (onHeal != null)
        {
            onHeal(heal);
        }
    }

    public static void OnPlayerDeath()
    {
        if (onPlayerDeath != null)
        {
            onPlayerDeath();
        }
    }

    public static void OnInteract(GameManager.Interaction interaction)
    {
        if (onInteract != null)
        {
            onInteract(interaction);
        }
    }

}
