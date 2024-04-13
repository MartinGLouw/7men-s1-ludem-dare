using System;
using UnityEngine;

namespace Events
{
    public class EnemyEvents
    {
        public event Action<Vector3> OnEnemyDeath;
        public event Action OnBossExit;
        public event Action OnBossEnter;
        public event Action<Vector3> OnBossDeath;

        public void FireEnemyDeathEvent(Vector3 deathPos)
        {
            OnEnemyDeath?.Invoke(deathPos);
        }
        
        public void FireBossExitEvent()
        {
            OnBossExit?.Invoke();
        }
        
        public void FireBossEnterEvent()
        {
            OnBossEnter?.Invoke();
        }
        
        public void FireBossDeathEvent(Vector3 deathPos)
        {
            OnEnemyDeath?.Invoke(deathPos);
        }

    }
}