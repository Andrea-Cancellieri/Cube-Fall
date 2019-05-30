using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriBall : NormalBall
{
	[Header("Tri-Ball Stats:")]
	public int ballNumber;										// Variable to distinguish between the three balls in scene
	public float shootingAngle;									// shooting angle differential between the three balls
	
	Vector3 TriBallDirection;
	float rotationSpeed = 3f;


	// NormalBall.Start is called


	protected override void ShootBall()
	{
		ballAlreadyShot = true;
		BallRb.simulated = true;
		BallColl.enabled = true;

		TriBallDirection = Quaternion.AngleAxis(shootingAngle, Vector3.forward) * Direction;

		BallRb.AddForce(TriBallDirection * shotStrenght, ForceMode2D.Impulse);

		// If it's the first of the three balls, calls manager function to spawn another ball
		if (ballNumber == 1)
		{
			Player.GetComponent<SpawnBall>().BallListener();

			// decrease ball ammo code
			foreach (GameObject button in GameObject.FindGameObjectsWithTag("PowerUpButton"))
			{
				if (button.GetComponent<PowerUpButton>().BallToSwitch.CompareTag(gameObject.tag))
				{
					button.GetComponent<PowerUpButton>().DecreaseAmmo();					// calls function to decrease button.currentAmmo
				}
			}
		}

		// destroys remaining aiming arrow (Bug Solver)
		foreach (GameObject ArrowInstance in GameObject.FindGameObjectsWithTag("Arrow"))
			Destroy(ArrowInstance);

		DecreaseScore();
		Destroy(gameObject, 3f);

	}


	// Arrow code
	protected override void AimArrow()
	{
		// only the first ball can render the arrow
		if (ballNumber != 1)
			return;

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

	// Update is called once per frame
	protected override void Update()
	{
		UpdateRoutine();
	}

	protected override void UpdateRoutine()
	{
		if (ballAlreadyShot)
			return;

		transform.RotateAround(SpawnPoint.transform.position, Vector3.forward, rotationSpeed);

		// checks if touch is inside allowed space to receive the input
		if (Input.touchCount > 0 &&
		Input.GetTouch(0).position.y / screenHeight > screenUnit &&
		Input.GetTouch(0).position.y / screenHeight < (screenUnit * 6))
		{
			// stores first input touch on screen
			Touch touch = Input.GetTouch(0);

			// touch ended, shoot ball
			if (touch.phase == TouchPhase.Ended)
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
