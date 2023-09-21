using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LayoutSpawn : BaseLayoutHud
{
    #region PUBLIC_VARIABLES
    /// <summary>
    /// Image icon of this ally
    /// </summary>
    public Image imgIcon;
    #endregion

    #region PUBLIC_REFERENCES
    /// <summary>
    /// Reference to the scriptableAlly to set the info in this layout
    /// </summary>
    public AlliesScriptable targetAlly;
    #endregion

    #region PRIVATE_METHODS
    /// <summary>
    /// Calls the ally associated with this layout to spawn
    /// </summary>
    private void InstanceAlly()
    {
        EventManager.TriggerEvent("SpawnAlly", targetAlly);
    }
    #endregion

    #region OVERRIDE_METHODS
    /// <summary>
    /// Overrides the cost for this layout
    /// </summary>
    /// <returns>Returns the final cost of the item</returns>
    protected override int GetCost()
    {
        return targetAlly.cost;
    }

    /// <summary>
    /// Set the values for the referenced ScriptableAlly
    /// </summary>
    protected override void StartFunc()
    {
        textCost.text = targetAlly.cost.ToString();
        imgIcon.sprite = targetAlly.spawnSprite;
    }

    /// <summary>
    /// Calls the instance ally function
    /// </summary>
    protected override void SetButtonFunc()
    {
        base.SetButtonFunc();
        InstanceAlly();
    }

    #endregion
}
