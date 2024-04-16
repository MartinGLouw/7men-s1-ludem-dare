using System.Collections;
using Managers.Lawyer;
using UnityEngine;

namespace Managers.Enemies
{
    public class Shotty : Enemy
    {
        private Collider[] _playerCollider = new Collider[3];
        
        float retreatDistance = 5.0f; // The distance at which the enemy will start retreating
        

        public override void EnemyMovement()
        {
            Vector3 targetPosition = _player.transform.position;
            navMeshAgent.SetDestination(targetPosition);
        }

        public override IEnumerator EnemyAttackBehavior()
        {
            if (canShoot)
            {
                canShoot = false;
                for (int i = 0; i < 5; i++)
                {
                    GameObject bullet = PoolableObjects.Instance.GetObject(BulletType.Slow, firePoint.position);
                    Vector3 spread = new Vector3((i - 2) * 1, 0, 0); // Adjust the 2 here to control the spread
                    Vector3 forward = firePoint.up * 90;
                    bullet.GetComponent<Rigidbody>().velocity = spread + forward;
                }
                yield return new WaitForSeconds(5);
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