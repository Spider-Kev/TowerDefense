using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Singleton of the Game Manager
    /// </summary>
    public static GameManager instance;

    #region PRIVATE_VARIABLES
    /// <summary>
    /// State of game over
    /// </summary>
    public static bool isGameOver;
    #endregion
    
    #region PUBLIC_REFERENCES
    /// <summary>
    /// References to player stats and wave manager
    /// </summary>
    public static PlayerStats playerStats;
    public WaveManager waveManager;
    #endregion

    #region UNITY_METHODS
    /// <summary>
    /// Starts the singleton
    /// </summary>
    private void Awake()
    {
        instance = this;
    }
    
    /// <summary>
    /// Listens to every change of life and currency
    /// </summary>
    private void OnEnable()
    {
        EventManager.StartListening("EnemyArrived", OnEnemyArrived); 
        EventManager.StartListening("EnemyDeath", OnEnemyDeath); 
        EventManager.StartListening("SpawnAlly", OnSpawnedAlly);

    }

    private void OnDisable()
    {
        EventManager.StopListening("EnemyArrived", OnEnemyArrived);  
        EventManager.StopListening("EnemyDeath", OnEnemyDeath);
        EventManager.StopListening("SpawnAlly", OnSpawnedAlly);

    }

    /// <summary>
    /// Start the current player stats
    /// </summary>
    private void Start()
    {
        playerStats.StartStats(15,100);
        EventManager.TriggerEvent("CurrencyChanged",100);
        EventManager.TriggerEvent("LifeChanged",100);
        waveManager.StartWave();
        isGameOver = false;
        
    }
    #endregion

    #region PUBLIC_METHODS
    /// <summary>
    /// Buy an ally or an upgrade
    /// </summary>
    /// <param name="amountSpent">amount of coins spent</param>
    public void Buy(int amountSpent)
    {
        playerStats.coins -= amountSpent;
        EventManager.TriggerEvent("CurrencyChanged",amountSpent);
    }
    
    /// <summary>
    /// Adds or sub coins to the player state
    /// </summary>
    /// <param name="amountGet">The amount of amount to be added or removed</param>
    public void AddCurrency(int amountGet)
    {
        playerStats.coins += amountGet;
        EventManager.TriggerEvent("CurrencyChanged",amountGet);

    }
    #endregion

    #region LISTENER_EVENTS
    /// <summary>
    /// Subs the currency of spawning the ally
    /// </summary>
    /// <param name="arg0"></param>
    private void OnSpawnedAlly(object arg0)
    {
        AddCurrency(-((AlliesScriptable)arg0).cost);
    }
    
    /// <summary>
    /// When an enemy arrives, decrease one life point, and if its 0 or less, the game overs
    /// </summary>
    /// <param name="arg0"></param>
    private void OnEnemyArrived(object arg0)
    {
        playerStats.life--;
        EventManager.TriggerEvent("LifeChanged",playerStats.life);
        if (playerStats.life <= 0)
        {
            EventManager.TriggerEvent("GameOver", false);
        }
    }
    
    /// <summary>
    /// Called when an enemy is death to increase the currency
    /// </summary>
    /// <param name="arg0"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnEnemyDeath(object arg0)
    {
        AddCurrency(((Enemy)arg0).cost);
    }
    #endregion
}
