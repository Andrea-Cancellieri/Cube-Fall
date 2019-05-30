using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallTriCube : CubeController
{
	[Header(" ")]
	public float fallingAngle;                                 // falling angle differential between the three cubes
	[HideInInspector] public Vector2 velocity;

	Vector3 TriCubeDirection;


	// Start is called before the first frame update
	protected override void Start()
	{
		Rigidbody2D Rb = GetComponent<Rigidbody2D>();
		Manager = GameObject.FindGameObjectWithTag("GameManager");
		HealtBar = GameObject.FindGameObjectWithTag("HealthBar");

		fallingAngle += Random.Range(-1f, 1f);
		Rb.AddForce(new Vector3(fallingAngle, 0f, 0f), ForceMode2D.Impulse);

		// randomize the torque to the cube 
		if (Random.value <= 0.5)
			randomTorque = Random.Range(minTorque, maxTorque);
		else
			randomTorque = -Random.Range(minTorque, maxTorque);

		// set torque to cube
		Rb.AddTorque(randomTorque, ForceMode2D.Impulse);
	}

}
