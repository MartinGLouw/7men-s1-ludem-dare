using UnityEngine;

namespace Managers.BossStates
{
    public class Idle : BossStateMachine
    {
        
    }
    
    public class FrontKick : BossStateMachine
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
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

        public void BossShoot()
        {
            Debug.Log("Shoot");
        }
        
    }
    
    public class MegaStomp : BossStateMachine
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
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

        public void BossShoot()
        {
            Debug.Log("Shoot");
        }
        
    }
    
    public class ShotgunStrike : BossStateMachine
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
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

        public void BossShoot()
        {
            Debug.Log("Shoot");
        }
        
    }
    
    public class HeavyStrike : BossStateMachine
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
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

        public void BossShoot()
        {
            Debug.Log("Shoot");
        }
        
    }
    
    public class CallToArms : BossStateMachine
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
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

        public void BossShoot()
        {
            Debug.Log("Shoot");
        }
        
    }
    
    public class Flee : BossStateMachine
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
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

        public void BossShoot()
        {
            Debug.Log("Shoot");
        }
        
    }
}