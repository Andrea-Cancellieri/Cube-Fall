using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
	public GameObject BallType;
	public GameObject SmokeCreationFX;
	public GameObject SmokeDestructionFX;

	bool oneLapDone = false;

	int maxLapsAllowed = 4;

	GameObject ButtonToMatch;                               // reference to the respective ball button of the same type
	GameObject HealtBar;									// reference to the health bar 

	// decides if power up has to be destroyed every lap
	void DestroyChance()
	{
		maxLapsAllowed--;

		if (!oneLapDone)
			oneLapDone = true;
		else
		{
			if (maxLapsAllowed == 0)
			{
				Destroy(gameObject);
			}
			else
			{
				if (Random.Range(0, 2) == 0)
				{
					SmokeCreation();
					Destroy(gameObject);
				}

			}
		}

	}


	// starts moving horizontally after delay
	protected IEnumerator StartMoving()
	{
		yield return new WaitForSeconds(0.5f);

		// start animation
		if (transform.position.x < 0)
			GetComponent<Animator>().SetBool("Right", true);
		else
			GetComponent<Animator>().SetBool("Left", true);
	}


	// instancitates smoke creation prefab 
	void SmokeCreation()
	{
		GameObject Smoke = Instantiate(SmokeCreationFX, transform.position, Quaternion.identity);

		Smoke.transform.localScale = new Vector3(10f * GetComponent<BoxCollider2D>().size.x,
												 10f * GetComponent<BoxCollider2D>().size.x,
												 1);
	}


	void SmokeDestruction()
	{
		GameObject Smoke = Instantiate(SmokeDestructionFX, transform.position, Quaternion.identity);

		Smoke.transform.localScale = new Vector3(10f * GetComponent<BoxCollider2D>().size.x,
												 10f * GetComponent<BoxCollider2D>().size.x,
												 1);
	}

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(StartMoving());
		SmokeCreation();

		// if life powerUp spawned
		if (gameObject.CompareTag("LifePowerUp"))
			HealtBar = GameObject.FindGameObjectWithTag("HealthBar");
		// if every other powerUp spawned
		else
		{
			foreach (GameObject button in GameObject.FindGameObjectsWithTag("PowerUpButton"))
			{
				if (BallType.CompareTag(button.GetComponent<PowerUpButton>().BallToSwitch.tag))
				{
					ButtonToMatch = button;
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.layer == 12)
		{
			// increase ammo or life code
			if (gameObject.CompareTag("LifePowerUp"))
				HealtBar.GetComponent<HealthController>().IncreaseHealth();
			else
				ButtonToMatch.GetComponent<PowerUpButton>().IncreaseAmmo();

			SmokeDestruction();
			Destroy(gameObject);
		}
	}

}
