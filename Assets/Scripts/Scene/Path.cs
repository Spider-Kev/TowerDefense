using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Path : MonoBehaviour
{
    /// <summary>
    /// Singleton of the path of this level
    /// </summary>
    public static Path instance { get; private set; }

    #region PUBLIC_VARIABLES
    /// <summary>
    /// Waypoints of the path
    /// </summary>
    public Vector3[] points;
    #endregion

    #region UNITY_METHODS
    private void Awake()
    {
        if (instance !=null && instance!=this)
            Destroy(this);
        else
            instance = this;
    }
    #if UNITY_EDITOR
    /// <summary>
    ///  Just in editor we will render the path with draw lines
    /// </summary>
    private void Update()
    {
        for (int i = 0; i < points.Length - 1; i++)
        {
            Debug.DrawLine(points[i], points[i+1]);
        }
        
    }
    #endif
    #endregion
    
    #region MyRegion
    /// <summary>
    /// Better try to assign the points before runtime, that's why we assign a ContextMenu
    /// This function could be helpful for editor 
    /// </summary>
    [ContextMenu("Assign points from the child transform")]
    public void AssignPoints()
    {
        points = new Vector3[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i).position;
        }
    }
    #endregion

}
