using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Ally : MonoBehaviour
{
    #region PUBLIC_PROPERTIES
    /// <summary>
    /// Get the current level of damage of this enemy
    /// </summary>
    public float damage => allyScript.damageLevels[currentLevel];

    public int currentLevel { get; private set; }
    public int maxLevels => allyScript.damageLevels.Length;
    public int cost => allyScript.cost;

    public bool isAtMaxLevel => currentLevel >= maxLevels - 1;
    #endregion
    
    #region PUBLIC_VARIABLES
    /// <summary>
    /// Queue of enemies to attack
    /// </summary>
    public Queue<GameObject> queueEnemies;
    /// <summary>
    /// Cannon to view enemies
    /// </summary>
    public Transform turretCannon;

    /// <summary>
    /// Pivot to spawn bullets
    /// </summary>
    public Transform pivotShoot; 
    #endregion

    #region PRIVATE_VARIABLES
    /// <summary>
    /// This turret is able to shoot
    /// </summary>
    private bool _ableToShoot = true;
    /// <summary>
    /// Time between shoots 
    /// </summary>
    private float _lapsedTime = 0;
    #endregion
    
    #region PUBLIC_REFERENCES
    /// <summary>
    /// Reference to bullet Type
    /// </summary>
    public EnumBulletType bulletType;
    #endregion

    #region PRIVATE_REFERENCES
    /// <summary>
    /// Reference to scriptable ally
    /// </summary>
    [SerializeField] private AlliesScriptable allyScript;

    /// <summary>
    /// Cell this object is attached to
    /// </summary>
    [SerializeField] private Cell _cell;
    #endregion

    #region UNITY_METHODS
    /// <summary>
    /// Starts the queue of the enemy
    /// </summary>
    private void Start()
    {
        queueEnemies = new Queue<GameObject>();
    }

    /// <summary>
    /// Always see to the enemy if there's any available and shoot in range
    /// </summary>
    private void Update()
    {
        if (queueEnemies.Count <= 0) return;
        turretCannon.LookAt(queueEnemies.Peek().transform);
        if (!queueEnemies.Peek().activeInHierarchy)
        {
            ExitEnemy();
            return;
        }
                
            
        if (_ableToShoot)
        {
            EventManager.TriggerEvent("AskForBullet", this);
            //bulletPool.AskForObject(pivotShoot.position, pivotShoot.rotation);
            _ableToShoot = false;
        }
        else
        {
            _lapsedTime += Time.deltaTime;
            if (_lapsedTime > allyScript.shootRange)
            {
                _ableToShoot = true;
                _lapsedTime = 0;
            }
        }
    }
    #endregion

    #region PUBLIC_METHODS
    /// <summary>
    /// Set new enemy in sight 
    /// </summary>
    public void SetEnemyInRange(GameObject targetEnemy)
    {
        queueEnemies.Enqueue(targetEnemy);
    }

    /// <summary>
    /// Dequeue last enemy
    /// </summary>
    public void ExitEnemy()
    {
        queueEnemies.Dequeue();
    }

    /// <summary>
    /// Set the turret above one of the tiles 
    /// </summary>
    public void SetAllyInScene(Cell targetCell)
    {
        gameObject.SetActive(true);
        _cell = targetCell;
        currentLevel = 0;
    }

    /// <summary>
    /// Updates the level of this ally
    /// </summary>
    public void UpdateAlly()
    {
        currentLevel++;
        if (currentLevel >= maxLevels)
            currentLevel = maxLevels - 1;
    }

    /// <summary>
    /// Disable ally due tu sell
    /// </summary>
    public void DisableAlly()
    {
        gameObject.SetActive(false);
        _cell.RemoveAlly();
    }
    #endregion
}
