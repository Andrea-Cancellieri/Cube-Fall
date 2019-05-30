using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriCube : CubeController
{
	[Header("Small Tri-Balls")]
	public GameObject Small1;
	public GameObject Small2;
	public GameObject Small3;

	[Header(" ")]
	public Sprite[] ColorArray;

	int count = 0;


	// changes color every given amount of time
	IEnumerator ChangeColor()
	{
		GetComponent<SpriteRenderer>().sprite = ColorArray[count];

		if (count == ColorArray.Length - 1)
			count = 0;
		else
			count++;

		yield return new WaitForSeconds(0.33f);

		StartCoroutine(ChangeColor());
	}


	// instancitates smoke creation prefab
	protected override void SmokeDestruction()
	{
		GameObject Smoke = Instantiate(SmokeDestructionFX, transform.position, Quaternion.Euler(1f, 1f, Random.Range(1f, 360f)));

		Smoke.transform.localScale = new Vector3(0.75f * GetComponent<BoxCollider2D>().size.x,
												 0.75f * GetComponent<BoxCollider2D>().size.y,
												 1);
	}


	// spawns three normal balls and destroy the object
	IEnumerator Trivide()
	{
		float positionY = Random.Range(-10f, 20f);
		yield return new WaitUntil(() => transform.position.y <= positionY);

		// instanciate three balls
		GameObject First = Instantiate(Small1, transform.position, Quaternion.identity);
		First.GetComponent<Rigidbody2D>().velocity = Rb.velocity;
		First.GetComponent<Rigidbody2D>().gravityScale = Rb.gravityScale;

		GameObject Second = Instantiate(Small2, transform.position, Quaternion.identity);
		Second.GetComponent<Rigidbody2D>().velocity = Rb.velocity;
		Second.GetComponent<Rigidbody2D>().gravityScale = Rb.gravityScale;

		GameObject Third = Instantiate(Small3, transform.position, Quaternion.identity);
		Third.GetComponent<Rigidbody2D>().velocity = Rb.velocity;
		Third.GetComponent<Rigidbody2D>().gravityScale = Rb.gravityScale;

		SmokeDestruction();
		Destroy(gameObject);
	}


	protected override void Start()
	{
		Manager = GameObject.FindGameObjectWithTag("GameManager");

		StartRoutine();
		StartCoroutine(ChangeColor());
		StartCoroutine(Trivide());
	}
}
