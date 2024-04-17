using Managers.Lawyer;
using UnityEngine;
using Managers.Pool;

namespace Managers.BossStates
{
    public class MegaStomp : BossStateMachine
    {
        private Collider[] player = new Collider[1];
        public LayerMask layer;
        
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            bossAnimator.SetTrigger("OnMegaStomp");
            bossAnimator.SetBool(BossStates.MegaStomp.ToString(), true);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            bossAnimator.SetBool(BossStates.MegaStomp.ToString(), false);
        }

        public override void ChangeState(BossStateMachine bossState)
        {
        
        }
        
        public void MegaStompEvent()
        {
            Physics.OverlapSphereNonAlloc(transform.position, 10f, player, layer);

            int numberOfProjectiles = 12; 
            float initialRadius = 2.0f; 
            float expandSpeed = 10f; 

            for (int j = 0; j < numberOfProjectiles; j++)
            {
                float angle = j * Mathf.PI * 2 / numberOfProjectiles;
                Vector3 ringOffset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * initialRadius;
                Vector3 projectilePosition = transform.position + ringOffset;
                GameObject projectile = PoolableObjects.Instance.GetObject(BulletType.Fast, transform.position);
                projectile.transform.position += new Vector3(0, 2, 0);
                
                Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
                Vector3 projectileDirection = (projectilePosition - transform.position).normalized;
                projectileRb.velocity = projectileDirection * expandSpeed;
            }
            
            foreach (var hit in player)
            {
                if (hit == null) return;
                if (hit.CompareTag("Player"))
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();
                    Vector3 direction = hit.transform.position - transform.position;
                    float distance = Vector3.Distance(hit.transform.position, transform.position);
                    if (distance < 5)
                    {
                        rb.AddForce(direction * meleeAttackForce);
                        
                        if (hit.TryGetComponent<IDamageable<DamageData>>(out IDamageable<DamageData> player))
                        {
                            player.TakeDamage(damageData);
                        }
                    }
                    
                    
                }
            }

           

        }
        
    }
}