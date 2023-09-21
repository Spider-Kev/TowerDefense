using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAllyMenu : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    /// <summary>
    /// Canvas of the interactions to spawn
    /// </summary>
    public Canvas canvasSpawn;
    /// <summary>
    /// Canvas of the upgrade interaction
    /// </summary>
    public Canvas canvasUpgrade;
    #endregion
    
    #region UNITY_METHODS
    /// <summary>
    /// Disable all huds
    /// </summary>
    private void Start()
    {
        DisableHuds();
    }
    #endregion

    #region PUBLIC_METHODS
    /// <summary>
    /// Enables the canvas of the spawn ally
    /// </summary>
    /// <param name="newPos">position of this object to ve moved</param>
    public void ShowSpawnHUD(Vector3 newPos)
    {
        canvasSpawn.enabled = true;
        canvasUpgrade.enabled = false;
        transform.position = newPos;
    }

    /// <summary>
    /// Enable the canvas that contains the upgrade functions for the ally in the cell
    /// </summary>
    /// <param name="targetCell">Reference of the cell</param>
    public void ShowUpgradeHUD(Vector3 newPos)
    {
        canvasSpawn.enabled = false;
        canvasUpgrade.enabled = true;
        transform.position = newPos;
    }
    
    /// <summary>
    /// Disable both, the upgrade and spawn huds
    /// </summary>
    public void DisableHuds()
    {
        canvasSpawn.enabled = false;
        canvasUpgrade.enabled = false;
    }
    #endregion
    
}
