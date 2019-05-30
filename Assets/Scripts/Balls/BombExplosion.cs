using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
	public int Atk = 2;

	IEnumerator DestroyCoroutine()
	{
		yield return new WaitUntil(() => GetComponent<SpriteRenderer>().color.a == 0);
		Destroy(gameObject);
	}


	// Start is called before the first frame update
	void Start()
	{
		GetComponent<Animator>().SetBool("Explosion", true);
		StartCoroutine(DestroyCoroutine());
	}

}
