using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUp : MonoBehaviour
{
	// Public var

	[Header("Time")]
	public float minTime;
	public float maxTime;

	[Header("PowerUp Prefabs")]
	public GameObject Life;
	public GameObject GreatBall;
	public GameObject TriBall;
	public GameObject BombBall;

	public GameObject HealthBar;

	[Header("PowerUp Buttons")]
	public GameObject GreatBallButton;
	public GameObject TriBallButton;
	public GameObject BombBallButton;


	// Private var
	float spawnDelay;

	// stores the spawn percentage of each power up
	float lifeSpawnPerc = 25f;
	float BallSpawnPerc = 25;

	// spawn position
	int spawnPositionX;
	int spawnPositionY;


	public void Spawn()
	{
		spawnDelay = Random.Range(minTime, maxTime);

		// stores which ball power up can spawn or not
		bool canGreatBallSpawn = true;
		bool canTriBallSpawn = true;
		bool canBombBallSpawn = true;


		// randomize the spawn position between certain range
		int randomChoice = Random.Range(0, 2);
		if (randomChoice == 0)
			spawnPositionX = -16;
		else
			spawnPositionX = 16;

		spawnPositionY = Random.Range(-10, 15);

		// takes health value from health bar
		int health = HealthBar.GetComponent<HealthController>().health;
		
		// if health is over 75% dont spawn life power up
		if (health > 15)
		{
			lifeSpawnPerc = 0f;
		}
		else
			lifeSpawnPerc = 25f + ((15 - health) * 2.5f);

		// life spawning code
		if (Random.Range(0f, 100f) <= lifeSpawnPerc)
		{
			Instantiate(Life, new Vector3(spawnPositionX, spawnPositionY, 0), Quaternion.identity);
			Invoke("Spawn", spawnDelay);
			return;
		}

		// else

		// checks ball power ups  ammo (needed to calculated their own spawn perc.)
		int remainingPowerUps = 3;
		if (GreatBallButton.GetComponent<PowerUpButton>().currentAmmo > 7)
		{
			canGreatBallSpawn = false;
			remainingPowerUps--;
		}
		if (TriBallButton.GetComponent<PowerUpButton>().currentAmmo > 7)
		{
			canTriBallSpawn = false;
			remainingPowerUps--;
		}
		if (BombBallButton.GetComponent<PowerUpButton>().currentAmmo > 7)
		{
			canBombBallSpawn = false;
			remainingPowerUps--;
		}

	// spawning code
		// if no ball powerUp can spawn, spawn life powerUp
		if(remainingPowerUps == 0)
		{
			Instantiate(Life, new Vector3(spawnPositionX, spawnPositionY, 0), Quaternion.identity);
		}
		else
		{
			// calculates ball power ups spawn percentage
			BallSpawnPerc = 100 / remainingPowerUps;

			//Debug.Log("BallSpawnPerc = " + BallSpawnPerc);

			bool foundPowerUp = false;
			while (!foundPowerUp)
			{
				// greatball
				if (canGreatBallSpawn && Random.Range(0f, 100f) <= BallSpawnPerc)
				{
					Instantiate(GreatBall, new Vector3(spawnPositionX, spawnPositionY, 0), Quaternion.identity);
					foundPowerUp = true;
				}
				// triball
				else if (canTriBallSpawn && Random.Range(0f, 100f) <= BallSpawnPerc)
				{
					Instantiate(TriBall, new Vector3(spawnPositionX, spawnPositionY, 0), Quaternion.identity);
					foundPowerUp = true;
				}
				// bombball
				else if (canBombBallSpawn && Random.Range(0f, 100f) <= BallSpawnPerc)
				{
					Instantiate(BombBall, new Vector3(spawnPositionX, spawnPositionY, 0), Quaternion.identity);
					foundPowerUp = true;
				}

			}
		}

		// loop the script
		Invoke("Spawn", spawnDelay);

	}

}
