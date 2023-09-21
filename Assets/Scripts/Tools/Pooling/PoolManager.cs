using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    /// <summary>
    /// Should instance the starting objects at the beginning (better uncheck but instance from the context menu instead)
    /// </summary>
    public bool instanceObjecstAtStart;
    /// <summary>
    /// Prefab to instantiate
    /// </summary>
    public GameObject prefabToCreate;
    /// <summary>
    /// List of objects created in scene
    /// </summary>
    public List<GameObject> createdObjects;
    #endregion
    
    #region PRIVATE_VARIABLES
    /// <summary>
    /// Number of objects to be spawned at the beggining
    /// </summary>
    [SerializeField] private int _startingObjects = 0;
    #endregion
    
    #region UNITY_METHODS
    /// <summary>
    /// If instanceObjectsAtStart is true, instance at runtime the starting needed objects 
    /// </summary>
    private void Start()
    {
        if (instanceObjecstAtStart)
        {
            createdObjects = new List<GameObject>();
            CreateStartingObjects();
        }
        
    }
    #endregion

    #region PUBLIC_METHODS
    /// <summary>
    /// Asks for an object in Object Pooling, if there's no enough objects, instance new one
    /// </summary>
    /// <param name="position">The position for this object, by default 0,0,0</param>
    /// <returns></returns>
    public virtual GameObject AskForObject(Vector3 position = new Vector3(), Quaternion rotation = new Quaternion())
    {
        for (int i = 0; i < createdObjects.Count; i++)
        {
            if (!createdObjects[i].activeInHierarchy)
            {
                createdObjects[i].transform.SetPositionAndRotation(position,rotation);
                return createdObjects[i];
            }
        }
        
        GameObject pivot = CreateObject(position, rotation);
        createdObjects.Add(pivot);
        return pivot;
    }
    
    #endregion

    #region PRIVATE_METHODS
    /// <summary>
    /// Creates the starting objects pooling (better use this in editor and uncheck instance at runtime)
    /// </summary>
    [ContextMenu("Create Starting Objects")]
    private void CreateStartingObjects()
    {
        for (int i = 0; i < _startingObjects; i++)
        {
            createdObjects.Add(CreateObject(Vector3.zero, Quaternion.identity));
        }
    }
    
    /// <summary>
    /// Instance the object in scene
    /// </summary>
    /// <param name="instancePos">Position where the object will instance</param>
    /// <returns></returns>
    private GameObject CreateObject(Vector3 instancePos, Quaternion instanceRot)
    {
        return Instantiate(prefabToCreate, instancePos, instanceRot);
    }
    #endregion
    
}
