using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is the base layout to buy allies and its upgrades
/// </summary>
public class BaseLayoutHud : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    /// <summary>
    /// Text for cost when updated the currency
    /// </summary>
    public Text textCost;
    /// <summary>
    ///  Image to block the interaction to avoid user to press
    /// </summary>
    public Image imgBlock;
    /// <summary>
    /// Button to disable the interaction
    /// </summary>
    public Button btnLayout;
    #endregion

    #region PUBLIC_REFERENCES
    /// <summary>
    /// Reference to the menu pop-up when selected a cell
    /// </summary>
    public SpawnAllyMenu allyMenu;
    #endregion
    
    #region UNITY_METHODS

    private void Awake()
    {
        if (allyMenu == null)
            allyMenu = FindObjectOfType<SpawnAllyMenu>();
    }

    /// <summary>
    /// Add the virtual function of SetButtonFunc to the button
    /// </summary>
    private void Start()
    {
        btnLayout.onClick.AddListener(SetButtonFunc);
        StartFunc();
    }
    
    /// <summary>
    /// Methods to implement observer to currency changed
    /// </summary>
    private void OnEnable()
    {
        EventManager.StartListening("CurrencyChanged", OnCurrencyChanged);
        StartListeningEvents();
    }

    private void OnDisable()
    {
        EventManager.StopListening("CurrencyChanged", OnCurrencyChanged);
        StopListeningEvents();
    }
    #endregion

    #region LISTENER_METHODS
    /// <summary>
    /// Check the new balance of the coins to see if this ally can be bought
    /// </summary>
    /// <param name="arg0">Delta of the new balance</param>
    protected virtual void OnCurrencyChanged(object arg0)
    {
        bool canBuy = GameManager.playerStats.coins >= GetCost();
        imgBlock.enabled = !canBuy;
        btnLayout.interactable = canBuy;
    }
    #endregion

    #region VIRTUAL_METHODS
    /// <summary>
    /// Each layout will inherit from this class will implement it's own cost
    /// </summary>
    /// <returns>Cost of the layout</returns>
    protected virtual int GetCost()
    {
        return 0;
    }

    protected virtual void StartFunc()
    {
        
    }

    protected virtual void SetButtonFunc()
    {
        allyMenu.DisableHuds();
    }
    protected virtual void StartListeningEvents()
    {
        
    }

    protected virtual void StopListeningEvents()
    {
        
    }
    #endregion
}
