using UnityEngine;
using UnityEngine.Rendering;

namespace Managers.Pool
{
    public class PooledProjectileSpawner : Singleton<PooledProjectileSpawner>
    {
        public PoolableObjects poolObjects;
        public Transform player;

        public void SpawnProjectile(Vector3 start, BulletType type)
        {
            GameObject newProjectile = poolObjects.GetObject(type, start);

            if (newProjectile.TryGetComponent<Bullet>(out Bullet projectile))
            {
                
                var speed = projectile.speed;
                Debug.Log($"speed: {speed}");

                Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
                rb.velocity = GetPlayerDirection(start) * speed;
            }
        }

        private Vector3 GetPlayerDirection(Vector3 start)
        {
            return player.position - start;
        }
    }
}