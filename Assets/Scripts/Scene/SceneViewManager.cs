using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneViewManager : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    /// <summary>
    /// Points that will form the path
    /// </summary>
    public List<Vector3> pointsInRoute;
    /// <summary>
    /// Matrix to instance grids in scene can be changed to a file content
    ///  0 - Normal spawn grid
    ///  1 - Ground grid, the player can't build allies here
    ///  2 - Point of path
    /// </summary>
    public int[][] gridMatrix = new int[][]
    {
        new []{0,0,0,0,0,0,0,2,0,0},
        new []{0,2,1,1,1,1,1,2,0,0},
        new []{0,1,0,0,0,0,0,0,0,0},
        new []{0,1,0,0,0,0,0,0,0,0},
        new []{0,2,1,1,1,1,1,1,2,0},
        new []{0,0,0,0,0,0,0,0,2,0}
    };
    #endregion

    #region  PUBLIC_REFERENCES
    /// <summary>
    /// Pools for instance ground and grass
    /// </summary>
    public PoolManager poolManagerGround;
    public PoolManager poolManagerGrass;
    #endregion

    #region UNITY_METHODS
    /// <summary>
    /// Set the points of the path founded in the array of routes
    /// </summary>
    private void Start()
    {
        Path.instance.points = pointsInRoute.ToArray();
    }

    #endregion

    #region PUBLIC_METHODS
    /// <summary>
    /// Will instance all grids in the gridMatrix, called just in editor
    /// </summary>
    [ContextMenu("Instance all grids")]                 
    public void InstanceGrid()
    {
        pointsInRoute = new List<Vector3>();
        Vector3 instancedPosition = Vector3.zero;
        
        for (int i = 0; i < gridMatrix.Length; i++)
        {
            for (int j = 0; j < gridMatrix[i].Length; j++)
            {
                instancedPosition.x = i * 2;
                instancedPosition.z = j * 2;
                switch (gridMatrix[i][j])
                {
                    case 1:
                        poolManagerGround.AskForObject(instancedPosition);
                        break;
                    
                    case 2:
                        poolManagerGround.AskForObject(instancedPosition);
                        pointsInRoute.Add(instancedPosition);
                        break;
                    
                    default:
                        poolManagerGrass.AskForObject(instancedPosition);
                        break;
                }
            }
        }

        
    }
    

    #endregion
    
}
