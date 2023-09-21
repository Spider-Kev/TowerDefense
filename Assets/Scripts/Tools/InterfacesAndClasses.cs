using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region INTERFACES
public interface IDamagable
{
    void ApplyDamage(float damage);
}
#endregion


#region STRUCTS
[System.Serializable]
public struct StructBulletPooling
{
    public PoolManager pooling;
    public EnumBulletType bulletType;
}

public struct PlayerStats
{
    public int coins;
    public int life;
    public void StartStats(int startingLife, int startingCurrency)
    {
        life = startingLife;
        coins = startingCurrency;
    }
    
}

[System.Serializable]
public struct StructAllyPooling
{
    public PoolManager pooling;
    public AlliesScriptable allyPooling;
}
#endregion


#region ENUMS

public enum EnumBulletType
{
    bullet_Type_1,
    bullet_Type_2,
    bullet_Type_3
    
}

#endregion

