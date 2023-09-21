using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    #region PUBLIC_METHODS
    /// <summary>
    /// Function to retry the scene loading it again
    /// </summary>
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Close app
    /// </summary>
    public void QuitApp()
    {
        Application.Quit();
    }
    #endregion
    
}
