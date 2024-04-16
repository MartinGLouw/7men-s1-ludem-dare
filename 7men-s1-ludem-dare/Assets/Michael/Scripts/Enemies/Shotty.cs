using System.Collections;
using Managers.Lawyer;
using UnityEngine;

namespace Managers.Enemies
{
    public class Shotty : Enemy
    {
        public float speed = 0.1f;
        public float spreadVal = 10f;

        public override IEnumerator EnemyAttackBehavior()
        {
            if (canShoot)
            {
                canShoot = false;
                enemyAnim.SetTrigger("OnAim");
                enemyAnim.SetBool("IsAttacking", true);
                for (int i = 0; i < 5; i++)
                {
                    GameObject bullet = PoolableObjects.Instance.GetObject(BulletType.Slow, firePoint.position);
                    Vector3 spread = new Vector3((i - 2) * spreadVal, 0, 0); // Adjust the 2 here to control the spread
                    Vector3 forward = firePoint.forward * 90;
                    bullet.GetComponent<Rigidbody>().velocity = (spread + forward) * speed;
                }

                yield return new WaitForSeconds(1.5f);
                enemyAnim.SetBool("IsAttacking", false);    
                
                yield return new WaitForSeconds(attackCooldown);
                canShoot = true;
            }
        }
        
    }
}