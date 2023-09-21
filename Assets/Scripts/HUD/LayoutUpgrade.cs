using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayoutUpgrade : BaseLayoutHud
{
    #region PRIVATE_REFERENCES
    /// <summary>
    /// Reference to the cell selected
    /// </summary>
    [SerializeField] private Ally asociatedAlly; 
    #endregion

    #region PRIVATE_METHODS
    /// <summary>
    /// Function to call the upgrade method in the ally and decrease currency
    /// </summary>
    private void UpgradeAlly()
    {
        asociatedAlly.UpdateAlly();
        GameManager.instance.Buy(asociatedAlly.cost);
        CellPointing.instance.RestartSelectedCell();
    }
    #endregion

    #region OVERRIDE_METHODS
    /// <summary>
    /// Also add the function to upgrade the ally in addition of disable the hud
    /// </summary>
    protected override void SetButtonFunc()
    {
        base.SetButtonFunc();
        UpgradeAlly();
    }

    /// <summary>
    /// Starts and stops listening the selectedCell event
    /// </summary>
    protected override void StartListeningEvents()
    {
        EventManager.StartListening("SelectedCell",OnSelectedCell);
    }

    protected override void StopListeningEvents()
    {
        EventManager.StopListening("SelectedCell",OnSelectedCell);
    }

    /// <summary>
    /// Update the text when the currency is changed
    /// </summary>
    /// <param name="arg0"></param>
    protected override void OnCurrencyChanged(object arg0)
    {
        if (asociatedAlly==null)
            return;
        
        base.OnCurrencyChanged(arg0);
        
        btnLayout.interactable = !asociatedAlly.isAtMaxLevel && GameManager.playerStats.coins>=GetCost();
        imgBlock.enabled = !btnLayout.interactable;
    }

    /// <summary>
    /// Override the cost of this ally to be showed according to different allies
    /// </summary>
    /// <returns></returns>
    protected override int GetCost()
    {
        if (asociatedAlly == null)
            return 0;
        return asociatedAlly.cost;
    }

    #endregion

    #region LISTENER_METHODS
    /// <summary>
    /// Assign the ally of the selected cell to be displayed
    /// </summary>
    /// <param name="arg0"></param>
    private void OnSelectedCell(object arg0)
    {
        asociatedAlly = ((Cell)arg0).asociatedAlly;
        if (asociatedAlly != null)
        {
            textCost.text = asociatedAlly.cost.ToString();
            btnLayout.interactable = !asociatedAlly.isAtMaxLevel && GameManager.playerStats.coins>=GetCost(); 
            imgBlock.enabled = !btnLayout.interactable;
        }
    }
    #endregion
}
