using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bullet : MonoBehaviour
{
    public string name;

    public GameObject prefab;

    public float shotsDelta, speed;

    public int damage;
    
}

public enum BulletType
{
    Fast,
    Normal,
    Slow,
    Player
}
