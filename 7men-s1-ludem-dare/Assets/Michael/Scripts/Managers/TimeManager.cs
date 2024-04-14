using System;
using System.Collections;
using UnityEngine;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {
        public float startCountdown;
        public float bossCooldown;
        public GameData gameData;
        
        private EventManager _eventManager;
        private float TimeTaken = 0;
        
        private void Start()
        {
            gameData.TimeTaken = 0;
            _eventManager = EventManager.Instance;
            StartCoroutine(GameStartCooldown());
        }

        private void Update()
        {
            gameData.TimeTaken += Time.deltaTime;
        }

        IEnumerator GameStartCooldown()
        {
            WaitForSeconds wait = new WaitForSeconds(startCountdown);
            yield return wait;
            _eventManager.TimeEvents.FireStartCountdownDoneEvent();
        }

        IEnumerator BossExitCooldown()
        {
            WaitForSeconds wait = new WaitForSeconds(bossCooldown);
            yield return wait;
            _eventManager.TimeEvents.FireBossCooldownCompleted();
        }
    }
}