using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class CockBro : MonoBehaviour
{

    public GameObject player;
    public PlayerMovement pscript;

    // public bool fyooming;4
    // public float cockHealth;

    public float cockRange;
    public float distanceFromMoja;
    public float modist;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pscript = player.GetComponent<PlayerMovement>();
        cockRange = 8;
        // cockHealth = 1;
        // fyooming = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromMoja = transform.position.x - player.transform.position.x;
        modist = distanceFromMoja < 0 ? distanceFromMoja * -1 : distanceFromMoja;
        if (modist <= cockRange)
        {
            fyoooom();
        }
    }

    void fyoooom()
    {
        // fyooming = true;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 5 * Time.deltaTime);
    }

    public void takeDamage()
    {
        Destroy(gameObject);
    }
}
