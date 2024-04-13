using System;

namespace Events
{
    public class TimeEvents
    {
        public event Action OnBossTimerFinished;
        public event Action OnStartCountdownCompleted;

        public void FireTimerFinishedEvent()
        {
            OnBossTimerFinished?.Invoke();
        }

        public void FireStartCountdownEvent()
        {
            OnStartCountdownCompleted?.Invoke();
        }
    }
}