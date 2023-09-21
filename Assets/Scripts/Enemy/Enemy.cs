using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Enemy : MonoBehaviour, IDamagable
{
    #region PUBLIC_VARIABLES
    /// <summary>
    /// Current life of the enemy
    /// </summary>
    public float currentLife { get; private set; }
    public float totalLife { get { return enemyScript.health; } }
    #endregion
    
    #region PRIVATE_REFERENCES
    /// <summary>
    /// Reference to enemy scriptable
    /// </summary>
    [SerializeField] private EnemyScriptable enemyScript;
    #endregion

    #region PRIVATE_VARIABLES
    /// <summary>
    /// Current index above points to move
    /// </summary>
    private int _currentIndex;

    public int cost { get { return enemyScript.coins; } }
    
    /// <summary>
    /// Delegate to call when this enemy receives damage
    /// </summary>
    public delegate void ReceivedDamage();
    public ReceivedDamage OnReceiveDamage; 
    #endregion
    
    #region UNITY_METHODS
    /// <summary>
    /// Calls the Enable object at the start 
    /// </summary>
    private void Start()
    {
        EnableObject();
    }
    
    /// <summary>
    /// Move along the array of points of the Path
    /// </summary>
    private void Update()
    {
        transform.position = Vector3.MoveTowards( transform.position,Path.instance.points[_currentIndex], enemyScript.speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, Path.instance.points[_currentIndex]) < 0.1f)
        {
            _currentIndex++;
            if (_currentIndex >= Path.instance.points.Length)
            {
                // TODO: Enemy finished path
                EventManager.TriggerEvent("EnemyArrived",this);
                gameObject.SetActive(false);
            }
        }
    }
    #endregion

    #region PUBLIC_METHODS
    /// <summary>
    /// Enables the object from the object pooling and sets at the start of the path
    /// </summary>
    public void EnableObject()
    {
        _currentIndex = 0;
        currentLife = enemyScript.health;
        transform.position = Path.instance.points[_currentIndex];
        gameObject.SetActive(true);
    }
    #endregion

    #region INTERFACE_METHODS
    /// <summary>
    /// Process Damage implementing interface   
    /// </summary>
    /// <param name="damage"></param>
    public void ApplyDamage(float damage)
    {
        currentLife -= damage;
        OnReceiveDamage.Invoke();
        if (currentLife<=0)
            Die();
    }

    /// <summary>
    /// Process Die event
    /// </summary>
    public void Die()
    {
        EventManager.TriggerEvent("EnemyDeath", this);
        gameObject.SetActive(false);
    }
    #endregion
}
