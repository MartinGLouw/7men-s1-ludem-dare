using UnityEngine;

namespace Managers.BossStates
{
    public class BulletStorm : BossStateMachine
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("Entering State Bullet Storm");
            bossAnimator.SetBool(BossStates.BulletStorm.ToString(), true);
        }

        public override void OnStateExit()
        {
            Debug.Log("Exiting State Bullet Storm");
            bossAnimator.SetBool(BossStates.BulletStorm.ToString(), false);
        }

        public override void ChangeState(BossStateMachine bossState)
        {
            
        }

        public void BossShoot()
        {
            Debug.Log("Shoot");
        }
    }
}