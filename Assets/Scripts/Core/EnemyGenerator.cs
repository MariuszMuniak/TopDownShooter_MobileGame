using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDS_MG.Core
{
    public class EnemyGenerator : MonoBehaviour
    {
        [SerializeField] float spawnAreaRadius = 1f;
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

            for (int i = 0; i < currentWave.zombieNumber; i++)
            {
                Instantiate(zombie, RandomWorldPositionInSpawnArea(), transform.rotation);
            }

            timeToNextWave = wave.timeToNextWave;
            timeSinceLastWave = 0f;

            NextWaveIndex();
        }

        private Vector3 RandomWorldPositionInSpawnArea()
        {
            Vector3 position;

            do
            {
                position = new Vector3
                {
                    x = Random.Range(-spawnAreaRadius, spawnAreaRadius),
                    y = 0f,
                    z = Random.Range(-spawnAreaRadius, spawnAreaRadius)
                };
            } while (position.magnitude > spawnAreaRadius);

            return transform.TransformPoint(position);
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, spawnAreaRadius);
        }
    }
}
