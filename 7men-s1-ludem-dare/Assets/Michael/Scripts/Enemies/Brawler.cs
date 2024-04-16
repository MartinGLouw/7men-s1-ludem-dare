using System;
using System.Collections;
using Managers.Lawyer;
using UnityEngine;

namespace Managers.Enemies
{
    public class Brawler : Enemy
    {
        private Collider[] _playerCollider = new Collider[1];
        private bool _hit = true;

        public override IEnumerator EnemyAttackBehavior()
        {
            if (_hit)
            {
                _hit = false;
                // Play attack animation
                enemyAnim.SetTrigger("OnAttack");
                Physics.OverlapBoxNonAlloc(firePoint.position, cubeAttackSize, _playerCollider, Quaternion.identity, detectionLayer);
                Debug.Log("Brawler");
            
                foreach (var hit in _playerCollider)
                {
                    if (hit == null) continue;
                    if (hit.CompareTag("Player"))
                    {
                        Rigidbody rb = hit.GetComponent<Rigidbody>();
                        Vector3 direction = hit.transform.position - transform.position;
                        float distance = Vector3.Distance(hit.transform.position, transform.position);
                   
                        if (distance < 6 && isAttacking)
                        {
                            rb.AddForce(direction * meleeAttackForce);
                
                            if (hit.TryGetComponent<IDamageable<DamageData>>(out IDamageable<DamageData> player))
                            {
                                player.TakeDamage(damageData);
                            }
                        }
                    }
                }
            
                isAttacking = false; // Stop attacking after one attack
            
                yield return new WaitForSeconds(attackCooldown);
                _hit = true;     
            }
            
            yield return null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            //Gizmos.DrawWireSphere(transform.position, 10f);
            
            Gizmos.DrawCube(firePoint.position, cubeAttackSize);
        }
    }
}