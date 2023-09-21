using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsFactory : MonoBehaviour
{
    #region PUBLIC_REFERENCES
    /// <summary>
    /// Reference to structs for pooling
    /// </summary>
    public StructBulletPooling[] bulletPoolings;
    #endregion

    #region PRIVATE_REFERENCES

    /// <summary>
    /// Reference to Ally asking for bullet
    /// </summary>
    private Ally _ally;
    #endregion

    #region UNITY_METHODS
/// <summary>
/// Listen when a bullet is required from an ally
/// </summary>
    private void OnEnable()
    {
        EventManager.StartListening("AskForBullet", OnAskedForBullets);
    }
    
    private void OnDisable()
    {
        EventManager.StopListening("AskForBullet", OnAskedForBullets);
    }

    #endregion

    #region LISTENER_METHODS
    /// <summary>
    /// Search for the type of bullet and instantiates that kind
    /// </summary>
    /// <param name="arg0">Ally reference invoked with the EventManager</param>
    private void OnAskedForBullets(object arg0)
    {
        _ally = (Ally)arg0;
        var objectFound = Array.Find(bulletPoolings, pooled => pooled.bulletType == _ally.bulletType);
        var askedObject = objectFound.pooling.AskForObject(_ally.pivotShoot.position, _ally.pivotShoot.rotation);
        askedObject.GetComponent<Bullet>().SetBullet(_ally);

    }
    #endregion
}
