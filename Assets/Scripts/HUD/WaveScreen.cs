using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveScreen : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    /// <summary>
    /// Indicates the Text in UI of the current Wave
    /// </summary>
    public Text textCurrentWave;
    #endregion
    
    #region UNITY_METHODS
    /// <summary>
    /// Listens when a new wave starts
    /// </summary>
    private void OnEnable()
    {
        EventManager.StartListening("StartWave", OnStartedWave);
    }
    
    private void OnDisable()
    {
        EventManager.StopListening("StartWave", OnStartedWave);

    }
    #endregion

    #region LISTENER_METHODS
    /// <summary>
    /// Once a wave starts, show the message and starts the routine
    /// </summary>
    /// <param name="arg0"></param>
    private void OnStartedWave(object arg0)
    {
        textCurrentWave.text = "Wave " + arg0;
        StartCoroutine(RoutineAnimMessage());
    }
    #endregion

    #region COROUTINES
    /// <summary>
    /// The routine will disable the text after 3 seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator RoutineAnimMessage()
    {
        textCurrentWave.enabled = true;
        yield return new WaitForSeconds(3);
        textCurrentWave.enabled = false;
    }
    #endregion
}
