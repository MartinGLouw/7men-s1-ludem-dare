using System;
using Managers.Lawyer;
using UnityEngine;

namespace Managers.Pool
{
    public class BulletCollisions : MonoBehaviour
    {
        private Bullet _bulletData;
        private DamageData _damageData;
        private PoolableObjects _poolableObjects;

        private void Start()
        {
            _poolableObjects = PoolableObjects.Instance;
        }

        private void OnEnable()
        {
            _bulletData = GetComponent<Bullet>();
            _damageData = _bulletData.damageData;
        }

        private void OnTriggerEnter(Collider other)
        {
        
            if (other.gameObject.tag == "Player" && gameObject.tag == "EnemyProjectile")
            {
                if (other.TryGetComponent<IDamageable<DamageData>>(out IDamageable<DamageData> damageable))
                {
                    damageable.TakeDamage(_damageData);
                }
                
                PoolableObjects.Instance.ReturnObject(_bulletData.type, gameObject);
            }

            if (other.gameObject.tag == "Enemy" && gameObject.tag == "PlayerProjectile")
            {
                if (other.TryGetComponent<IDamageable<DamageData>>(out IDamageable<DamageData> damageable))
                {
                    damageable.TakeDamage(_damageData);
                }
                
                PoolableObjects.Instance.ReturnObject(_bulletData.type, gameObject);
            }
        }
    }
}