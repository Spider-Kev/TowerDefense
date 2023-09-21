using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    /// <summary>
    /// Times for the wave to wait
    /// </summary>
    public float timeBetweenWaves = 3f;
    public float timeBeforeWaves = 2f;
    public float timeAfterWaves = 2f;
    public Wave[] waves;
    #endregion

    #region PRIVATE_VARIABLES
    /// <summary>
    /// Variable to check the current defeated or arrived enemies
    /// </summary>
    private int currentEnemyLength;
    /// <summary>
    /// Flag to ask if all enemies have been defeated 
    /// </summary>
    private bool allEnemiesDeath;
    #endregion

    #region PUBLIC_REFERENCES
    /// <summary>
    /// Reference to the enemy factory to spawn enemies
    /// </summary>
    public EnemiesFactory enemyFactory;
    #endregion

    #region UNITY_METHODS
    /// <summary>
    /// Listen when a enemy has been defeated or arrived to check if the wave has been over
    /// </summary>
    private void OnEnable()
    {
        EventManager.StartListening("EnemyDeath", OnEnemyDeath);
        EventManager.StartListening("EnemyArrived", OnEnemyDeath);

    }

    private void OnDisable()
    {
        EventManager.StopListening("EnemyDeath", OnEnemyDeath);
        EventManager.StopListening("EnemyArrived", OnEnemyDeath);
    }

    #endregion
    
    #region PUBLIC_METHODS
    /// <summary>
    /// Starts the wave from the 0 index
    /// </summary>
    public void StartWave()
    {
        currentEnemyLength = waves[0].enemiesSpawnTime.Length;
        allEnemiesDeath = false;
        StartCoroutine(RoutineStartWaves());
    }
    #endregion

    #region LISTENER_METHODS
    /// <summary>
    /// When a enemy dies reduce the amount of enemies active of this wave
    /// </summary>
    /// <param name="arg0"></param>
    private void OnEnemyDeath(object arg0)
    {
        currentEnemyLength--;
        if (currentEnemyLength <= 0)
            allEnemiesDeath = true;
    }
    #endregion
    
    
    #region COROUTINES
    /// <summary>
    /// Routine to spawn enemies waiting times before, during and after waves.
    /// Each enemy waits its given time to be spawned again
    /// </summary>
    /// <returns></returns>
    IEnumerator RoutineStartWaves()
    {
        yield return new WaitForSeconds(timeBeforeWaves);
        for (int i = 0; i < waves.Length; i++)
        {
            EventManager.TriggerEvent("StartWave", i + 1);
            for (int j = 0; j < waves[i].enemiesSpawnTime.Length; j++)
            {
                enemyFactory.AskForEnemy(waves[i].enemiesSpawnTime[j].indexEnemy);
                yield return new WaitForSeconds(waves[i].enemiesSpawnTime[j].timeToSpawn);
            }

            while (!allEnemiesDeath)
            {
                yield return new WaitForEndOfFrame();
            }
            
            currentEnemyLength = waves[i].enemiesSpawnTime.Length;
            allEnemiesDeath = false;
            
            yield return new WaitForSeconds(timeBetweenWaves);
        }
        
        yield return new WaitForSeconds(timeAfterWaves);
        EventManager.TriggerEvent("GameOver", true);
    }
    

    #endregion
}

[System.Serializable]
public struct Wave
{
    public WaveEnemy[] enemiesSpawnTime;
}

[System.Serializable]
public struct WaveEnemy
{
    public int indexEnemy;
    public float timeToSpawn;
}
