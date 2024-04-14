using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ProjectileSpawner : MonoBehaviour
{
    public Projectiles[] projectiles;

    public int playerProjectile, enemyProjectile1;

    public int shotgunShots = 3;

    public bool[] canInvoke = new bool[] {true, true};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnPlayerProjectiles(Vector3 start, Vector3 direction)
    {
        GameObject newProjectile = Instantiate(projectiles[playerProjectile].prefab, start, Quaternion.identity);

        newProjectile.GetComponent<Rigidbody>().velocity = new Vector3 (direction.x * projectiles[playerProjectile].speed, 0f, direction.z * projectiles[playerProjectile].speed);

        yield return new WaitForSeconds(projectiles[playerProjectile].shotsDelta);

    }

    public IEnumerator SpawnShotgunProjectiles(Vector3 start, Vector3 direction)
    {
        canInvoke[1] = true;

        for (int i = 0; i < shotgunShots ; i++)
        {
            GameObject newProjectile = Instantiate(projectiles[enemyProjectile1].prefab, start, Quaternion.identity);

            newProjectile.GetComponent<Rigidbody>().velocity = new Vector3((direction.x + (i-1)) * projectiles[enemyProjectile1].speed, 0f, (direction.z + (i+1)) * projectiles[enemyProjectile1].speed);

            yield return new WaitForSeconds(projectiles[enemyProjectile1].shotsDelta);
        }

        canInvoke[1] = false;
    }

}
