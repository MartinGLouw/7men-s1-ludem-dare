using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bullet : MonoBehaviour
{
    public float speed;
    public BulletType type;

    public DamageData damageData;

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
