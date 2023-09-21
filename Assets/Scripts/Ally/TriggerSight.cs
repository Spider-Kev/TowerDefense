using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerSight : MonoBehaviour
{
    #region PUBLIC_REFERENCES
    /// <summary>
    ///  Reference to ally
    /// </summary>
    public Ally ally;
    #endregion

    #region UNITY_METHODS
    /// <summary>
    /// Check if triggers the enemy to add into the referenced ally queue
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        ally.SetEnemyInRange(other.gameObject);
    }

    /// <summary>
    /// Check if enemy exits the trigger to dequeue the enemy
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        ally.ExitEnemy();
    }

    #endregion
}
