using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatCube : CubeController
{
	// CubeController.Start is called


	// instancitates smoke creation prefab
	protected override void SmokeDestruction()
	{
		GameObject Smoke = Instantiate(SmokeDestructionFX, transform.position, Quaternion.Euler(1f, 1f, Random.Range(1f, 360f)));

		Smoke.transform.localScale = new Vector3(0.75f * GetComponent<BoxCollider2D>().size.x,
												 0.75f * GetComponent<BoxCollider2D>().size.y,
												 1);
	}
}
