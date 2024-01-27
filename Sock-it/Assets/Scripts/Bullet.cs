using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        StartCoroutine(bultdie());

    }

    void OnCollisionEnter2D(Collision2D obj){
        if(obj.gameObject.CompareTag("CockRoach")){
            CockBro c = obj.gameObject.GetComponent<CockBro>();
            c.takeDamage();
            Destroy(gameObject);
        }
    }

    IEnumerator bultdie()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
