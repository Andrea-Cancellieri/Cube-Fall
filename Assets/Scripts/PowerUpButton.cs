using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpButton : MonoBehaviour
{
	// Public Var
	public GameObject BallToSwitch;
	public int currentAmmo = 3;
	public GameObject BallImage;
	public GameObject AmmoImage;
	public bool isEmptyAmmo = false;                                    // tells if the button is interactable or not (empy ammo)
	public bool isSelected = false;										// tells which button is the selected one (with the ball active)
	public List<Sprite> ImageCountList = new List<Sprite>(10);

	// Private Var
	GameObject Player;
	GameObject CurrentBall;

	// color attributes of the button that change during game
	Color32 ButtonColorDefault = new Color32(255, 192, 0, 255);
	Color32 ButtonColorActive = new Color32(255, 25, 0, 255);
	Color32 ButtonColorFaded = new Color32(255, 192, 0, 200);
	Color32 ColorDefault = new Color32(255, 255, 255, 255);
	Color32 ColorFaded = new Color32(150, 150, 150, 200);

	// decrease ammo code
	public void DecreaseAmmo()
	{
		// don't decrease ammo if it's NormalBall
		if (BallToSwitch.CompareTag("Ball"))
			return;

		// decreases Ammo and change Ammo image
		currentAmmo -= 1;
		AmmoImage.GetComponent<Image>().sprite = ImageCountList[currentAmmo];

		// checks ammo count
		if (currentAmmo <= 0)
		{
			// disable button, fade color and change currentball
			isEmptyAmmo = true;
			GetComponent<Button>().interactable = false;

			GetComponent<Image>().color = ButtonColorFaded;
			BallImage.GetComponent<Image>().color = ColorFaded;
			AmmoImage.GetComponent<Image>().color = ColorFaded;

			Player.GetComponent<SpawnBall>().SwitchCurrentBall();

		}

	}


	// increase ammo code
	public void IncreaseAmmo()
	{
		// if ammo was empy, reactivates button
		if (currentAmmo <= 0)
		{
			isEmptyAmmo = false;

			GetComponent<Button>().interactable = true;

			GetComponent<Image>().color = ButtonColorDefault;
			BallImage.GetComponent<Image>().color = ColorDefault;
			AmmoImage.GetComponent<Image>().color = ColorDefault;
		}

		currentAmmo += 3;

		// caps to max ammo
		if(currentAmmo > 9)
			currentAmmo = 9;

		AmmoImage.GetComponent<Image>().sprite = ImageCountList[currentAmmo];

	}


	// function activated when button pressed
	public void ChangeBall()
	{
		CurrentBall = Player.GetComponent<SpawnBall>().CurrentBall;

		// if the ball in already this type, do nothing
		if (CurrentBall.CompareTag(BallToSwitch.tag))
			return;

		// deactivates every button in the scene until ball is spawned
		foreach (GameObject button in GameObject.FindGameObjectsWithTag("PowerUpButton"))
		{
			button.GetComponent<Button>().interactable = false;
			// deactivates the prior active button
			if(button.GetComponent<PowerUpButton>().isSelected == true)
				button.GetComponent<PowerUpButton>().DeselectButton();
		}

		// activates and changes button color
		isSelected = true;
		GetComponent<Image>().color = ButtonColorActive;

		// AGGIUNGI QUA IL CHECK DEL BOTTONE PRECEDENTEMENTE ATTIVO E LA SUA CHIAMATA A FUNZIONE PER CAMBIARE COLORE A DEFAULT (ANCH'ESSA DA FARE)

		// calls function to change ball in game
		Player.GetComponent<SpawnBall>().SwitchCurrentBall(BallToSwitch);

	}


	public void DeselectButton()
	{
		isSelected = false;
		GetComponent<Image>().color = ButtonColorDefault;
	}


	// Start is called before the first frame update
	void Start()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
		GetComponent<Button>().interactable = false;

		// if it's NormaBall button
		if (BallToSwitch.CompareTag("Ball"))
		{
			isSelected = true;
			GetComponent<Image>().color = ButtonColorActive;
			currentAmmo = int.MaxValue;
		}
			
	}

}
