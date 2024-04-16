using System;
using System.Collections;
using Managers.Lawyer;
using UnityEngine;

namespace Managers.Enemies
{
    public class Brawler : Enemy
    {
        private Collider[] _playerCollider = new Collider[3];

        public override void EnemyMovement()
        {
            Vector3 targetPosition = _player.transform.position;
            navMeshAgent.SetDestination(targetPosition);
        }

        public override IEnumerator EnemyAttackBehavior()
        {
            float attackCooldown = 0.8f; 
            float attackRange = attackingDistance;  

            while (true)
            {
                Debug.Log("Start Attack");
                float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

                if (distanceToPlayer <= attackRange && isAttacking)
                {
                    // Play attack animation
                    enemyAnim.SetTrigger("OnAttack");
                    Physics.OverlapBoxNonAlloc(firePoint.position, cubeAttackSize, _playerCollider, Quaternion.identity, detectionLayer);

                    foreach (var hit in _playerCollider)
                    {
                        Debug.Log("Attacking Fist");
                        if (hit == null) continue;
                        if (hit.CompareTag("Player"))
                        {
                            Rigidbody rb = hit.GetComponent<Rigidbody>();
                            Vector3 direction = hit.transform.position - transform.position;
                            rb.AddForce(direction * meleeAttackForce);
                            Debug.Log("Add Fighter Force");

                            if (hit.TryGetComponent<IDamageable<DamageData>>(out IDamageable<DamageData> player))
                            {
                                player.TakeDamage(damageData);
                            }
                        }
                    }
                    
                    isAttacking = false; // Stop attacking after one attack

                    yield return new WaitForSeconds(1f);
                    yield return new WaitForSeconds(attackCooldown);
                }

                yield return null; 
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            //Gizmos.DrawWireSphere(transform.position, 10f);
            
            Gizmos.DrawCube(firePoint.position, cubeAttackSize);
        }
    }
}