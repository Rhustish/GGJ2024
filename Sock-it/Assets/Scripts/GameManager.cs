using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variables

    public static GameManager Instance;

    public GameObject Player;

    public PlayerMovement playerMov;

    public Slider healthBar;

    public Baatcheet baatWriter;

    public List<BaatScriptableObject> scriptObjects;

    public BaatCheetHelper helper;

    public enum GameState
    {
        Playing,
        Paused,
        Interacting,
        GameOver
    }

    public enum PlayerState
    {
        Alive,
        Dead
    }

    public enum Interaction
    {
        None,
        Kachha1,
        Kachha2,
        tie,
        scissors1,
        scissors2,
        magicalMoja,
        rat1,
        rat2
    }

    public Interaction interaction;
    public GameState gameState;
    public PlayerState playerState;

    #endregion

    #region Unity callbacks

    private void OnEnable()
    {
        EventManager.onTakeDamage += ReduceHealth;
        EventManager.onHeal += IncreaseHealth;
        EventManager.onInteract += ShowInteractionText;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        healthBar.value = 100;
        helper = GetComponent<BaatCheetHelper>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            baatWriter.WriteNextBaatInQueue();
        }
    }

    private void OnDisable()
    {
        EventManager.onTakeDamage -= ReduceHealth;
        EventManager.onHeal -= IncreaseHealth;
    }

    #endregion


    #region Custom methods

    public void ShowInteractionText(Interaction inter)
    {
        gameState = GameState.Interacting;
        interaction = inter;
        switch (interaction)
        {
            case Interaction.tie:
                helper.StartInteraction(scriptObjects[0]);
                break;
            case Interaction.Kachha1:
                helper.StartInteraction(scriptObjects[1]);
                break;
            case Interaction.scissors1:
                helper.StartInteraction(scriptObjects[2]);
                break;
            case Interaction.scissors2:
                helper.StartInteraction(scriptObjects[3]);
                break;
            case Interaction.magicalMoja:
                helper.StartInteraction(scriptObjects[4]);
                break;
            case Interaction.Kachha2:
                helper.StartInteraction(scriptObjects[5]);
                break;
            case Interaction.rat1:
                helper.StartInteraction(scriptObjects[6]);
                break;
            case Interaction.rat2:
                helper.StartInteraction(scriptObjects[7]);
                break;

        }

    }

    public void CheckAndChangeHealthColor()
    {
        if(healthBar.value >=70)
        {
            healthBar.fillRect.GetComponent<Image>().color = Color.green;
        }else if(healthBar.value < 70 && healthBar.value > 30)
        {
            healthBar.fillRect.GetComponent<Image>().color = Color.yellow;
        }else
        {
            healthBar.fillRect.GetComponent<Image>().color = Color.red;
        }
    }

    public void ReduceHealth(int damage)
    {
        healthBar.value -= damage;
        CheckAndChangeHealthColor();
    }

    public void IncreaseHealth(int heal)
    {
        healthBar.value += heal;
        CheckAndChangeHealthColor();
    }

    public void OnPlayerDeath()
    {
        // do something
    }

    #endregion

}
