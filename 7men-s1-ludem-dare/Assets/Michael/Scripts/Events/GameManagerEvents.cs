using System;

namespace Events
{
    public class GameManagerEvents
    {
        public event Action OnEndGame;
        public event Action OnLoseGame;
        public event Action OnGameStart;
        public event Action OnGamePause;
        
        public void FireEndGameEvent()
        {
            OnEndGame?.Invoke();
        }

        public void FireLoseGameEvent()
        {
            OnLoseGame?.Invoke();
        }
        
        public void FireGameStartEvent()
        {
            OnGameStart?.Invoke();
        }
        
        public void FireGamePauseEvent()
        {
            OnGamePause?.Invoke();
        }
        
    }
}