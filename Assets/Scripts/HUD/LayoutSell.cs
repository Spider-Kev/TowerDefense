using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class LayoutSell : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    /// <summary>
    ///  Text to display price
    /// </summary>
    public Text textSellCost;
    /// <summary>
    /// Button to click and sell ally
    /// </summary>
    public Button btnSellAlly;
    #endregion
    
    #region PUBLIC_REFERENCES
    /// <summary>
    /// Reference to the menu pop-up when selected a cell
    /// </summary>
    public SpawnAllyMenu allyMenu;
    #endregion
    
    #region PRIVATE_REFERENCES
    /// <summary>
    /// Reference to the cell selected
    /// </summary>
    [SerializeField] private Ally asociatedAlly; 
    #endregion

    #region UNITY_METHODS
    private void Awake()
    {
        if (allyMenu == null)
            allyMenu = FindObjectOfType<SpawnAllyMenu>();
    }
    
    /// <summary>
    /// Sets the method listener for the button
    /// </summary>
    private void Start()
    {
        btnSellAlly.onClick.AddListener(SellAlly);
    }
    
    /// <summary>
    /// Starts listening for SelectedCell event to assign the reference of the Ally
    /// </summary>
    void OnEnable()
    {
        EventManager.StartListening("SelectedCell",OnSelectedCell);
    }

    void OnDisable()
    {
        EventManager.StopListening("SelectedCell",OnSelectedCell);
    }
    #endregion

    #region PRIVATE_METHODS
    /// <summary>
    /// Function to sell ally by disabling it, null the reference in cell and change currency
    /// </summary>
    private void SellAlly()
    {
        allyMenu.DisableHuds();
        asociatedAlly.DisableAlly();
        GameManager.instance.AddCurrency(asociatedAlly.cost /2);
        CellPointing.instance.RestartSelectedCell();
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
        if (asociatedAlly!=null)
            textSellCost.text = (asociatedAlly.cost/2).ToString();
    }
    #endregion
}
