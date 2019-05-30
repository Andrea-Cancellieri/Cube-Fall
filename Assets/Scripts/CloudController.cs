using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{

	float cloudScale;
	float randomVelocity;



	void SetScaleAndVelocity()
	{
		// set random scale in range
		cloudScale = Random.Range(0.5f, 1f);
		gameObject.transform.localScale = new Vector3(cloudScale, cloudScale, 0f);

		// set random x-axys velocity in range
		randomVelocity = Random.Range(0.05f, 0.10f);
	}

	// Start is called before the first frame update
	void Start()
	{
		SetScaleAndVelocity();
	}

	// Update is called once per frame
	void Update()
	{
		// move right
		transform.position += new Vector3(randomVelocity, 0f, 0f);

		// if cloud out of range, set to initial position, new scale and velocity
		if (transform.position.x >= 20f)
		{
			transform.position = new Vector3(-20f, transform.position.y, 0f);
			SetScaleAndVelocity();
		}
	}

}
