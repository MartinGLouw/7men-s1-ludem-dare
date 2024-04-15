using UnityEngine;

namespace Managers.BossStates
{
    public class MegaStomp : BossStateMachine
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            bossAnimator.SetTrigger("OnMegaStomp");
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
        
        public void MegaStompEvent()
        {
            
        }
        
    }
}