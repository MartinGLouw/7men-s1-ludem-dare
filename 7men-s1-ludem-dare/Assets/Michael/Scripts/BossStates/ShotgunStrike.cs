using UnityEngine;
using Managers.Pool;

namespace Managers.BossStates
{
    public class ShotgunStrike : BossStateMachine
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            bossAnimator.SetTrigger("OnShotgunStrike");
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

        public void ShotgunStrikeAnimEvent()
        {
            Vector3 pos = gunSP.position;
            for (int i = -2; i < 2; i++)
            {

                pos += new Vector3(pos.x * i, 0, 0);
                PooledProjectileSpawner.Instance.SpawnProjectile(gunSP.position, BulletType.Fast);
            }
            
        }
    }
}