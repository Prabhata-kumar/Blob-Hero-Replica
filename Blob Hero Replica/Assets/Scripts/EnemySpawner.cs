using UnityEngine;
using System.Collections.Generic;

namespace BlobHero
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab;  // Reference to the enemy prefab
        public int totalEnemies = 100;  // Total number of enemies
        public int maxActiveEnemies = 80;  // Maximum active enemies on screen
        public float spawnInterval = 1f;  // Time interval to check and spawn enemies
        public Transform[] spawnPoints;  // Spawn points for enemies

        private List<GameObject> enemyPool = new List<GameObject>();  // Pool of enemies
        private List<GameObject> activeEnemies = new List<GameObject>();  // Active enemies list

        void Start()
        {
            InitializeEnemyPool();
            InvokeRepeating("CheckAndSpawnEnemies", 0f, spawnInterval);
        }

        // Initialize enemy pool
        private void InitializeEnemyPool()
        {
            for (int i = 0; i < totalEnemies; i++)
            {
                GameObject enemy = Instantiate(enemyPrefab);
                enemy.SetActive(false);  // Set all enemies to inactive initially
                enemyPool.Add(enemy);
            }
        }

        // Check and spawn enemies if needed
        private void CheckAndSpawnEnemies()
        {
            int activeEnemyCount = activeEnemies.Count;

            if (activeEnemyCount < maxActiveEnemies)
            {
                int enemiesToSpawn = maxActiveEnemies - activeEnemyCount;

                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    SpawnEnemy();
                }
            }
        }

        // Spawn an enemy from the pool
        private void SpawnEnemy()
        {
            GameObject enemy = GetInactiveEnemyFromPool();

            if (enemy != null)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                enemy.transform.position = spawnPoint.position;  // Spawn at a random spawn point
                enemy.SetActive(true);  // Activate the enemy
                activeEnemies.Add(enemy);
            }
        }

        // Get an inactive enemy from the pool
        private GameObject GetInactiveEnemyFromPool()
        {
            foreach (var enemy in enemyPool)
            {
                if (!enemy.activeInHierarchy)
                {
                    return enemy;
                }
            }

            return null;  // If no inactive enemy is available, return null
        }

        // Remove enemy from active list and return it to the pool
        public void ReturnEnemyToPool(GameObject enemy)
        {
            enemy.SetActive(false);
            activeEnemies.Remove(enemy);
        }
    }
}
