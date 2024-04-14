using UnityEngine;

namespace Managers
{
    [CreateAssetMenu(menuName = "Mediator/ UI", fileName = "New UI Mediator")]
    public class GameData : ScriptableObject
    {
        [Header("UI")]
        public float TimeTaken;
        public int Health;
        public int BossHealth;
        [Header("Extra")]
        public int EnemiesInScene;
    }
}