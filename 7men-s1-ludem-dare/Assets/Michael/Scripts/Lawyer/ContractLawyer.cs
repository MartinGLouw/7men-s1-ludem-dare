using System;
using UnityEngine;

namespace Managers.Lawyer
{
    public class ContractLawyer : Lawyer
    {
        public float bulletSlowMultiplier = 2f;
        
        public SphereCollider bulletArea;
        public override void OnSpawn()
        {
            base.OnSpawn();
            ActivateBulletSlow(true);
        }

        public override void OnAttack()
        {
            base.OnAttack();
        }

        public override void OnDeath()
        {
            base.OnDeath();
        }

        void ActivateBulletSlow(bool activate)
        {
            bulletArea.enabled = activate;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("EnemyProjectile"))
            {
                Rigidbody projectileRB = other.GetComponent<Rigidbody>();
                projectileRB.velocity /= bulletSlowMultiplier;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("EnemyProjectile"))
            {
                Rigidbody projectileRB = other.GetComponent<Rigidbody>();
                projectileRB.velocity *=  bulletSlowMultiplier;
            }
        }
    }

    public enum projectile
    {
        one,
        tweo,
        three
    }
}