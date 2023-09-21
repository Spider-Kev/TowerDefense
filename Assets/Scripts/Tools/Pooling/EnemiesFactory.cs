using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesFactory : MonoBehaviour
{
    #region PUBLIC_REFERENCES
/// <summary>
/// Reference to all poolingObjects of enemies
/// </summary>
    public PoolManager[] poolsEnemies;
    #endregion

    #region PUBLIC_METHODS
    /// <summary>
    /// This prefab has always the same index for enemies, so it would be easier just to ask for enemy index
    /// </summary>
    /// <param name="indexEnemy"></param>
    public void AskForEnemy(int indexEnemy)
    {
        poolsEnemies[indexEnemy].AskForObject().GetComponent<Enemy>().EnableObject();
    }
    

    #endregion
}
