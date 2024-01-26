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

    #endregion

    #region Unity callbacks

    private void OnEnable()
    {
        EventManager.onTakeDamage += ReduceHealth;
        EventManager.onHeal += IncreaseHealth;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        EventManager.onTakeDamage -= ReduceHealth;
        EventManager.onHeal -= IncreaseHealth;
    }

    #endregion


    #region Custom methods

    public void ReduceHealth(int damage)
    {
        healthBar.value -= damage;
    }

    public void IncreaseHealth(int heal)
    {
        healthBar.value += heal;
    }

    #endregion

}
