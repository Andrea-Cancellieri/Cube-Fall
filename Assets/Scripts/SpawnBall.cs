using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBall : MonoBehaviour
{

	// Public var
	public float spawnDelay;          // time delay between two possible shots

	// Ball Prefab Types
	[Header("Ball Prefabs")]
	[Header(" ")]
	public GameObject NormalBall;
	public GameObject GreatBall;
	public GameObject TriBall1;
	public GameObject TriBall2;
	public GameObject TriBall3;
	public GameObject BombBall;

	[Header(" ")]
	public GameObject CurrentBall;                              // reference to the ball prefab type to shoot
	
	[HideInInspector]
	public bool ballLoaded = false;								// tells if there is a ball ready to be shot or not

	// Private var
	GameObject BallInstance;
	GameObject SpawnPoint;                                      // ref to the Ball Platform positionù
	Vector3 TriBall1Position;
	Vector3 TriBall2Position;
	Vector3 TriBall3Position;


	public void BallListener()
	{
		ballLoaded = false;

		Invoke("CreateBall", spawnDelay);
	}


	// spawn ball at spawn point
	public void CreateBall()
	{
		ballLoaded = true;

		// Tri-ball instanciate code
		if (CurrentBall == TriBall1)
		{
			Instantiate(TriBall1, TriBall1Position, Quaternion.identity);
			Instantiate(TriBall2, TriBall2Position, Quaternion.identity);
			Instantiate(TriBall3, TriBall3Position, Quaternion.identity);
		}
		// every other ball instanciate code
		else
			Instantiate(CurrentBall, SpawnPoint.transform.position, Quaternion.identity);

		// if power up buttons were deactivated after one of them being pressed, activates every button in the scene again
		if (GameObject.FindGameObjectWithTag("PowerUpButton").GetComponent<Button>().interactable == false)
		{
			foreach (GameObject button in GameObject.FindGameObjectsWithTag("PowerUpButton"))
			{
				// enables only buttons that are not empty
				if(button.GetComponent<PowerUpButton>().isEmptyAmmo == false)
					button.GetComponent<Button>().interactable = true;
			}
		}

		// checks if createBall is called more than one time simultaneously
		CheckForClones();
	}


	// destroy all the ball clones, if there are some [Bug solver]
	void CheckForClones()
	{
		int BallCount = GameObject.FindGameObjectsWithTag(CurrentBall.tag).Length;

		// Tri-Ball code
		if (CurrentBall == TriBall1)
		{
			if (BallCount > 6)
			{
				int i = 0;
				foreach (GameObject Ball in GameObject.FindGameObjectsWithTag("Tri-Ball"))
				{
					// if ball isn't shot
					if (Ball.GetComponent<Collider2D>().enabled == false)
					{
						// destroy all but one ball
						if (i != 0)
						{
							//Debug.Log(i + " DOUBLE TRI-BALL ELIMINATED!");
							Destroy(Ball);
						}
						i++;
					}
				}
			}
		}
		// every other ball code
		else if (BallCount > 2)
		{
			int i = 0;
			foreach (GameObject Ball in GameObject.FindGameObjectsWithTag(CurrentBall.tag))
			{
				// if ball isn't shot
				if (Ball.GetComponent<Collider2D>().enabled == false)
				{
					// destroy all but one ball
					if (i != 0)
					{
						//Debug.Log(i + " DOUBLE ELIMINATED!");
						Destroy(Ball);
					}
					i++;
				}
			}
		}
	}


	// change ball type code
	public void SwitchCurrentBall(GameObject BallToSwitch)
	{
		
		foreach (GameObject Ball in GameObject.FindGameObjectsWithTag(CurrentBall.tag))
		{
			// checks if the ball isn't already shot, if so destroy it, change ball type and create new ball
			if (Ball.GetComponent<Collider2D>().enabled == false)
			{
				Destroy(Ball);
			}
				
		}
		
		// change ball type based on the power up button pressed (passed as the function argument)
		CurrentBall = BallToSwitch;

		// if there was aldready a ball loaded, calls CreateBall (otherwise it will not be called)
		if (ballLoaded)
		{
			CreateBall();
		}

	}


	// default change ball type code (if other balls ran out of ammo)
	public void SwitchCurrentBall()
	{
		// change ball to normalBall
		CurrentBall = NormalBall;
	}



	// Start is called before the first frame update
	void Start()
	{
		CurrentBall = NormalBall;
		SpawnPoint = GameObject.FindGameObjectWithTag("BallSpawnPoint");

		// calculates spawn position of the three balls
		TriBall1Position = new Vector3(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y + 1.5f, SpawnPoint.transform.position.z);
		TriBall2Position = new Vector3(SpawnPoint.transform.position.x - 1.25f, SpawnPoint.transform.position.y - 0.75f, SpawnPoint.transform.position.z);
		TriBall3Position = new Vector3(SpawnPoint.transform.position.x + 1.25f, SpawnPoint.transform.position.y - 0.75f, SpawnPoint.transform.position.z);
	}

}
