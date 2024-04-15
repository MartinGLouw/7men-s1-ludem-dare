using UnityEngine;

namespace Managers.BossStates
{
    public class BulletStorm : BossStateMachine
    {
        private bool entered;
        
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("Entering State Bullet Storm");
            if (!entered)
            {
                bossAnimator.SetBool(BossStates.BulletStorm.ToString(), true);
                entered = true;
            }
            
        }

        public override void OnStateExit()
        {
            Debug.Log("Exiting State Bullet Storm");
            entered = false;
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