using Managers.Lawyer;
using UnityEngine;

namespace Managers.BossStates
{
    public class HeavyStrike : BossStateMachine
    {
        private Collider[] player = new Collider[3];
        public LayerMask layer;

        public Transform frontAttackPosition;
        public Vector3 cubeSize = new Vector3(4, 1.4f, 3);
        
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("Heavy");
            bossAnimator.SetTrigger("OnHeavySwipe");
            bossAnimator.SetBool(BossStates.HeavySwipe.ToString(), true);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            bossAnimator.SetBool(BossStates.HeavySwipe.ToString(), false);
        }

        public override void ChangeState(BossStateMachine bossState)
        {
        
        }

        public void HeavyStrikeAnimEvent()
        {
            Physics.OverlapBoxNonAlloc(frontAttackPosition.position, cubeSize, player, Quaternion.identity, layer);

            foreach (var hit in player)
            {
                if (hit == null) return;
                
                if (hit.CompareTag("Player"))
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();
                    Vector3 direction = hit.transform.position - transform.position;
                    rb.AddForce(direction * meleeAttackForce);
                    
                    if (hit.TryGetComponent<IDamageable<DamageData>>(out IDamageable<DamageData> player))
                    {
                        player.TakeDamage(damageData);
                    }
                }
            }
        }
        
        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            //Gizmos.DrawWireSphere(transform.position, 10f);
            
            Gizmos.DrawCube(frontAttackPosition.position, cubeSize);
        }
        
    }
}