using System.Collections;
using Managers.Lawyer;
using UnityEngine;

namespace Managers.Enemies
{
    public class Sniper : Enemy
    {

        public override void Start()
        {
            base.Start();
            Vector3 direction = _player.transform.position - transform.position;
            transform.forward = direction;
        }

        public override IEnumerator EnemyAttackBehavior()
        {
            if (canShoot)
            {
                canShoot = false;
                enemyAnim.SetTrigger("OnAim");
                enemyAnim.SetBool("IsAttacking", true);
                SoundManager.Instance.PlaySFX(8, 0.5f);
                GameObject bullet = PoolableObjects.Instance.GetObject(BulletType.Fast, firePoint.position);
                bullet.GetComponent<Rigidbody>().velocity = firePoint.forward * 20;
                
                yield return new WaitForSeconds(1.5f);
                enemyAnim.SetBool("IsAttacking", false);   
                
                yield return new WaitForSeconds(attackCooldown); // Wait for 5 seconds before the next attack
                canShoot = true;
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