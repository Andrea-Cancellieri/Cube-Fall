using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCube : CubeController
{

	// if it has no children, destroy gameobject and add score
	public void CheckForChildren()
	{
		// == 1 because when function is called the last child is dying but not destroyed yet
		if (transform.childCount == 1)
		{
			// gives score points
			if (transform.position.y > -20f)
			{
				Killed();
			}
			// when killed, 50% to spawn another random cube
			if(Random.Range(0,2) == 0)
				GameObject.FindGameObjectWithTag("GameManager").GetComponent<SpawnCube>().SecondSpawn();

			Destroy(gameObject);
		}
	}


	protected override void SmokeDestruction()
	{
	}

	protected override void OnCollisionEnter2D(Collision2D collision)
	{
	}

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
	}
}
