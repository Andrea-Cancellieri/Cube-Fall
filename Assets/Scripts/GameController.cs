using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine;


public class GameController : MonoBehaviour
{
	// reference to the player platform
	GameObject Player;

	[Header("Pause")]
	// reference to the pause button
	public Button PauseButton;

	// reference to pause panel animation
	public Animator pauseAnim;

	// reference to Pause panel
	public GameObject PauseMenu;

	// reference to game over panel animation
	[Header("Game Over")]
	public Animator gameOverAnim;

	// reference to gameOver panel
	public GameObject GameOverMenu;


	public IEnumerator WaitForMenuOn()
	{
		yield return new WaitForSeconds(0.75f);
		Time.timeScale = 0;
	}


	public IEnumerator WaitForMenuOff()
	{
		Time.timeScale = 1;
		yield return new WaitForSeconds(0.75f);

		PauseButton.enabled = true;
		PauseMenu.SetActive(false);
	}


	// pause panel functions
	public void PauseMenuOn()
	{
		PauseMenu.SetActive(true);
		PauseButton.enabled = false;
		pauseAnim.SetBool("PauseOn", true);

		StartCoroutine(WaitForMenuOn());
	}

	public void PauseMenuOff()
	{
		pauseAnim.SetBool("PauseOff", true);

		StartCoroutine(WaitForMenuOff());
	}

	// game over restart button
	public void RestartGame()
	{
		Time.timeScale = 1;

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}


	// game over quit button
	public void QuitGame()
	{
		Application.Quit();
	}


	// function called when the game is over, disable all the spawners, buttons and calls gameover animation
	public void EndGame()
	{
		Time.timeScale = 1;

		// play game over audio clip
		GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioController>().GameOverSoundEffect();

		// game over animation
		gameOverAnim.SetBool("GameOver", true);
		GameOverMenu.GetComponent<FinalScore>().SetScore();

		// disable spawners
		Player.GetComponent<SpawnBall>().enabled = false;
		GetComponent<SpawnCube>().enabled = false;
		GetComponent<SpawnBoss>().enabled = false;
		GetComponent<SpawnPowerUp>().enabled = false;

		// deactivates every button in the scene
		GameObject.FindGameObjectWithTag("PauseButton").GetComponent<Button>().interactable = false;

		GameObject.FindGameObjectWithTag("AudioButton").GetComponent<Button>().interactable = false;

		foreach (GameObject button in GameObject.FindGameObjectsWithTag("PowerUpButton"))
		{
			button.GetComponent<Button>().interactable = false;
		}

		StartCoroutine(GameOver());
	}


	IEnumerator GameOver()
	{
		yield return new WaitForSeconds(0.75f);

		// destroys normal cubes left
		foreach (GameObject Cube in GameObject.FindGameObjectsWithTag("Cube"))
		{
			Destroy(Cube);
		}

		Time.timeScale = 0f;
	}


	// Start is called before the first frame update
	void Start()
	{
		//Time.timeScale = 12f;

		Player = GameObject.FindGameObjectWithTag("Player");
	}

}
