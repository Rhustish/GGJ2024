using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;
    public float playerSpeed = 2;
    public float jumpForce = 2;
    public bool isGrounded = true;
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
        rb.velocity = new Vector2(x: horizontal * playerSpeed, y: rb.velocity.y);
        horizontal = Input.GetAxis("Horizontal");
        //Debug.Log(horizontal);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(x: rb.velocity.x, y: jumpForce);
        }

        isGrounded = Physics2D.Raycast(origin: transform.position, direction: Vector2.down, distance: rayCastLength, groundLayerMask);

    }
    void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector2.down * rayCastLength, Color.green);
    }
}