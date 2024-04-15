using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Managers.Lawyer;
using UnityEngine;

public class TestPlayer : MonoBehaviour, IDamageable<string>
{
    private int health = 100;
    
    public void TakeDamage(string type)
    {
        
        Debug.Log("take damage");
    }
}
