using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    #region Variables

    [System.Serializable]
    struct PlayerFactory
    {
        public float speed;
        public float jumpForce;
        public bool isGrounded;
        public int health;
    }

    [SerializeField]PlayerFactory moja;

    private Rigidbody2D rb;
    private float horizontal;

    public float rayCastLength = 1.2f;
    public LayerMask groundLayerMask;

    public bool isTakeingDamage;

    #endregion


    #region Unity callbacks

    private void OnEnable()
    {
        EventManager.onTakeDamage += TakeDamage;
        EventManager.onHeal += Heal;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isTakeingDamage = false;
        moja.health = 100;

    }

    // Update is called once per frame
    void Update()
    {

        // giving damage to player
        //GameManager.Instance.playerMov.isTakeingDamage = true;


        horizontal = Input.GetAxis("Horizontal");
        if(horizontal!=0)
        {
            if(moja.isGrounded){
                rb.velocity = new Vector2(x: horizontal* moja.speed, y: moja.jumpForce);
            }else{
                rb.velocity = new Vector2(x: horizontal * moja.speed, y: rb.velocity.y);
            }
        }

        if (isTakeingDamage)
        {
            EventManager.OnTakeDamage(10);
            isTakeingDamage = false;
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

    void OnDisable()
    {
        EventManager.onTakeDamage -= TakeDamage;
        EventManager.onHeal -= Heal;
    }

    #endregion


    #region Custom methods
   
    public void TakeDamage(int damage)
    {
        moja.health -= damage;
    }

    public void Heal(int heal)
    {
        moja.health += heal;
    }
        
    #endregion

}