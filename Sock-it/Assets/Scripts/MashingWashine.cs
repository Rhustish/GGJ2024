using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MashingWashine : MonoBehaviour
{

    private Rigidbody2D rb;
    // public GameObject player;
    public GameObject detergent;

    struct MachineFactory
    {
        public float health;
        public bool isAttacking;

        public bool isMoving;

    }

    MachineFactory bhai;

    //is bhai ke attacks

    //spits detergent --done
    //rumbledry --done
    //touchmenot --done


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass=100;
        bhai.health = 200;
        // player = GameObject.FindGameObjectWithTag("Player");
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
        GameObject newDet = Instantiate(detergent, gameObject.transform.GetChild(0).position, Quaternion.identity);
        // Debug.Log(transform.position.);
        Rigidbody2D newDetRb = newDet.GetComponent<Rigidbody2D>();
        newDetRb.velocity = new Vector2(Random.Range(-5f,-2f),Random.Range(5f,10f));
    }

    void move(){
        rb.velocity = new Vector2(5*Random.Range(-1.0f,1.0f),5*Random.Range(-1.0f,1.0f));
    }
    
    public void dies(){
        Debug.Log("yahoooooooooooooooooo");
        StopAllCoroutines();
        bhai.isAttacking = true;
        bhai.isMoving = true;
        rb.velocity = new Vector2(0,0);
        StartCoroutine(deathBuffer());
        gameObject.tag = "MachineAfterDeath";
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
        yield return new WaitForSeconds(2);
        bhai.isMoving = false;
    }
    IEnumerator deathBuffer(){
        //dead animation;
        yield return new WaitForSeconds(5);
        // Destroy(gameObject);
    }
}




