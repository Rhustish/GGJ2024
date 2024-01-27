using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Detergent : MonoBehaviour
{
    private bool isGrounded; 
    public LayerMask groundLayerMask;
    void Start()
    {
        isGrounded = false;
    }
    void Update(){
        isGrounded = Physics2D.Raycast(origin: transform.position, direction: Vector2.down, distance: 0.6f, groundLayerMask);
        if(isGrounded){
            StartCoroutine(ohnoitdie());
        }
    }
    void OnDrawGizmos(){
        Debug.DrawRay(transform.position,Vector2.down*0.6f,color: Color.blue);  
    }
    IEnumerator ohnoitdie(){
        yield return new WaitForSeconds(5);
        StopAllCoroutines();
        Destroy(gameObject);
    }

}
