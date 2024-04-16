using Managers.Lawyer;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ProjectileSpawner : MonoBehaviour
{
    public Projectiles[] projectiles;

    public int playerProjectile, normal, slow, fast;

    float speed;

    // Start is called before the first frame updat
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

        newProjectile.GetComponent<Rigidbody>().velocity = new Vector3(direction.x * projectiles[playerProjectile].speed, 0f, direction.z * projectiles[playerProjectile].speed);
    }
    
    public void SpawnBasicEnemyProjectile(Vector3 start, Vector3 direction)
    {
        GameObject newProjectile = Instantiate(projectiles[normal].prefab, start, Quaternion.identity);

        newProjectile.GetComponent<Rigidbody>().velocity = new Vector3(direction.x * projectiles[normal].speed, 0f, direction.z * projectiles[normal].speed);
    }

    public void SpawnFastEnemyProjectile(Vector3 start, Vector3 direction)
    {
        GameObject newProjectile = Instantiate(projectiles[fast].prefab, start, Quaternion.identity);

        newProjectile.GetComponent<Rigidbody>().velocity = new Vector3(direction.x * projectiles[fast].speed, 0f, direction.z * projectiles[fast].speed);
    }

    public void SpawnSlowEnemyProjectile(Vector3 start, Vector3 direction)
    {
        GameObject newProjectile = Instantiate(projectiles[slow].prefab, start, Quaternion.identity);

        newProjectile.GetComponent<Rigidbody>().velocity = new Vector3(direction.x * projectiles[slow].speed, 0f, direction.z * projectiles[slow].speed);
    }
    

}
