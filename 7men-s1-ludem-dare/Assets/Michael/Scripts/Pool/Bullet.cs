using System;
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
    public BulletType type;

    private void OnEnable()
    {
        StartCoroutine(ReturnBulletToPool());
    }

    IEnumerator ReturnBulletToPool()
    {
        yield return new WaitForSeconds(4f);

        if (gameObject.activeSelf)
        {
            PoolableObjects.Instance.ReturnObject(type, gameObject);
        }
    }
}

public enum BulletType
{
    Fast,
    Normal,
    Slow,
    Player
}
