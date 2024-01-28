using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    #region Variables

    [System.Serializable]
    struct PlayerFactory
    {
        public int suidhaga;
        public bool inPlug  ;

        public int cheese;
        public int stone;
        public float speed;
        public float jumpForce;
        public bool isGrounded;
        public int health;

        public bool isHurting;
    }

    [SerializeField] PlayerFactory moja;

    private Rigidbody2D rb;
    private float horizontal;

    public float rayCastLength = 1.2f;
    public LayerMask groundLayerMask;

    public bool isTakeingDamage;

    public Camera mainLevelCamera;

    public SpriteRenderer mojaSprite;

    public Animator mojaAnimator;

    #endregion


    #region Unity callbacks

    private void OnEnable()
    {
        EventManager.onTakeDamage += TakeDamage;
        EventManager.onHeal += Heal;
        EventManager.onPlayerDeath += DeathFun;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isTakeingDamage = false;
        moja.health = 100;
        moja.isHurting = false;
        moja.cheese = 0;
        moja.inPlug = false;
        moja.stone = 0;
        mojaSprite = GetComponent<SpriteRenderer>();
        mojaAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.Instance.playerState == GameManager.PlayerState.Alive && GameManager.Instance.gameState == GameManager.GameState.Playing)
        {
            if (moja.inPlug && Input.GetKeyDown(KeyCode.Q))
            {
                killMachine();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadScene("RockPaperScissors", LoadSceneMode.Additive);

            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                // StopCoroutine(escapeRoutine());
                StopAllCoroutines();
                moja.isHurting = false;
            }

            // Debug.Log(moja.suidhaga);
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (moja.suidhaga > 0)
                {
                    EventManager.OnHeal(25);
                    moja.suidhaga--;
                }
            }

            if (moja.health <= 0 && GameManager.Instance.playerState != GameManager.PlayerState.Dead)
            {
                EventManager.OnPlayerDeath();
            }

            // giving damage to player
            //GameManager.Instance.playerMov.isTakeingDamage = true;

            horizontal = Input.GetAxis("Horizontal");
            if (horizontal != 0 && !moja.isHurting)
            {
                if (horizontal > 0)
                {
                    mojaSprite.flipX = false;
                }
                else
                {
                    mojaSprite.flipX = true;
                }
                mojaAnimator.SetBool("isMoving", true);
                if (moja.isGrounded)
                {
                    rb.velocity = new Vector2(x: horizontal * moja.speed, y: moja.jumpForce);
                }
                else
                {
                    rb.velocity = new Vector2(x: horizontal * moja.speed, y: rb.velocity.y);
                }
            }
            else if (horizontal == 0)
            {
                mojaAnimator.SetBool("isMoving", false);
            }

            if (isTakeingDamage)
            {
                EventManager.OnTakeDamage(10);
                isTakeingDamage = false;
            }

            //Debug.Log(horizontal);

            if (Input.GetKeyDown(KeyCode.Space) && moja.isGrounded && !moja.isHurting)
            {
                mojaAnimator.SetBool("isHighJump", true);
                rb.velocity = new Vector2(x: rb.velocity.x, y: moja.jumpForce * 2);
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                mojaAnimator.SetBool("isHighJump", false);
            }

            moja.isGrounded = Physics2D.Raycast(origin: transform.position, direction: Vector2.down, distance: rayCastLength, groundLayerMask);
        }
    }

    void OnCollisionEnter2D(Collision2D obj)
    {

        if (obj.gameObject.CompareTag("Nail"))
        {

            StartCoroutine(cantMove());
            GameManager.Instance.playerMov.isTakeingDamage = true;
            Vector2 prev = rb.velocity;
            rb.velocity = new Vector2(0, 0);
            rb.velocity = new Vector2(prev.x > 0 ? 5 : -5, prev.y);
        }
        if (obj.gameObject.CompareTag("Wool"))
        {
            moja.suidhaga++;
            Destroy(obj.gameObject);
        }
        if (obj.gameObject.CompareTag("MouseTrap"))
        {
            rb.velocity = new Vector2(0, 0);
            moja.isHurting = true;
            StartCoroutine(escapeRoutine());
            // Debug.Log("Rokooooo");
        }
        if (obj.gameObject.CompareTag("WalterBlue"))
        {
            EventManager.OnPlayerDeath();
        }
        if(obj.gameObject.CompareTag("Wmachine")){
            EventManager.OnTakeDamage(40);
        }
        if(obj.gameObject.CompareTag("Detergent")){
            EventManager.OnTakeDamage(30);
        }
        if(obj.gameObject.CompareTag("Cheese")){
            moja.cheese++;
            Destroy(obj.gameObject);
        }
        if(obj.gameObject.CompareTag("Stone")){
            moja.stone++;
            Destroy(obj.gameObject);
        }
        if(obj.gameObject.CompareTag("Plug")){
            moja.inPlug = true;
        }
        if(obj.gameObject.CompareTag("CockRoach")){
            EventManager.OnTakeDamage(35);
        }
    }

    void OnCollisionExit2D(Collision2D obj){
        if(obj.gameObject.CompareTag("Plug")){
            moja.inPlug = false;
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        switch(obj.gameObject.name)
        {
            case "Kachha":
                if (moja.suidhaga > 0)
                {
                    moja.suidhaga--;
                    EventManager.OnInteract(GameManager.Interaction.Kachha2);
                }
                else
                {
                    EventManager.OnInteract(GameManager.Interaction.Kachha1);
                }
                break;
            case "Mr. Tie":
                EventManager.OnInteract(GameManager.Interaction.tie);
                break;
            
            //case ""

        }
    }

    void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector2.down * rayCastLength, Color.green);
    }

    void OnDisable()
    {
        EventManager.onTakeDamage -= TakeDamage;
        EventManager.onHeal -= Heal;
        EventManager.onPlayerDeath -= DeathFun;
    }

    #endregion


    #region Custom methods

    public void killMachine(){
        MashingWashine machine = GameObject.FindGameObjectWithTag("Wmachine").GetComponent<MashingWashine>();
        machine.dies();
        Debug.Log("machine -- ");
    }

    public void TakeDamage(int damage)
    {
        moja.health -= damage;
    }

    public void Heal(int heal)
    {
        moja.health += heal;
    }

    public void Stagger()
    {
        StartCoroutine(cantMove());
    }

    public void DeathFun()
    {
        GameManager.Instance.playerState = GameManager.PlayerState.Dead;
        rb.velocity = new Vector2(0, 0);
        moja.health = 0;
        moja.isHurting = true;
        StartCoroutine(death());
    }

    IEnumerator cantMove()
    {
        moja.isHurting = true;
        yield return new WaitForSeconds(2);
        moja.isHurting = false;
    }
    IEnumerator death()
    {
        //ded ho gaya animation
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator escapeRoutine()
    {
        while (moja.health > 0)
        {
            yield return new WaitForSeconds(2f);
            EventManager.OnTakeDamage(1);
        }
    }



    #endregion

}