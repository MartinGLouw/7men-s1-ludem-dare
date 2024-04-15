using System;
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
        public BossStates BossState;

        protected BossStateMachine runningState;
        protected Animator bossAnimator;
        protected Transform gunSP;

        public virtual void OnStateEnter()
        {
            bossAnimator = GetComponent<Animator>();
            gunSP = GetComponent<GunSP>().gunSP;
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
            
            Debug.Log("Changing state");
            runningState.OnStateExit();
            bossState.OnStateEnter();

            runningState = bossState;
        }
        
    }
    
    
}