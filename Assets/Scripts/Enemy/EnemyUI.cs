using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    #region PUBLIC_REFERENCES
    /// <summary>
    /// Target enemy
    /// </summary>
    public Enemy enemy;
    /// <summary>
    /// Reference to the fill amount image of the UI
    /// </summary>
    public Image imageHP;
    #endregion

    #region UNITY_METHODS
    /// <summary>
    /// Starts listening for delegate of enemy damage
    /// </summary>
    private void OnEnable()
    {
        imageHP.fillAmount = 1;
        enemy.OnReceiveDamage += OnEnemyReceivedDamage;
    }

    /// <summary>
    /// Stops listening for delegate of enemy damage
    /// </summary>
    private void OnDisable()
    {
        enemy.OnReceiveDamage -= OnEnemyReceivedDamage;
    }
    #endregion

    #region LISTENER_METHODS
    /// <summary>
    /// Listener when the enemy received damage
    /// </summary>
    private void OnEnemyReceivedDamage()
    {
        imageHP.fillAmount = enemy.currentLife / enemy.totalLife;
    }
    #endregion
}
