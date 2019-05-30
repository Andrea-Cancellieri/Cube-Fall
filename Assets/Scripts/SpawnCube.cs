using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{

	// Public var
	[Header("Cube Prefabs")]
	public GameObject NormalCube;
	public GameObject GreatCube;
	public GameObject TriCube;


	// Serialized var
	[Header("Spawn Time")]
	[Header(" ")]
	[SerializeField] protected float minTime;
	[SerializeField] protected float maxTime;
	[SerializeField] protected float minTimereduction;
	[SerializeField] protected float maxTimereduction;

	[Header("Horizontal:")]
	[Header("Spawn Positions")]
	[SerializeField] protected int minSpawnPosition;
	[SerializeField] protected int maxSpawnPosition;
	[SerializeField] int minSpawnPositionTriCube = -20;
	[SerializeField] int maxSpawnPositionTriCube = 20;
	[Header("Vertical:")]
	[SerializeField] protected int spawnPositionY;


	// Protected var
	protected GameObject Manager;

	protected float randomSpawnTime;
	protected float randomPositionX;


	// Private var
	GameObject CurrentCube;

	float currentMinTime;
	float currentMaxTime;

	float bossDelay = 2f;					// spawn ball delay to add when a boss is in game

	// cubes initial spawn percentage
	float InitnormalCubePerc = 75f;
	float InitgreatCubePerc = 12.5f;
	float InittriCubePerc = 12.5f;
	
	// cubes spawn percentage to be changed during game
	float normalCubePerc = 75f;
	float greatCubePerc = 12.5f;
	float triCubePerc = 12.5f;

	float secondSpawnChance = 25f;

	// checks elapsed time to change cubes spawn time and percentage
	public void CheckTime()
	{
		float time = Manager.GetComponent<ScoreAndTime>().timer;
		
		// change spawn perc
		// if percentage of the three cube types are 33% each, stops function
		if (normalCubePerc > 33f)
		{
			normalCubePerc = InitnormalCubePerc - (time / 2f);
			greatCubePerc = InitgreatCubePerc + (time / 4f);
			triCubePerc = InittriCubePerc + (time / 4f);
		}


		// change spawn time
		if (currentMinTime > 1.5f)
		{
			currentMinTime = minTime - (time / minTimereduction);
			currentMaxTime = maxTime - (time / maxTimereduction);
		}
		else
			Time.timeScale = 1f;


		// chance of double spawn
		if(time >= 60f)
		{
			// spawn another cube simultaneously if true
			if(Random.Range(0f, 100f) <= secondSpawnChance)
			{
				if (GameObject.FindGameObjectWithTag("CubeSpawner") || GameObject.FindGameObjectWithTag("BossCube"))
					Invoke("SecondSpawn", randomSpawnTime * bossDelay);
				else
					Invoke("SecondSpawn", randomSpawnTime);
			}
			// increase till two minutes in the game, when it is equal to 50%
			if(time <= 120f)
			{
				secondSpawnChance = time / 2.4f;
			}
		}

		// change bossDelay
		if (time >= 60f && time < 120f)
			bossDelay = 1.5f;
		else if (time > 120f)
			bossDelay = 1f;

	}


	// Spawns current cube type based on elapsed time
	public virtual void Spawn()
	{
		// randomize the x-axys spawn position of cube
		randomPositionX = Random.Range(minSpawnPosition, maxSpawnPosition);

		bool foundCube = false;
		while (!foundCube)
		{
			// NormalCube
			if (Random.Range(0f, 100f) <= normalCubePerc)
			{
				foundCube = true;
				CurrentCube = NormalCube;
			}
			// GreatCube
			else if (Random.Range(0f, 100f) <= greatCubePerc)
			{
				foundCube = true;
				CurrentCube = GreatCube;
			}
			// Tri-Cube
			else if (Random.Range(0f, 100f) <= triCubePerc)
			{
				foundCube = true;
				// randomize the x-axys spawn position of Tri-cube
				randomPositionX = Random.Range(minSpawnPositionTriCube, maxSpawnPositionTriCube);
				CurrentCube = TriCube;
			}
		}

		// spawn chosen cube at random pos in range
		Instantiate(CurrentCube, new Vector3(randomPositionX, spawnPositionY, 0f), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));


		CheckTime();


		// Start a new timer for the next random spawn and invoke it
		randomSpawnTime = Random.Range(minTime, maxTime);

		// if there's one of the two bosses in the game, spawn a cube within a double delay time
		if (GameObject.FindGameObjectWithTag("CubeSpawner") || GameObject.FindGameObjectWithTag("BossCube"))
			Invoke("Spawn", randomSpawnTime * bossDelay);
		// else, spawn normally
		else
			Invoke("Spawn", randomSpawnTime);

	}


	public void SecondSpawn()
	{
		GameObject CurrentCube2 = NormalCube;

		// randomize the x-axys spawn position of cube
		 float randomPositionX2 = Random.Range(minSpawnPosition, maxSpawnPosition);

		bool foundCube2 = false;
		while (!foundCube2)
		{
			// NormalCube
			if (Random.Range(0f, 100f) <= normalCubePerc)
			{
				foundCube2 = true;
				CurrentCube2 = NormalCube;
			}
			// GreatCube
			else if (Random.Range(0f, 100f) <= greatCubePerc)
			{
				foundCube2 = true;
				CurrentCube2 = GreatCube;
			}
			// Tri-Cube
			else if (Random.Range(0f, 100f) <= triCubePerc)
			{
				foundCube2 = true;
				// randomize the x-axys spawn position of Tri-cube
				randomPositionX2 = Random.Range(minSpawnPositionTriCube, maxSpawnPositionTriCube);
				CurrentCube2 = TriCube;
			}
		}

		// spawn chosen cube at random pos in range
		Instantiate(CurrentCube2, new Vector3(randomPositionX2, spawnPositionY, 0f), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

	}


	// Start is called before the first frame update
	void Start()
	{
		currentMinTime = minTime;
		currentMaxTime = maxTime;

		Manager = GameObject.FindGameObjectWithTag("GameManager");
	}

}
