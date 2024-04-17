using Managers.Lawyer;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers.BossStates
{
    public class FrontKick : BossStateMachine
    {
        private Collider[] player = new Collider[1];
        public LayerMask layer;

        public Transform frontAttackPosition;
        public Vector3 cubeSize = new Vector3(2, 2, 4);
        
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            if (bossParent) bossParent.enabled = false;
            bossAnimator.SetTrigger("OnFrontKick");
            bossAnimator.SetBool(BossStates.FrontKick.ToString(), true);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            bossAnimator.SetBool(BossStates.FrontKick.ToString(), false);
            if (bossParent) bossParent.enabled = true;
        }

        public override void ChangeState(BossStateMachine bossState)
        {
        
        }

        public void FrontKickLogic()
        {
            GameObject test = null;
            SoundManager.Instance.PlaySFX(23, 2);
            
            Physics.OverlapBoxNonAlloc(frontAttackPosition.position, cubeSize, player, Quaternion.identity, layer);
            
            foreach (var hit in player)
            {
                if (hit == null) return;
                if (hit.CompareTag("Player"))
                {
                    test = hit.gameObject;
                    Rigidbody rb = hit.GetComponent<Rigidbody>();
                    Vector3 direction = hit.transform.position - transform.position;
                    float distance = Vector3.Distance(hit.transform.position, transform.position);
                    if (distance < 7)
                    {
                        rb.AddForce(direction * meleeAttackForce);
                        
                        if (hit.TryGetComponent<IDamageable<DamageData>>(out IDamageable<DamageData> player))
                        {

                            player.TakeDamage(damageData);
                        }
                    }
                    
                    
                }
                //if(hit.TryGetComponent<IDamageable<DamageData>>())
            }
            
        }
        
        void OnDrawGizmos()
        {
            
            Gizmos.color = Color.red;
            //Gizmos.DrawWireSphere(transform.position, 10f);
            
            Gizmos.DrawCube(frontAttackPosition.position, cubeSize);
        }
        
    }
}