using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Managers.Lawyer
{
    public class ProsecutorLawyer : Lawyer
    {
        public float fireRate = 0.5f;
        public Transform lawyer;
        public Transform bulletSpawnPos;
        public GameObject projectilePrefab;
        
        private float _fireRateTimer = 0;
        
        private List<GameObject> enemies = new();
        
        public override void OnSpawn()
        {
            base.OnSpawn();
        }

        public override void OnAttack()
        {
            base.OnAttack();
            if(GetClosestPlayer() == null) return;

            AimInClosestEnemyDirection();
                
            if (_fireRateTimer <= 0)
            {
                Shoot();
                _fireRateTimer = fireRate;
            }
            else
            {
                _fireRateTimer -= Time.deltaTime;
            }
        }

        public override void OnDeath()
        {
            base.OnDeath();
        }

        Vector3 AimInClosestEnemyDirection()
        {
            Vector3 bulletDirection = transform.position - GetClosestPlayer().transform.position;
            bulletSpawnPos.up = bulletDirection;
            lawyer.forward = -bulletDirection;
            return bulletDirection;
        }
        
        
        void Shoot()
        {
            Vector3 bulletDirection = AimInClosestEnemyDirection();
            //ProjectileSpawner.Instance.SpawnPlayerProjectiles(bulletSpawnPos.position, -bulletDirection);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<EnemyHandling>(out EnemyHandling enemy))
            {
                enemies.Add(enemy.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<EnemyHandling>(out EnemyHandling enemy))
            {
                enemies.Remove(enemy.gameObject);
            }
        }

        GameObject GetClosestPlayer()
        {
            if (enemies.Count == 0) return null;

            float closestDistance = Mathf.Infinity;
            GameObject closestEnemy = null;
            
            foreach (var enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;
        }
    }
}