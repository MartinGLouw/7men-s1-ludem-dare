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
        public float meleeAttackForce = 150;
        public DamageData damageData;

        protected BossStateMachine runningState;
        protected Animator bossAnimator;
        protected Transform gunSP;

        private void OnEnable()
        {
            EventManager.Instance.GameManagerEvents.OnEndGame += StopActions;
            EventManager.Instance.GameManagerEvents.OnLoseGame += StopActions;
        }

        private void OnDisable()
        {
            EventManager.Instance.GameManagerEvents.OnEndGame -= StopActions;
            EventManager.Instance.GameManagerEvents.OnLoseGame -= StopActions;
        }

        public virtual void OnStateEnter()
        {
            bossAnimator = GetComponent<Animator>();
            gunSP = GetComponent<GunSP>().gunSP;
            runningState = this;
        }

        public virtual void OnStateUpdate()
        {
            
        }

        public virtual void OnStateExit() {  }

        public virtual void ChangeState(BossStateMachine bossState)
        {
            if (bossState == runningState) return;
            
            runningState.OnStateExit();
            bossState.OnStateEnter();

            runningState = bossState;
        }

        private void StopActions()
        {
            this.enabled = false;
        }
        
    }
    
    
}