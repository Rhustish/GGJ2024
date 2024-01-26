using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [System.Serializable]
    struct PlayerFactory
    {
        public float playerSpeed;
        public float jumpForce;
        public bool isGrounded;
    }

    [SerializeField]PlayerFactory moja;

    private Rigidbody2D rb;
    private float horizontal;

    public float rayCastLength = 1.2f;
    public LayerMask groundLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if(horizontal!=0)
        {
            if(moja.isGrounded){
                rb.velocity = new Vector2(x: horizontal* moja.playerSpeed, y: moja.jumpForce);
            }else{
                rb.velocity = new Vector2(x: horizontal * moja.playerSpeed, y: rb.velocity.y);
            }
  
            
            
        }
        
        
        //Debug.Log(horizontal);

        if (Input.GetKeyDown(KeyCode.Space) && moja.isGrounded)
        {
            rb.velocity = new Vector2(x: rb.velocity.x, y: moja.jumpForce*2);
        }

        moja.isGrounded = Physics2D.Raycast(origin: transform.position, direction: Vector2.down, distance: rayCastLength, groundLayerMask);

    }
    void OnDrawGizmos(){
        Debug.DrawRay(transform.position, Vector2.down * rayCastLength, Color.green);
    }
}