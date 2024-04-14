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

            _eventManager.EnemyEvents.OnBossDeath += HandleBossDeath;
            _eventManager.PlayerEvents.OnPlayerDeath += PlayerDeath;
        }

        void HandleBossDeath(Vector3 pos)
        {
            PlayerWonEndGame();
        }

        void PlayerWonEndGame()
        {
            //Pop up menu
            
        }

        void PlayerDeath()
        {
            
        }

        
    }
}