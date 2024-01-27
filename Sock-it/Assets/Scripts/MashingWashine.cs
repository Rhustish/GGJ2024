using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MashingWashine : MonoBehaviour
{

    private Rigidbody2D rb;
    struct MachineFactory
    {
        public float health;
        public bool isAttacking;

        public bool isMoving;

    }

    MachineFactory bhai;

    //is bhai ke attacks

    //spits detergent
    //rumbledry
    //touchmenot


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bhai.health = 200;
        bhai.isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!bhai.isAttacking ){
            float atk = Random.Range(0.0f,2.0f);
            if(atk>1){
                rumbledry();
            }else{
                thew();
            }
            StartCoroutine(attackBuffer());
        }

        if(!bhai.isMoving){
            move();
            StartCoroutine(moveBuffer());
        }

    }

    void thew(){
        //buttel
        Debug.Log("asdf");
    }

    void move(){
        rb.velocity = new Vector2(5*Random.Range(-1.0f,1.0f),5*Random.Range(-1.0f,1.0f));
    }


    void rumbledry(){


        PlayerMovement pscript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        pscript.Stagger();
    }

    IEnumerator attackBuffer()
    {
        bhai.isAttacking = true;
        yield return new WaitForSeconds(3);
        bhai.isAttacking = false;

    }

    IEnumerator moveBuffer(){
        bhai.isMoving = true;
        yield return new WaitForSeconds(5);
        bhai.isMoving = false;
    }
}




