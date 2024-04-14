using System;
using UnityEngine;

namespace Managers
{
    [CreateAssetMenu(menuName = "Game Data", fileName = "New Game Data")]
    public class GameData : ScriptableObject
    {
        [Header("UI")]
        public float TimeTaken;
        public int BossHealth;
        
        [Header("Extra")]
        public int EnemiesInScene;

        [Header("Player")] 
        public GameObject Player;
        public int Health;
    }
}