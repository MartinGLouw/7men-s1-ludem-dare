using System;
using Managers.Lawyer;
using Unity;
using UnityEngine;

namespace Events
{
    public class PlayerEvents
    {
        public event Action OnPlayerDeath;
        public event Action<int> OnPlayerDamage;
        public event Action<Vector3> OnShoot;
        public event Action<GameObject> OnPlayerSpawn;
        public event Action<LawyerType, Vector3> OnSpawnLawyer;
        
        public void FirePlayerDamageEvent(int health)
        {
            OnPlayerDamage?.Invoke(health);
        }
        
        public void FirePlayerDeathEvent()
        {
            OnPlayerDeath?.Invoke();
        }
        
        public void FireOnShootEvent(Vector3 shootPos)
        {
            OnShoot?.Invoke(shootPos);
        }

        public void FirePlayerSpawnEvent(GameObject player)
        {
            OnPlayerSpawn?.Invoke(player);
        }

        public void FireSpawnLawyerEvent(LawyerType lawyer, Vector3 playerLocation)
        {
            OnSpawnLawyer?.Invoke(lawyer, playerLocation);
        }
    }
}