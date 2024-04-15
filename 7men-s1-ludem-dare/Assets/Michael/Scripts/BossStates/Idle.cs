using UnityEngine;

namespace Managers.BossStates
{
    public class Idle : BossStateMachine
    {
        
        public override void OnStateEnter()
        {
            Debug.Log("idle");
            base.OnStateEnter();
            runningState = this;
        }
    }
}