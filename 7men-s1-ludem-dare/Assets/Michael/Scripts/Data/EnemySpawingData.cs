using UnityEngine;

namespace Managers.Data
{
    [CreateAssetMenu(menuName = "", fileName = "New Enemy Spawning Data")]
    public class EnemySpawningData : ScriptableObject
    {
        public EnemyData[] enemies;
    }

    public struct EnemyData
    {
        public GameObject enemyPrefab;
        public int weighting;
        public int enemyTier;
    }
}