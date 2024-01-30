using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

	public int waveNumber = 0;
	public int waveEnemyCount = 3;
	public float startDelayTime = 1f;
	public float wavesDelayTime = 10f;
	bool hasEnemyDeployed = false;
	bool hasStarted = false;
	public float enemySpawnDelay = 1f;
	

	public List<GameObject> enemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
	{
		StartCoroutine(StartDelay(startDelayTime));
    }

	// Update is called once per frame
	void Update()
	{
		Debug.Log(waveNumber);
		if (!hasStarted) return;
		if (GameObject.FindWithTag("Enemy") == null)
		{
			endWave();
		}
    }

	private IEnumerator StartDelay(float time_in_sec)
	{
		yield return new WaitForSeconds(time_in_sec);
		hasStarted = true;
		waveNumber++;
        yield return SpawnEnemy(waveEnemyCount++, 2, 8, enemySpawnDelay);
    }

	private IEnumerator SpawnEnemy(int count,int start, int end,float wait_time = 0)
	{
		for (int i = 0; i < count; i++)
		{
			
			
			GameObject newEnemy = Instantiate(enemies[0], new Vector2(2, 2), Quaternion.identity);
            Destroy(newEnemy, 5f);
            yield return new WaitForSeconds(wait_time);
        }
    }

	private void endWave()
	{
		hasStarted = false;
		StartCoroutine(StartDelay(wavesDelayTime));
	}

	
}
