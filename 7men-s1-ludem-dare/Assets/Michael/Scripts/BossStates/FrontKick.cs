using Managers.Lawyer;
using UnityEngine;

namespace Managers.BossStates
{
    public class FrontKick : BossStateMachine
    {
        private Collider[] player = new Collider[1];
        public LayerMask layer;
        
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            bossAnimator.SetTrigger("OnFrontKick");
            bossAnimator.SetBool(BossStates.FrontKick.ToString(), true);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            bossAnimator.SetBool(BossStates.FrontKick.ToString(), false);
        }

        public override void ChangeState(BossStateMachine bossState)
        {
        
        }

        public void FrontKickLogic()
        {
            Debug.Log("Kicking");
            Physics.OverlapSphereNonAlloc(transform.position, 10f, player, layer);

            foreach (var hit in player)
            {
                if (hit.CompareTag("Player"))
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();
                    Vector3 direction = hit.transform.position - transform.position;
                    rb.AddForce(direction * 200);
                }
                //if(hit.TryGetComponent<IDamageable<DamageData>>())
            }
        }
        
    }
}