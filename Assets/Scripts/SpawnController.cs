using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

	public bool spawnOnStart = true;
	public float timeBetweenSpawns = 1;
	public List<Transform> spawnPositions;

	[SerializeField]
	private AtheistBaby atheistBabyPrefab;

	private bool isSpawning;

	private void Start()
	{
		if (spawnOnStart)
		{
			StartSpawning();
		}
	}

	public void StartSpawning()
	{
		StopAllCoroutines();
		isSpawning = true;
		StartCoroutine(SpawningCoroutine());
	}

	public void StopSpawning()
	{
		isSpawning = false;
	}

	private IEnumerator SpawningCoroutine()
	{
		yield return new WaitForSeconds(timeBetweenSpawns);
		while(isSpawning)
		{
			SpawnBaby();
			yield return new WaitForSeconds(timeBetweenSpawns);
		}
	}

	private void SpawnBaby()
	{
		Transform spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Count)];
		SpawnBaby(spawnPosition.position);
	}

	private void SpawnBaby(Vector3 position)
	{
		AtheistBaby spawnedBaby = Instantiate(atheistBabyPrefab);
		spawnedBaby.transform.position = position;
	}
}
