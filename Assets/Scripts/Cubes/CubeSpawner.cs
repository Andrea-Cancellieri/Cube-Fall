using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
	[Header("Stats")]
	public int health;
	public int cubePoints;

	[Header(" ")]

	public GameObject NormalCube;
	public GameObject GreatCube;

	public GameObject SmokeCreationFX;
	public GameObject SmokeDestructionFX;

	GameObject Manager;


	// operation to do when killed
	void Dying()
	{
		Killed();
		SmokeDestruction();

		// when killed, 50% to spawn another random cube
		if (Random.Range(0, 2) == 0)
			GameObject.FindGameObjectWithTag("GameManager").GetComponent<SpawnCube>().SecondSpawn();

		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		// collision with Great Ball (Atk = 2)
		if (collision.gameObject.CompareTag("GreatBall"))
		{
			health -= collision.gameObject.GetComponent<GreatlBall>().Atk;
		}
		// collision with every other ball (Atk = 1)
		else
			health -= 1;


		// checks health
		if (health <= 0)
		{
			Dying();
		}

	}


	// bomb explosion collision event
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("BombBall"))
		{
			health -= collision.gameObject.GetComponent<BombExplosion>().Atk;
		}

		// destroy immediately the explosion erased its health
		if (health <= 0)
		{
			Dying();
		}

	}


	// instancitates smoke creation prefab 
	void SmokeCreation()
	{
		GameObject Smoke = Instantiate(SmokeCreationFX, transform.position, Quaternion.Euler(0f, 0f, 180f));

		Smoke.transform.localScale = new Vector3(0.3f * GetComponent<BoxCollider2D>().size.x,
												 0.3f * GetComponent<BoxCollider2D>().size.x,
												 1);
	}


	void SmokeDestruction()
	{
		GameObject Smoke = Instantiate(SmokeDestructionFX, transform.position, Quaternion.identity);

		Smoke.transform.localScale = new Vector3(0.4f * GetComponent<BoxCollider2D>().size.x,
												 0.4f * GetComponent<BoxCollider2D>().size.x,
												 1);
	}


	// starts moving horizontally after delay
	IEnumerator StartMoving()
	{
		yield return new WaitForSeconds(1f);

		// start animation
		if (transform.position.x < 0)
			GetComponent<Animator>().SetBool("Right", true);
		else
			GetComponent<Animator>().SetBool("Left", true);
	}


	IEnumerator SpawnNormalCube()
	{
		yield return new WaitForSeconds(Random.Range(0.5f, 2.5f));

		if(Random.Range(0, 2) == 0)
			Instantiate(NormalCube, new Vector3(transform.position.x, transform.position.y-5f, 0), Quaternion.identity);
		else
			Instantiate(GreatCube, new Vector3(transform.position.x, transform.position.y - 5f, 0), Quaternion.identity);
	}


	// Adds points to the score when destroyed
	void Killed()
	{
		Manager.GetComponent<ScoreAndTime>().UpdateScore(cubePoints);
	}


	// Start is called before the first frame update
	void Start()
	{
		Manager = GameObject.FindGameObjectWithTag("GameManager");

		StartCoroutine(StartMoving());
		SmokeCreation();
	}

}
