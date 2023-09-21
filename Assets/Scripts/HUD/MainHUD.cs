using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainHUD : MonoBehaviour
{
    public Text textLife;

    public Text textCoins;

    #region UNITY_METHODS
    /// <summary>
    /// Methods to implement observer to currency changed
    /// </summary>
    private void OnEnable()
    {
        EventManager.StartListening("CurrencyChanged", OnCurrencyChanged);
        EventManager.StartListening("LifeChanged", OnLifeChanged);

    }

    private void OnDisable()
    {
        EventManager.StopListening("CurrencyChanged", OnCurrencyChanged);
        EventManager.StopListening("LifeChanged", OnLifeChanged);

    }
    #endregion

    #region LISTENERS_METHODS
    /// <summary>
    /// Update the UI currency 
    /// </summary>
    /// <param name="arg0"></param>
    private void OnCurrencyChanged(object arg0)
    {
        textCoins.text = GameManager.playerStats.coins.ToString();
    }
    
    /// <summary>
    /// Update the UI life
    /// </summary>
    /// <param name="arg0"></param>
    private void OnLifeChanged(object arg0)
    {
        textLife.text = GameManager.playerStats.life.ToString();
    }
    #endregion
}
