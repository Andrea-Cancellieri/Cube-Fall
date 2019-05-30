using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : SpawnCube
{
	[Header("Boss Prefabs")]
	public GameObject CubeSpawner;
	public GameObject BossCube;

	// cubes spawn percentage to be changed during game
	float cubeSpawnerPerc = 75f;
	float bossCubePerc = 25f;

	int BossNumber = 0;         // number of boss alreay spawned


	// checks number of bosses spawned to change boss spawn time and percentage
	new void CheckTime()
	{
		if (BossNumber == 3)
			return;

		if (BossNumber == 0)
		{
			cubeSpawnerPerc = 50f;
			bossCubePerc = 50f;
		}
		else if (BossNumber == 1)
		{
			minTime = 20f;
			maxTime = 30f;
		}

		BossNumber++;
	}

	// Spawns one of the two bossed randomly
	public override void Spawn()
	{

		bool foundBoss = false;
		while (!foundBoss)
		{
			// BossCube
			if (Random.Range(0f, 100f) <= bossCubePerc)
			{
				foundBoss = true;

				// randomize the x-axys spawn position of BossCube
				randomPositionX = Random.Range(minSpawnPosition, maxSpawnPosition);
				Instantiate(BossCube, new Vector3(randomPositionX, spawnPositionY, 0f), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
			}
			// CubeSpawner
			else if (Random.Range(0f, 100f) <= cubeSpawnerPerc)
			{
				foundBoss = true;

				// randomize the x-axys spawn position of Spawner between two values
				randomPositionX = Random.Range(0, 2);
				if (Random.Range(0, 2) == 0)
					randomPositionX = -14;
				else
					randomPositionX = 14;

				Instantiate(CubeSpawner, new Vector3(randomPositionX, 20, 0), Quaternion.identity);
			}
			
		}

		CheckTime();

		Invoke("Spawn", Random.Range(minTime, maxTime));
	}

}
