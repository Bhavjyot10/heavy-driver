using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemiesSpawning : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Camera mainCamera;
    public float initialSpawnDelay = 2f;
    public float timeBetweenWaves = 10f;
    public float waveDifficultyMultiplier = 1.2f;

    private int currentWave = 0;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(initialSpawnDelay);

        while (true)
        {
            currentWave++;
            Debug.Log("Wave " + currentWave);

            // Calculate difficulty for the wave
            float waveDifficulty = Mathf.Pow(waveDifficultyMultiplier, currentWave - 1);

            // Spawn enemies for the wave
            SpawnWaveEnemies(waveDifficulty);

            // Wait for the next wave
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void SpawnWaveEnemies(float waveDifficulty)
    {
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            // Adjust the number of enemies spawned per wave based on difficulty
            int enemiesToSpawn = Mathf.RoundToInt(currentWave * waveDifficulty);

            for (int j = 0; j < enemiesToSpawn; j++)
            {
                // Randomly select an enemy prefab
                GameObject randomEnemyPrefab = enemyPrefabs[i];

                // Spawn the enemy outside the camera's view
                Vector3 spawnPosition = GetRandomOffscreenPosition();
                Instantiate(randomEnemyPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    Vector3 GetRandomOffscreenPosition()
    {
        // Get the camera's current position
        Vector3 cameraPosition = mainCamera.transform.position;

        // Get a random point outside the camera's view
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        float spawnX = Random.Range(-cameraWidth, cameraWidth) * 2f + cameraPosition.x;
        float spawnY = Random.Range(-cameraHeight, cameraHeight) * 2f + cameraPosition.y;

        // Ensure the enemy spawns outside the camera bounds
        if (Random.value > 0.5f)
        {
            spawnX = Mathf.Sign(spawnX - cameraPosition.x) * cameraWidth * 1.5f + cameraPosition.x;
        }
        else
        {
            spawnY = Mathf.Sign(spawnY - cameraPosition.y) * cameraHeight * 1.5f + cameraPosition.y;
        }

        return new Vector3(spawnX, spawnY, 0f);
    }
}
