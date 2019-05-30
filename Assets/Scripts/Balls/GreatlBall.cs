using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatlBall : NormalBall
{

	// instancitates smoke creation prefab
	protected override void SmokeCreation()
	{
		GameObject Smoke = Instantiate(SmokeCreationFX, transform.position, Quaternion.identity);

		Smoke.transform.localScale = new Vector3(1f * GetComponent<CircleCollider2D>().radius,
												 1f * GetComponent<CircleCollider2D>().radius,
												 1);
	}


	// instancitates smoke creation prefab
	protected override void SmokeDestruction()
	{
		GameObject Smoke = Instantiate(SmokeDestructionFX, transform.position, Quaternion.Euler(1f, 1f, Random.Range(1f, 360f)));

		Smoke.transform.localScale = new Vector3(1f * GetComponent<CircleCollider2D>().radius,
												 1f * GetComponent<CircleCollider2D>().radius,
												 1);
	}


	// Start is called before the first frame update
	protected override void Start()
	{
		StartRoutine();
		SmokeCreation();
	}

	// Update is called once per frame
	protected override void Update()
	{
		UpdateRoutine();
		transform.Rotate(0f, 0f, 2.5f);
	}
}
