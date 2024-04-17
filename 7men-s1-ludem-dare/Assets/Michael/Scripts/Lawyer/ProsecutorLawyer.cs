using System;
using System.Collections.Generic;
using System.Linq;
using Managers.Enemies;
using Managers.Pool;
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
            if(GetClosestEnemy() == null) return;

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
            enemies.Clear();
        }

        Vector3 AimInClosestEnemyDirection()
        {
            Vector3 bulletDirection = transform.position - GetClosestEnemy().transform.position;
            bulletSpawnPos.up = bulletDirection;
            lawyer.forward = -bulletDirection;
            return bulletDirection;
        }
        
        
        void Shoot()
        {
            if (enemies.Count == 0) return;
            
            Vector3 bulletDirection = AimInClosestEnemyDirection();
            PooledProjectileSpawner.Instance.SpawnPlayerProjectiles(bulletSpawnPos.position, BulletType.Player, -bulletDirection.normalized);
            //ProjectileSpawner.Instance.SpawnPlayerProjectiles(bulletSpawnPos.position, -bulletDirection);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                enemies.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                enemies.Add(other.gameObject);
            }
        }

        GameObject GetClosestEnemy()
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