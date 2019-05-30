using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpawnCube : SpawnCube
{
	public override void Spawn()
	{
		// randomize the x-axys spawn position of cube
		randomPositionX = Random.Range(minSpawnPosition, maxSpawnPosition);

		// spawn chosen cube at random pos in range
		Instantiate(NormalCube, new Vector3(randomPositionX, spawnPositionY, 0f), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

		// Start a new timer for the next random spawn and invoke it
		randomSpawnTime = Random.Range(minTime, maxTime);

		Invoke("Spawn", randomSpawnTime);
	}
}
