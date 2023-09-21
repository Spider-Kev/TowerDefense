using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    /// <summary>
    /// Canvases for win and lose screen
    /// </summary>
    public Canvas canvasWin;
    public Canvas canvasLose;
    public Canvas canvasGameOver;
    #endregion

    #region UNITY_METHODS

    private void Start()
    {
        canvasWin.enabled = false;
        canvasLose.enabled = false;
        canvasGameOver.enabled = false;
    }

    /// <summary>
    /// Starts listening the GameOver event
    /// </summary>
    private void OnEnable()
    {
        EventManager.StartListening("GameOver", OnGameOver);
    }

    private void OnDisable()
    {
        EventManager.StopListening("GameOver", OnGameOver);
    }

    #endregion

    #region LISTENER_METHODS
    /// <summary>
    /// According if the player won display the win or lose canvas
    /// </summary>
    /// <param name="arg0">Did the player win?</param>
    private void OnGameOver(object arg0)
    {
        if (GameManager.isGameOver)
            return;
        
        canvasGameOver.enabled = true;
        if ((bool)arg0)
            canvasWin.enabled = true;
        else
            canvasLose.enabled = true;

        GameManager.isGameOver = true;
    }
    #endregion
}
