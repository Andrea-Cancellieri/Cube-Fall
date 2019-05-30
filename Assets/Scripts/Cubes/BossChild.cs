using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChild : GreatCube
{
	new void OnCollisionEnter2D(Collision2D collision)
	{
		// don't do anithing if collides with map wall or with the other boss children
		if (collision.gameObject.layer == 10 || collision.gameObject.layer == 13)
			return;

		// destroy if collides with base and deal damage
		if (collision.gameObject.layer == 8 && gameObject.layer != PHANTOM)
		{
			gameObject.layer = PHANTOM;

			// takes life points
			HealtBar.GetComponent<HealthController>().DecreaseHealth(gameObject);

			SmokeDestruction();
			transform.parent.GetComponent<BossCube>().CheckForChildren();
			Destroy(gameObject);
			return;
		}
		// if collides with something else
		// collision with Great Ball (Atk = 2)
		else if (collision.gameObject.CompareTag("GreatBall"))
		{
			health -= collision.gameObject.GetComponent<GreatlBall>().Atk;
		}
		// collision with every other ball (Atk = 1)
		else
			health -= 1;


		// checks health
		if (health <= 0)
		{
			Killed();
			SmokeDestruction();
			transform.parent.GetComponent<BossCube>().CheckForChildren();
			Destroy(gameObject);
		}

	}


	// bomb explosion collision event
	new void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("BombBall"))
			health -= collision.gameObject.GetComponent<BombExplosion>().Atk;

		// checks health
		if (health <= 0)
		{
			Killed();
			SmokeDestruction();
			transform.parent.GetComponent<BossCube>().CheckForChildren();
			Destroy(gameObject);
		}

	}


	// Start is called before the first frame update
	new void Start()
	{
		HealtBar = GameObject.FindGameObjectWithTag("HealthBar");
		Manager = GameObject.FindGameObjectWithTag("GameManager");
	}

}
