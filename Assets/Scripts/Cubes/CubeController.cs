using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
	// Public var
	[Header("Stats")]
	public int health = 1;
	public int damage = 1;
	public int cubePoints;

	[Header("Spawn Force")]
	public float minForce = 1f;
	public float maxForce = 10f;

	[Header("Spawn Torque")]
	public float minTorque = 1f;
	public float maxTorque = 2.5f;

	[Header("Gravity Scale")]
	public float minGravity = -0.25f;
	public float maxGravity = 0.1f;

	[Header(" ")]
	public GameObject SmokeDestructionFX;
	
	
	// Protected var
	protected const int PHANTOM = 9;

	protected Rigidbody2D Rb;

	protected float randomForceX;
	protected float randomTorque;

	protected GameObject Manager;
	protected GameObject HealtBar;


	// instancitates smoke creation prefab
	protected virtual void SmokeDestruction()
	{
		GameObject Smoke = Instantiate(SmokeDestructionFX, transform.position, Quaternion.Euler(1f, 1f, Random.Range(1f, 360f)));

		Smoke.transform.localScale = new Vector3(GetComponent<BoxCollider2D>().size.x,
												 GetComponent<BoxCollider2D>().size.y,
												 1);
	}


	protected virtual void OnCollisionEnter2D(Collision2D collision)
	{
		// don't destroy if collides with map wall
		if (gameObject.layer == PHANTOM)
			return;

		// don't destroy if collides with map wall
		if (collision.gameObject.layer == 10)
			return;

		// if collides with base
		// destroy after 1 sec and deal damage
		if (collision.gameObject.layer == 8 && gameObject.layer != PHANTOM)
		{
			// takes life points
			HealtBar.GetComponent<HealthController>().DecreaseHealth(gameObject);

			// change layer type so it won't collide with anything else except base
			gameObject.layer = PHANTOM;
			Invoke("SmokeDestruction", 1f);
			Destroy(gameObject, 1f);
			return;
		}
		// if collides with something else
		// collision with Great Ball (Atk = 2)
		else if(collision.gameObject.CompareTag("GreatBall"))
		{
			health -= collision.gameObject.GetComponent<GreatlBall>().Atk;
		}
		// collision with every other ball (Atk = 1)
		else
			health -= 1;


		// checks health
		if(health <= 0)
		{
			gameObject.layer = PHANTOM;

			// destroy after 0.5 sec if collides with the other balls
			Invoke("Killed", 0.5f);
			Invoke("SmokeDestruction", 0.5f);
			Destroy(gameObject, 0.5f);
		}
		// destroy after 0.1 sec if collides with bomb ball
		else if (health == 0 && collision.gameObject.CompareTag("BombBall"))
		{
			gameObject.layer = PHANTOM;

			Invoke("Killed", 0.1f);
			Invoke("SmokeDestruction", 0.1f);
			Destroy(gameObject, 0.1f);
		}
			
	}


	// bomb explosion collision event
	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("BombBall"))
			health -= collision.gameObject.GetComponent<BombExplosion>().Atk;

		// destroy immediately the explosion erased its health
		if (health <= 0)
		{
			Killed();
			SmokeDestruction();
			Destroy(gameObject);
		}

	}


	// Adds points to the score when destroyed
	protected virtual void Killed()
	{
		Manager.GetComponent<ScoreAndTime>().UpdateScore(cubePoints);
	}


	// Start is called before the first frame update
	protected virtual void Start()
	{
		StartRoutine();
	}

	protected void StartRoutine()
	{
		Rb = GetComponent<Rigidbody2D>();
		Manager = GameObject.FindGameObjectWithTag("GameManager");
		HealtBar = GameObject.FindGameObjectWithTag("HealthBar");

		// randomize the gravity scale inside range (-0.25, 0.25)
		Rb.gravityScale += Random.Range(minGravity, maxGravity);

		// randomize the x-axys force to the cube based on the spawn position
		if (transform.position.x <= 0)
			randomForceX = Random.Range(minForce, maxForce);
		else
			randomForceX = -Random.Range(minForce, maxForce);

		// randomize torque
		if (Random.value <= 0.5)
			randomTorque = Random.Range(minTorque, maxTorque);
		else
			randomTorque = -Random.Range(minTorque, maxTorque);

		// set force and torque to cube
		Rb.AddForce(new Vector2(randomForceX, 0f), ForceMode2D.Impulse);
		Rb.AddTorque(randomTorque, ForceMode2D.Impulse);
	}
}
