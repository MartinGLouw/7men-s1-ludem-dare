using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public ProjectileSpawner projectileSpawner;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if(projectileSpawner.canInvoke[1])
        {
            StartCoroutine(projectileSpawner.SpawnShotgunProjectiles(gameObject.transform.position, gameObject.transform.forward));
        }
        
    }
}
