using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    //Shoot
    public ProjectileSpawner projectileSpawner;

    bool canInvoke = true;

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
        if (canInvoke)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canInvoke = false;

        projectileSpawner.SpawnBasicEnemyProjectile(transform.position, transform.forward);

        projectileSpawner.SpawnSlowEnemyProjectile(transform.position, transform.forward);

        projectileSpawner.SpawnFastEnemyProjectile(transform.position, transform.forward);

        yield return new WaitForSeconds(2);

        canInvoke = true;
    }

}
