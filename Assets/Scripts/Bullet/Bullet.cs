using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Bullet : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    /// <summary>
    /// Speed this bullet will travel
    /// </summary>
    public float speed;

    /// <summary>
    /// Targeted Enemy
    /// </summary>
    public GameObject enemyToFollow;
    #endregion

    #region PUBLIC_REFERENCES
    /// <summary>
    /// Ally this script is attached to
    /// </summary>
    public Ally targetAlly;
    #endregion

    #region UNITY_METHODS
    /// <summary>
    /// Moves the bullet towards its front vector, if the target is active check if the distance is close enough to apply damage
    /// </summary>
    private void Update()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * speed));
        if (enemyToFollow.activeInHierarchy)
        {
            if (Vector3.Distance(transform.position, enemyToFollow.transform.position) < 1f)
            {
                // TODO: Considered collided
                enemyToFollow.GetComponent<IDamagable>().ApplyDamage(targetAlly.damage);
                gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    ///  Also disable the object if is no longer visible in scene
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);    
    }
    #endregion

    #region PUBLIC_METHODS

    /// <summary>
    /// Set the ally and target enemy for this object 
    /// </summary>
    /// <param name="ally">The ally who called for this bullet</param>
    public void SetBullet(Ally ally)
    {
        gameObject.SetActive(true);
        targetAlly = ally;
        enemyToFollow = ally.queueEnemies.Peek();
    }
    #endregion
}
