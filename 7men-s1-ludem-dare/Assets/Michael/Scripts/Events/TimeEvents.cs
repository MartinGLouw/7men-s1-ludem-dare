using System;

namespace Events
{
    public class TimeEvents
    {
        public event Action OnBossTimerFinished;
        public event Action OnStartCountdownCompleted;
        public event Action OnBossCooldownCompleted;

        public void FireTimerFinishedEvent()
        {
            OnBossTimerFinished?.Invoke();
        }

        public void FireStartCountdownDoneEvent()
        {
            OnStartCountdownCompleted?.Invoke();
        }
        
        public void FireBossCooldownCompleted()
        {
            OnBossCooldownCompleted?.Invoke();
        }
    }
}