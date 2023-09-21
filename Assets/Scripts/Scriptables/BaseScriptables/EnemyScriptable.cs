using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Scriptables/Enemy")]
public class EnemyScriptable : ScriptableObject
{
    public float speed;     // Movement speed for this enemy
    public float health;    // Total HP before this enemy death
    public int coins;       // Coins the enemy will give after death
}
