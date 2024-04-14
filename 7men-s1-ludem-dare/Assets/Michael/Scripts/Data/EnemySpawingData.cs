using System;
using UnityEngine;

namespace Managers.Data
{
    [CreateAssetMenu(menuName = "", fileName = "New Enemy Spawning Data")]
    public class EnemySpawningData : ScriptableObject
    {
        public EnemyData[] Enemies;
    }

    [Serializable]
    public struct EnemyData
    {
        public GameObject EnemyPrefab;
        [Header("Keep percentage sum under a 100")]
        public float SpawnPercentage;
        public int EnemyTier;
    }
}

