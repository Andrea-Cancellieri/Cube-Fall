using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class NormalBall : MonoBehaviour
{

	// Public var
	[Header("Ball Stats:")]
	public int Atk; 
	public int shotStrenght;
	public int ballCost;

	[Header(" ")]
	public GameObject Joystick;
	public GameObject Arrow;
	public GameObject SmokeCreationFX;
	public GameObject SmokeDestructionFX;

	[HideInInspector]
	public bool ballAlreadyShot = false;


	// Protected var
	protected const int PHANTOM = 9;
	protected GameObject Manager;
	protected GameObject Player;
	protected GameObject SpawnPoint;
	protected Rigidbody2D BallRb;
	protected Collider2D BallColl;
	protected Joystick JMovement;
	protected float horizontalInput;
	protected float verticalInput;
	protected Vector3 Direction;
	protected GameObject ArrowInstance;
	protected bool arrowDisplayed = false;
	protected Vector3 ArrowDirection;
	protected float screenHeight;
	protected const float screenUnit = 0.125f;                                              // represents the normalized screen height unit (screenHeight/8)


	// decrease the cost of the ball to the score on every shot
	protected void DecreaseScore()
	{
		Manager.GetComponent<ScoreAndTime>().UpdateScore(ballCost);
	}


	protected virtual void ShootBall()
	{
		ballAlreadyShot = true;
		BallRb.simulated = true;
		BallColl.enabled = true;

		BallRb.AddForce(Direction * shotStrenght, ForceMode2D.Impulse);

		Player.GetComponent<SpawnBall>().BallListener();									// calls manager function to spawn another ball

		// decrease ball ammo code
		foreach(GameObject button in GameObject.FindGameObjectsWithTag("PowerUpButton"))
		{
			if (button.GetComponent<PowerUpButton>().BallToSwitch.CompareTag(gameObject.tag))
			{
				button.GetComponent<PowerUpButton>().DecreaseAmmo();						// calls function to decrease button.currentAmmo
			}
		}

		// destroys remaining aiming arrow (Bug Solver)
		foreach (GameObject ArrowInstance in GameObject.FindGameObjectsWithTag("Arrow"))
			Destroy(ArrowInstance);

		DecreaseScore();
		Destroy(gameObject, 3f);
	}


	protected virtual void OnCollisionEnter2D(Collision2D collision)
	{
		//gameObject.layer = PHANTOM;
		Invoke("SmokeDestruction", 0.5f);
		Destroy(gameObject, 0.5f);
	}


	// Arrow code
	protected virtual void AimArrow()
	{
		// display arrow
		if (!arrowDisplayed)
		{
			arrowDisplayed = true;
			ArrowInstance = Instantiate(Arrow, transform.position, Quaternion.identity);
		}

		// rotate arrow
		ArrowDirection = Quaternion.AngleAxis(52.5f, Vector3.forward) * Direction;
		ArrowInstance.transform.up = ArrowDirection;

		// scale arrow base on direction magnitude
		ArrowInstance.transform.localScale = new Vector3(1.75f * Direction.magnitude, 1.75f * Direction.magnitude, 1f);
	}


	// instancitates smoke creation prefab
	protected virtual void SmokeCreation()
	{
		GameObject Smoke = Instantiate(SmokeCreationFX, transform.position, Quaternion.identity);

		Smoke.transform.localScale = new Vector3(2f * GetComponent<CircleCollider2D>().radius,
												 2f * GetComponent<CircleCollider2D>().radius,
												 1);
	}


	// instancitates smoke creation prefab
	protected virtual void SmokeDestruction()
	{
		GameObject Smoke = Instantiate(SmokeDestructionFX, transform.position, Quaternion.Euler(1f, 1f, Random.Range(1f, 360f)));

		Smoke.transform.localScale = new Vector3(2f * GetComponent<CircleCollider2D>().radius,
												 2f * GetComponent<CircleCollider2D>().radius,
												 1);
	}



	// Start is called before the first frame update
	protected virtual void Start()
	{
		StartRoutine();
		SmokeCreation();
	}

	protected void StartRoutine()
	{
		Player = GameObject.FindGameObjectWithTag("Player");

		SpawnPoint = GameObject.FindGameObjectWithTag("BallSpawnPoint");

		Manager = GameObject.FindGameObjectWithTag("GameManager");

		BallRb = GetComponent<Rigidbody2D>();
		BallRb.simulated = false;

		BallColl = GetComponent<Collider2D>();
		BallColl.enabled = false;

		Joystick = GameObject.FindGameObjectWithTag("Joystick");
		JMovement = Joystick.GetComponent<Joystick>();

		screenHeight = Screen.height;

	}

	// Update is called once per frame
	protected virtual void Update()
	{
		// checks if ball is already shot to stop the input
		UpdateRoutine();
	}

	protected virtual void UpdateRoutine()
	{
		if (ballAlreadyShot)
			return;

		// checks if touch is inside allowed space to receive the input
		if (Input.touchCount > 0 &&
		Input.GetTouch(0).position.y / screenHeight > screenUnit &&
		Input.GetTouch(0).position.y / screenHeight < (screenUnit*6))
		{
			// stores first input touch on screen
			Touch touch = Input.GetTouch(0);

			// touch ended, shoot ball
			if (touch.phase == TouchPhase.Ended || Input.touchCount > 1)
			{
				arrowDisplayed = false;
				Destroy(ArrowInstance);
				ShootBall();
			}
			// touching joystick
			else if (touch.phase == TouchPhase.Moved)
			{
				// stores coordinates for the input
				horizontalInput = JMovement.Horizontal;
				verticalInput = JMovement.Vertical;
				Direction = Vector3.right * horizontalInput + Vector3.up * verticalInput;

				AimArrow();

			}
		}
	}

}