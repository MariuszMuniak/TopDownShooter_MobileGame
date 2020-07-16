using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Core
{
    public class EnemyGenerator : MonoBehaviour
    {
        [SerializeField] SpawnPoint[] spawnPoints = new SpawnPoint[0];
        [Space]
        [SerializeField] GameObject zombie = null;
        [SerializeField] Wave[] waves = new Wave[1];

        Wave currentWave = null;
        int waveIndex = 0;
        bool isGenerating = false;
        float timeSinceLastWave = Mathf.Infinity;
        float timeToNextWave = 0f;

        private void Start()
        {
            StartSpawnWaves();
        }

        private void Update()
        {
            if (CanSpawn())
            {
                SpawnWaves();
            }

            timeSinceLastWave += Time.deltaTime;
        }

        private void StartSpawnWaves()
        {
            isGenerating = true;
        }

        private void StopSpawnWaves()
        {
            isGenerating = false;
        }

        private bool CanSpawn()
        {
            return isGenerating && timeSinceLastWave > timeToNextWave;
        }

        private void SpawnWaves()
        {
            Wave wave = waves[waveIndex];
            currentWave = wave;
            SpawnPoint spawnPoint = RandomSpawnPoint();

            for (int i = 0; i < currentWave.zombieNumber; i++)
            {
                Instantiate(zombie, RandomWorldPositionInSpawnArea(spawnPoint), transform.rotation);
            }

            timeToNextWave = wave.timeToNextWave;
            timeSinceLastWave = 0f;

            NextWaveIndex();
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

        private SpawnPoint RandomSpawnPoint()
        {
            SpawnPoint spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            return spawnPoint;
        }

        private void NextWaveIndex()
        {
            waveIndex++;

            if (waveIndex >= waves.Length)
            {
                StopSpawnWaves();
            }
        }

        [System.Serializable]
        private class Wave
        {
            public int zombieNumber;
            public float timeToNextWave;
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
