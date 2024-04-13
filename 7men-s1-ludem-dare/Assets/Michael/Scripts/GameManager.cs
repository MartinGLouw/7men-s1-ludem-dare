using System;
using UnityEngine;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {

        private EventManager _eventManager;

        private void Start()
        {
            _eventManager = EventManager.Instance;
            _eventManager.GameManagerEvents.FireGameStartEvent();
        }

        
    }
}