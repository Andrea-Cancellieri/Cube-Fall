using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeCreation : MonoBehaviour
{
	public float frameDelay = 0.05f;

	public List<Sprite> SmokeFrames = new List<Sprite>();               // stores the set of images which create the animation

	int frameCount = 0;

	IEnumerator SmokeAnimation()
	{
		GetComponent<SpriteRenderer>().sprite = SmokeFrames[frameCount];

		yield return new WaitForSeconds(frameDelay);

		frameCount++;

		if (frameCount == SmokeFrames.Count)
			Destroy(gameObject);
		else
			StartCoroutine(SmokeAnimation());
	}

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(SmokeAnimation());
	}

}
