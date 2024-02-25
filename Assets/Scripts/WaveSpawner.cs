using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{

	public int waveNumber = 0;
	public int waveEnemyCount = 3;
	private int enemyCount;
	public float startDelayTime = 5f;
	public float wavesDelayTime = 10f;
	
	public float enemySpawnDelay = 1f;

	public float innerRadius = 2f;
	public float outerRadius = 5f;
	

	public List<GameObject> enemies = new List<GameObject>();

	public GameObject Car;

	public static event Action OnWaveStart;
    // Start is called before the first frame update
    void Start()
	{
		OnWaveStart += StartSpawningCoroutine;
        StartCoroutine(StartDelay(startDelayTime));   
    }

	// Update is called once per frame
	void Update()
	{
		Debug.Log(waveNumber);

		//if(GameObject.FindWithTag("Enemy") == null && hasWaveStarted)
		//{
			//EndWave();
		//}
		
    }

	private IEnumerator StartDelay(float time_in_sec)
	{
		yield return new WaitForSeconds(time_in_sec);
		waveNumber+=1;		
		enemyCount = waveEnemyCount;
		OnWaveStart?.Invoke();
    }

	private IEnumerator SpawnEnemy()
	{    while (enemyCount != 0)
		{
			float xPos = Random.Range(0, 2) == 0 ? Random.Range(-outerRadius, -innerRadius) : Random.Range(innerRadius, outerRadius);
			float yPos = Random.Range(0, 2) == 0 ? Random.Range(-outerRadius, -innerRadius) : Random.Range(innerRadius, outerRadius);
			Vector3 enemyPosOffset = new Vector2(xPos, yPos);
			GameObject newEnemy = Instantiate(enemies[0], Car.transform.position + enemyPosOffset, Quaternion.identity);
			//Destroy(newEnemy, 5f);
			enemyCount--;
			yield return null;
		}
		EndWave();
    }

	private void EndWave()
	{
		//hasWaveStarted = false;
        waveEnemyCount += 1;
        StartCoroutine(StartDelay(wavesDelayTime));
	}
    void StartSpawningCoroutine()
    {
        StartCoroutine(SpawnEnemy());
    }

}
