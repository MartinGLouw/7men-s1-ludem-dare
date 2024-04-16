using System.Collections;
using Managers.Lawyer;
using UnityEngine;

namespace Managers.Enemies
{
    public class Sprayer : Enemy
    {

        public override IEnumerator EnemyAttackBehavior()
        {
            if (canShoot)
            {
                canShoot = false;
                enemyAnim.SetTrigger("OnAim");
                enemyAnim.SetBool("IsAttacking", true);
                for (int i = 0; i < 8; i++)
                {
                    GameObject bullet = PoolableObjects.Instance.GetObject(BulletType.Normal, firePoint.position);
                    bullet.GetComponent<Rigidbody>().velocity = firePoint.forward * 80;

                    yield return new WaitForSeconds(0.1f); // Adjust this to control the speed of the rapid fire
                }
                
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