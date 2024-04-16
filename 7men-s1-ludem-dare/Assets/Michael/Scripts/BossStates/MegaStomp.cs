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
            Debug.Log("Entering Stomp");
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
            Debug.Log("Stomping");
            Physics.OverlapSphereNonAlloc(transform.position, 10f, player, layer);

            foreach (var hit in player)
            {
                if (hit == null) return;
                if (hit.CompareTag("Player"))
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();
                    Vector3 direction = hit.transform.position - transform.position;
                    rb.AddForce(direction * meleeAttackForce);
                    Debug.Log("Ring");
                }
            }
            
            
            int numberOfProjectiles = 6; 
            float radius = 2.0f; 

            for (int j = 0; j < numberOfProjectiles; j++)
            {
                float angle = j * Mathf.PI * 2 / numberOfProjectiles;
                Vector3 ringOffset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
                PooledProjectileSpawner.Instance.SpawnProjectile(gunSP.position + ringOffset, BulletType.Slow, gunSP);
            }
        }
        
    }
}