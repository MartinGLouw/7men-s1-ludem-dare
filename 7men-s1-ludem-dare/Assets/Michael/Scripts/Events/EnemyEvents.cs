using System;
using UnityEngine;

namespace Events
{
    public class EnemyEvents
    {
        public event Action<Vector3> OnEnemyDeath;
        public event Action<int> OnSpawnEnemies;
        public event Action OnBossExit;
        public event Action OnBossEnter;

        public event Action OnBossShockWave;
        
        public event Action<Vector3> OnBossDeath;

        public void FireEnemyDeathEvent(Vector3 deathPos)
        {
            OnEnemyDeath?.Invoke(deathPos);
        }

        public void FireOnSpawnEnemies(int phase)
        {
            OnSpawnEnemies?.Invoke(phase);
        }
        
        public void FireBossExitEvent()
        {
            OnBossExit?.Invoke();
        }
        
        public void FireBossShockwave()
        {
            OnBossShockWave?.Invoke();
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