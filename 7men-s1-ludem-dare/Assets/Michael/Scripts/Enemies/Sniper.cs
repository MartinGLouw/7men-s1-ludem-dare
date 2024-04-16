using System.Collections;
using Managers.Lawyer;
using UnityEngine;

namespace Managers.Enemies
{
    public class Sniper : Enemy
    {
        private Collider[] _playerCollider = new Collider[3];

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
                GameObject bullet = PoolableObjects.Instance.GetObject(BulletType.Slow, firePoint.position);
                bullet.GetComponent<Rigidbody>().velocity = firePoint.up * 100;
                yield return new WaitForSeconds(5); // Wait for 5 seconds before the next attack
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