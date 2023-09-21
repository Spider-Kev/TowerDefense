using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPointing : MonoBehaviour
{
    public static CellPointing instance;
    
    #region PUBLIC_VARIABLES
    /// <summary>
    /// Camera in scene
    /// </summary>
    public Camera mainCam;
    /// <summary>
    /// Layer mask for the raycast to work
    /// </summary>
    public LayerMask maskToHit;
    #endregion
    
    #region PUBLIC_REFERENCES
    /// <summary>
    /// HUD that will create allies and upgrades
    /// </summary>
    public SpawnAllyMenu hudAlly;
    /// <summary>
    /// Sets the selected transform into this tile
    /// </summary>
    public Cell selectedCell { get; private set; }
    #endregion

    #region UNITY_METHODS

    private void Awake()
    {
        instance = this;
        
        if (hudAlly == null)
            hudAlly = FindObjectOfType<SpawnAllyMenu>();
    }

    /// <summary>
    /// Asks if user click pressed over a Tile with a Raycast
    /// </summary>
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (!Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out var hitInfo, 100f,
                maskToHit)) return;
        if (selectedCell == null)
        {
            selectedCell = hitInfo.transform.GetComponent<Cell>();
            if (selectedCell.asociatedAlly==null)
                hudAlly.ShowSpawnHUD(selectedCell.transform.position);
            else
                hudAlly.ShowUpgradeHUD(selectedCell.transform.position);
                    
            EventManager.TriggerEvent("SelectedCell", selectedCell);
        }
        else
        {
            if (selectedCell.transform == hitInfo.transform) return;
            RestartSelectedCell();
            hudAlly.DisableHuds();
        }
    }

    #endregion

    #region PUBLIC_METHODS
    /// <summary>
    /// Restarts the selected cell to make it selectable again
    /// </summary>
    public void RestartSelectedCell()
    {
        selectedCell = null;
    }
    #endregion
}
