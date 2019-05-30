using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBall : NormalBall
{
	public GameObject BombExplosion;


	protected override void OnCollisionEnter2D(Collision2D collision)
	{
		gameObject.layer = PHANTOM;
		Instantiate(BombExplosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}


	// NormalBall.Start is called


	// NormalBall.Update is called

}
