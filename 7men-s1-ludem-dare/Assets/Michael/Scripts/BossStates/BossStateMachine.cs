using UnityEngine;

namespace Managers.BossStates
{
    public enum BossStates
    {
        BulletStorm,
        FrontKick,
        HeavySwipe,
        ShotgunStrike,
        CallToArms,
        MegaStomp,
        Fleeing
    }
    
    public abstract class BossStateMachine : MonoBehaviour
    {
        public Animator bossAnimator;

        public BossStates BossState;

        protected BossStateMachine runningState;

        public virtual void OnStateEnter()
        {
            runningState = this;
            Debug.Log($"Entering {runningState.name}");
        }

        public virtual void OnStateUpdate()
        {
            
        }

        public virtual void OnStateExit()
        {
            Debug.Log($"Exiting {BossState}");
        }

        public virtual void ChangeState(BossStateMachine bossState)
        {
            if (bossState == runningState) return;
            
            runningState.OnStateExit();
            bossState.OnStateEnter();

            runningState = bossState;


        }
    }
    
    
}