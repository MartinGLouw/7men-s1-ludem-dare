using UnityEngine;

namespace Managers.BossStates
{
    public class Idle : BossStateMachine
    {
        
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            runningState = this;
        }
    }
}