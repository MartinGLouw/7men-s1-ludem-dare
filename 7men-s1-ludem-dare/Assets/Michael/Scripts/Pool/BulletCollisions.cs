using System;
using Managers.Lawyer;
using UnityEngine;

namespace Managers.Pool
{
    public class BulletCollisions : MonoBehaviour
    {
        private Bullet bullet;
        private PoolableObjects _poolableObjects;

        private void Start()
        {
            _poolableObjects = PoolableObjects.Instance;
        }

        private void OnEnable()
        {
            bullet = GetComponent<Bullet>();
        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.TryGetComponent<IDamageable<Bullet>>(out IDamageable<Bullet> damageable))
            {
                damageable.TakeDamage(bullet);
            }
        
            if (other.gameObject.tag == "Player" && gameObject.tag == "EnemyProjectile")
            {
                Destroy(gameObject);
            }

            if (other.gameObject.tag == "Enemy" && gameObject.tag == "PlayerProjectile")
            {
                Destroy(gameObject);
            }
        }
    }
}