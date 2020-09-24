using System.Collections;
using System.Collections.Generic;
using TDS_MG.Attributes;
using UnityEngine;

namespace TDS_MG.Core
{
    public class EnemyGenerator : MonoBehaviour
    {
        [SerializeField] SpawnPoint[] spawnPoints = new SpawnPoint[0];
        [Space]
        [SerializeField] GameObject zombie = null;
        [SerializeField] Wave[] waves = new Wave[1];

        int waveIndex = 0;
        bool isGenerating = false;
        float timeSinceLastWave = Mathf.Infinity;
        float timeToNextWave = 0f;
        List<Health> instantiateZombies = new List<Health>();

        private void Start()
        {
            StartSpawnWaves();
        }

        private void Update()
        {
            if (CanSpawn())
            {
                SpawnZombieWave();
            }

            timeSinceLastWave += Time.deltaTime;
        }

        private void StartSpawnWaves()
        {
            isGenerating = true;
        }

        private bool CanSpawn()
        {
            return isGenerating && timeSinceLastWave >= timeToNextWave;
        }

        private void SpawnZombieWave()
        {
            Wave currentWave = waves[waveIndex];
            InstantiateZombiesAtRandomSpawnPoint(currentWave.zombieNumber);

            NextWaveIndex();

            timeToNextWave = currentWave.timeToNextWave;
            timeSinceLastWave = 0f;
        }

        private void InstantiateZombiesAtRandomSpawnPoint(int amount)
        {
            SpawnPoint spawnPoint = RandomSpawnPoint();

            for (int i = 0; i < amount; i++)
            {
                GameObject instantiateZombie = Instantiate(zombie, RandomWorldPositionInSpawnArea(spawnPoint), transform.rotation);
                instantiateZombies.Add(instantiateZombie.GetComponent<Health>());
            }
        }

        private SpawnPoint RandomSpawnPoint()
        {
            SpawnPoint spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            return spawnPoint;
        }

        private Vector3 RandomWorldPositionInSpawnArea(SpawnPoint spawnPoint)
        {
            Vector3 position;
            float radius = spawnPoint.areaRadius;

            do
            {
                position = new Vector3
                {
                    x = Random.Range(-radius, radius),
                    y = 0f,
                    z = Random.Range(-radius, radius)
                };
            } while (position.magnitude > radius);

            return spawnPoint.center.TransformPoint(position);
        }

        private void NextWaveIndex()
        {
            waveIndex++;

            if (waveIndex >= waves.Length)
            {
                StopSpawnWaves();
            }
        }

        private void StopSpawnWaves()
        {
            isGenerating = false;
        }

        public bool AreAllZombiesDead()
        {
            if (isGenerating)
            {
                return false;
            }

            foreach(Health instantiateZombie in instantiateZombies)
            {
                if (!instantiateZombie.IsDead())
                {
                    return false;
                }
            }

            return true;
        }

        [System.Serializable]
        private class Wave
        {
            public int zombieNumber = 0;
            public float timeToNextWave = 0f;
        }

        [System.Serializable]
        private class SpawnPoint
        {
            public Transform center = null;
            public float areaRadius = 1f;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;

            foreach (SpawnPoint spawnPoint in spawnPoints)
            {
                if (spawnPoint.center != null)
                {
                    Gizmos.DrawWireSphere(spawnPoint.center.position, spawnPoint.areaRadius);
                }
            }
        }
    }
}
