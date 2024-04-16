using UnityEngine;
using UnityEngine.Rendering;

namespace Managers.Pool
{
    public class PooledProjectileSpawner : Singleton<PooledProjectileSpawner>
    {
        public PoolableObjects poolObjects;
        public Transform player;

        public void SpawnProjectile(Vector3 start, BulletType type, Transform spawn)
        {
            GameObject newProjectile = poolObjects.GetObject(type, start);

            if (newProjectile.TryGetComponent<Bullet>(out Bullet projectile))
            {
                
                var speed = projectile.speed;
                Debug.Log($"speed: {speed}");

                Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
                Vector3 direction = new Vector3(spawn.up.x, 0, spawn.up.z).normalized;
                rb.velocity = direction * speed;
            }
            
            
        }
        
        public void SpawnPlayerProjectiles(Vector3 start, BulletType type, Vector3 direction)
        {
            GameObject newProjectile = poolObjects.GetObject(type, start);
            if (newProjectile.TryGetComponent<Bullet>(out Bullet projectile))
            {
                var speed = projectile.speed;
                Debug.Log($"speed: {speed}");
                Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
                newProjectile.GetComponent<Rigidbody>().velocity = new Vector3(
                    direction.x * speed, 
                    0f, direction.z * speed
                    );
                
            }

            //newProjectile.GetComponent<Rigidbody>().velocity = new Vector3(direction.x * projectiles[playerProjectile].speed, 0f, direction.z * projectiles[playerProjectile].speed);
        }
        
        private Vector3 GetPlayerDirection(Vector3 start)
        {
            return player.position - start;
        }
    }
}