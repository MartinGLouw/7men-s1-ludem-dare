using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Managers.Lawyer;
using UnityEngine;

public enum DamageType
{
    Player,
    EnemyShotgun,
    EnemySprayer,
    EnemySniper,
    EnemyBrawler,
    EnemyCrowbar
}

public class TestPlayer : MonoBehaviour, IDamageable<DamageData>
{
    private int health = 100;
    
    public void TakeDamage(DamageData data)
    {
        
        Debug.Log("take damage");
    }
}

[Serializable]
public struct DamageData
{
    public DamageType type;
    public int dmgAmount;
}
