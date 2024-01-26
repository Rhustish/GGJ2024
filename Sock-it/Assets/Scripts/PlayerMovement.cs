using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
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

        public bool isHurting;
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
        moja.isHurting = false;
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
        if(moja.health == 0){
            StartCoroutine(death());
        }

        // giving damage to player
        //GameManager.Instance.playerMov.isTakeingDamage = true;


        horizontal = Input.GetAxis("Horizontal");
        if(horizontal!=0 && !moja.isHurting)
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

        if (Input.GetKeyDown(KeyCode.Space) && moja.isGrounded && !moja.isHurting)
        {
            rb.velocity = new Vector2(x: rb.velocity.x, y: moja.jumpForce*2);
        }

        moja.isGrounded = Physics2D.Raycast(origin: transform.position, direction: Vector2.down, distance: rayCastLength, groundLayerMask);

    }

    void OnCollisionEnter2D(Collision2D obj){

        if(obj.gameObject.CompareTag("Nail")){

            StartCoroutine(cantMove());
            GameManager.Instance.playerMov.isTakeingDamage = true;   
            Vector2 prev = rb.velocity;
            rb.velocity = new Vector2(0,0);
            rb.velocity = new Vector2(prev.x>0?5:-5,prev.y);

        }
        // Debug.Log(obj.gameObject.name);
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

    IEnumerator cantMove(){
        moja.isHurting = true;
        yield return new WaitForSeconds(2);
        moja.isHurting = false;
    }
    IEnumerator death(){
        moja.isHurting = true;
        //ded ho gaya animation
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }    
    
    #endregion

}