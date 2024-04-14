using System;
using System.Collections.Generic;
using Managers.Data;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    [RequireComponent(typeof(ProbabilityWeighting))]
    public class SpawnManager : MonoBehaviour
    {
        [Header("References: ")] 
        public Transform enemyParent;
        public EnemySpawningData spawningData;
        public Transform[] spawnLocations;
        public GameObject player;

        [Header("Spawn Amounts: ")] 
        public Vector2 phaseOneSpawnMinMax = new Vector2(5, 7);
        public Vector2 phaseTwoSpawnMinMax = new Vector2(9, 12);
        public Vector2 phaseThreeSpawnMinMax = new Vector2(15, 18);

        private EventManager _eventManager;
        private ProbabilityWeighting _probability;
        private List<float> _enemyPercentages = new();

        private void Start()
        {
            _eventManager = EventManager.Instance;
            _probability = GetComponent<ProbabilityWeighting>();

            _eventManager.EnemyEvents.OnSpawnEnemies += HandleEnemySpawn;
            
            foreach (var data in spawningData.Enemies)
            {
                _enemyPercentages.Add(data.SpawnPercentage);
            }
        }

        public void HandleEnemySpawn(int phase)
        {
            float randomNum;
            switch (phase)
            {
                case 1:
                    Debug.Log("Spawn enemies 1");
                    randomNum = Random.Range(phaseOneSpawnMinMax.x, phaseOneSpawnMinMax.y);
                    SpawnEnemies(randomNum);
                    break;
                    
                case 2:
                    randomNum = Random.Range(phaseTwoSpawnMinMax.x, phaseTwoSpawnMinMax.y);
                    SpawnEnemies(randomNum);
                    break;
                
                case 3:
                    randomNum = Random.Range(phaseThreeSpawnMinMax.x, phaseThreeSpawnMinMax.y);
                    SpawnEnemies(randomNum);
                    break;
            }
        }

        Vector3 GetRandomLocation()
        {
            int randomIndex = Random.Range(0, spawnLocations.Length);
            return spawnLocations[randomIndex].position;
        }

        GameObject GetRandomEnemiesBasedOnSpawnPercentage()
        {
            int itemIndex = _probability.GetItemByProbability(_enemyPercentages);

            return spawningData.Enemies[itemIndex].EnemyPrefab;
        }

        void SpawnEnemies(float numEnemiesToSpawn)
        {
            int num = (int)numEnemiesToSpawn;

            for (int i = 0; i < numEnemiesToSpawn; i++)
            {
                Debug.Log("Spawn enemies 2");
                Vector3 spawnPos = GetRandomLocation();
                GameObject enemy = GetRandomEnemiesBasedOnSpawnPercentage();

                GameObject enemySpawned = Instantiate(enemy, spawnPos, Quaternion.identity, enemyParent);
                EnemyHandling handling = enemySpawned.GetComponent<EnemyHandling>();
                handling.Init(player);

            }
        }
    }
}