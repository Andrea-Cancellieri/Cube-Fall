using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class HealthController : MonoBehaviour
{
	// public var
	public int maxHealth;
	[HideInInspector] public int health;

	public int LIfePowerUpValue;


	// private var
	GameObject Manager;

	RectTransform HealthTransform;

	float maxWidth;
	float width;

	Color32 ColorDefault = new Color32(0, 255, 255, 255);
	Color32 ColorDamage = new Color32(0, 100, 255, 255);
	Color32 ColorLife = new Color32(0, 255, 0, 255);


	// resizes bar based on health
	IEnumerator ChangeColor(Color32 newColor)
	{
		HealthTransform.sizeDelta = new Vector2(width, HealthTransform.sizeDelta.y);

		GetComponent<Image>().color = newColor;

		yield return new WaitForSeconds(0.4f);

		GetComponent<Image>().color = ColorDefault;
	}


	public void DecreaseHealth(GameObject cube)
	{
		// decreases health
		int damageTaken = cube.GetComponent<CubeController>().damage;
		health -= damageTaken;
		
		// decreases bar width
		width = (maxWidth / maxHealth) * health;
		StartCoroutine(ChangeColor(ColorDamage));

		// checks health
		if (health <= 0)
		{
			health = 0;
			Manager.GetComponent<GameController>().EndGame();
		}
	}


	public void IncreaseHealth()
	{
		// increases health
		health += LIfePowerUpValue;

		// checks health
		if (health > maxHealth)
		{
			health = maxHealth;
		}

		// increases bar width
		width = (maxWidth / maxHealth) * health;
		StartCoroutine(ChangeColor(ColorLife));

	}


	// Start is called before the first frame update
	void Start()
	{
		health = maxHealth;

		Manager = GameObject.FindGameObjectWithTag("GameManager");

		HealthTransform = GetComponent<RectTransform>();

		maxWidth = HealthTransform.sizeDelta.x;
		width = maxWidth;
	}

}
