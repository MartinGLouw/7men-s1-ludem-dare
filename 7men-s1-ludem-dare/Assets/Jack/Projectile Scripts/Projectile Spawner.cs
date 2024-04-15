using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ProjectileSpawner : Singleton<ProjectileSpawner>
{
    public Projectiles[] projectiles;

    public int playerProjectile, enemyProjectile1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayerProjectiles(Vector3 start, Vector3 direction)
    {
        GameObject newProjectile = Instantiate(projectiles[playerProjectile].prefab, start, Quaternion.identity);

        newProjectile.GetComponent<Rigidbody>().velocity = new Vector3 (direction.x * projectiles[playerProjectile].speed, 0f, direction.z * projectiles[playerProjectile].speed);
    }

    public void SpawnBasicEnemyProjectile(Vector3 start, Vector3 direction)
    {
        GameObject newProjectile = Instantiate(projectiles[enemyProjectile1].prefab, start, Quaternion.identity);

        newProjectile.GetComponent<Rigidbody>().velocity = new Vector3(direction.x * projectiles[enemyProjectile1].speed, 0f, direction.z * projectiles[enemyProjectile1].speed);
    }
}
