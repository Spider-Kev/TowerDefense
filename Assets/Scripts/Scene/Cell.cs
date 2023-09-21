using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    /// <summary>
    /// Point where the turret will be spawned
    /// </summary>
    public Transform spawnPoint;
    #endregion

    #region PUBLIC_REFERENCES
    /// <summary>
    /// Assign the cell with an ally
    /// </summary>
    public Ally asociatedAlly { get; private set; }
    #endregion

    #region PUBLIC_METHODS
    /// <summary>
    /// Set the Ally reference
    /// </summary>
    /// <param name="newAlly">The new ally</param>
    public void SetAlly(Ally newAlly)
    {
        asociatedAlly = newAlly;
    }

    /// <summary>
    /// Set null the ally to set a new one after
    /// </summary>
    public void RemoveAlly()
    {
        asociatedAlly = null;
    }
    #endregion
}
