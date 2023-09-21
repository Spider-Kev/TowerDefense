using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ally", menuName = "Scriptables/Ally")]
public class AlliesScriptable : ScriptableObject
{
    public float shootRange;    // 
    public int cost;            // Cost of this turret to spawn in scene
    public Sprite spawnSprite;  // Image to show at Spawn HUD

    public float[] damageLevels;    // Levels of damage this object will have
}
