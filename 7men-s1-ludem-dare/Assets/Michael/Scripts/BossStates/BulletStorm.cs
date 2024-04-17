using Managers.Pool;
using UnityEngine;

namespace Managers.BossStates
{
    public class BulletStorm : BossStateMachine
    {
        private bool entered;
        
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            
            bossAnimator.SetTrigger("OnBulletStorm");
            bossAnimator.SetBool(BossStates.BulletStorm.ToString(), true);
            entered = true;
            
        }

        public override void OnStateExit()
        {
            entered = false;
            bossAnimator.SetBool(BossStates.BulletStorm.ToString(), false);
        }

        public override void ChangeState(BossStateMachine bossState)
        {
            
        }

        public void BossShoot()
        {
            PooledProjectileSpawner.Instance.SpawnProjectile(gunSP.position, BulletType.Fast, gunSP);
        }
    }
}