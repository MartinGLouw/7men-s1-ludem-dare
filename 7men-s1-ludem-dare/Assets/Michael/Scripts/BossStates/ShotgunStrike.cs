﻿using UnityEngine;
using Managers.Pool;

namespace Managers.BossStates
{
    public class ShotgunStrike : BossStateMachine
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            if (bossParent) bossParent.enabled = false;
            bossAnimator.SetTrigger("OnShotgunStrike");
            bossAnimator.SetBool(BossStates.ShotgunStrike.ToString(), true);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            if (bossParent) bossParent.enabled = true;
            bossAnimator.SetBool(BossStates.ShotgunStrike.ToString(), false);
        }

        public override void ChangeState(BossStateMachine bossState)
        {
        
        }

        public void ShotgunStrikeAnimEvent()
        {
            SoundManager.Instance.PlaySFX(7,0.5f);
            int numberOfProjectiles = 5; 
            float radius = 2.0f; 

            for (int j = 0; j < numberOfProjectiles; j++)
            {
                float angle = j * Mathf.PI * 2 / numberOfProjectiles;
                Vector3 ringOffset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
                Vector3 projectilePosition = gunSP.position + gunSP.rotation * ringOffset;
                PooledProjectileSpawner.Instance.SpawnProjectile(projectilePosition + ringOffset, BulletType.Normal, gunSP);
            }
            
        }
    }
}