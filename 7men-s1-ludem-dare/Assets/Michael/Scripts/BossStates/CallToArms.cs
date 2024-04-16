using UnityEngine;

namespace Managers.BossStates
{
    public class CallToArms : BossStateMachine
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            bossAnimator.SetTrigger("OnJump");
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public override void ChangeState(BossStateMachine bossState)
        {
        
        }
        
    }
}