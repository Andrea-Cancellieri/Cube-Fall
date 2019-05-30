using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStart : MonoBehaviour
{


	// public var
	public Animator FadAnim;
	public Image FadeImage;
	public GameObject lastHierarchyIndex;
	public GameObject Background;                               // ref to the background sprite
	public TextMeshProUGUI StartMsg;                            // ref to the starting text

	// private var
	GameObject Player;
	int firstHierarchyIndex;
	SpriteRenderer BackgroundColor;                             // ref to the background color
	float screenHeight;
	const float screenUnit = 0.125f;							// represents the normalized screen height unit (screenHeight/8)


	void Awake()
	{
		firstHierarchyIndex = FadeImage.transform.GetSiblingIndex();
		FadeImage.transform.SetSiblingIndex(lastHierarchyIndex.transform.GetSiblingIndex());
		FadAnim.SetBool("Fade", true);
	}



	IEnumerator WaitForFading()
	{
		yield return new WaitUntil(() => FadeImage.color.a == 0);
		FadeImage.transform.SetSiblingIndex(firstHierarchyIndex);
		lastHierarchyIndex.SetActive(false);
	}


	IEnumerator GameStartCoroutine()
	{
		// Start game after black fading effect
		yield return new WaitUntil(() => FadeImage.color.a == 0);
		FadeImage.transform.SetSiblingIndex(firstHierarchyIndex);
		lastHierarchyIndex.SetActive(false);

		// Waits to start game untile a touch is detected in allowed screen area
		yield return new WaitUntil(() => (Input.GetMouseButtonDown(0) ||
										 Input.touchCount > 0 &&
										 Input.GetTouch(0).position.y / screenHeight > screenUnit &&
										 Input.GetTouch(0).position.y / screenHeight < (screenUnit * 6) &&
										 Input.GetTouch(0).phase == TouchPhase.Ended));

		StartMsg.enabled = false;
		BackgroundColor.color = new Color32(255, 255, 255, 255);

		// starts time
		GetComponent<ScoreAndTime>().StartTime = true;

		// starts spawning ball
		Player.GetComponent<SpawnBall>().BallListener();
		
		// starts spawning cubes
		GetComponent<SpawnCube>().Invoke("Spawn", 2f);

		// start spawning boss cubes at a random time in range
		GetComponent<SpawnBoss>().Invoke("Spawn", Random.Range(20f, 30f));

		// starts powerUp spawn code
		GetComponent<SpawnPowerUp>().Invoke("Spawn", Random.Range(10f, 15f));

		// starts game audio
		GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioController>().StartGame();

		// activates every button in the scene
		foreach (GameObject button in GameObject.FindGameObjectsWithTag("PowerUpButton"))
		{
			button.GetComponent<Button>().interactable = true;
		}
	}


	// Start is called before the first frame update
	void Start()
	{
		Player = GameObject.FindGameObjectWithTag("Player");

		BackgroundColor = Background.GetComponent<SpriteRenderer>();
		BackgroundColor.color = new Color32(150, 150, 150, 255);
		StartMsg.text = "Tap here to\nstart the game!";

		screenHeight = Screen.height;

		StartCoroutine(GameStartCoroutine());
	}

}
