using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class FinalScore : MonoBehaviour
{

	// stores the score value based on gameplay
	int ScoreValue = 0;

	// stores the score value based on survived time
	int TimeSurvivedValue = 0;
	
	// stores the total score value
	int TotalPointsValue = 0;


	// score texts
	public TextMeshProUGUI GameScore;
	public TextMeshProUGUI TimeScore;
	public TextMeshProUGUI TotalScore;
	public TextMeshProUGUI BestHighScore;

	// [new] best high score text
	public TextMeshProUGUI NewBestHighScore;


	IEnumerator BlinkText()
	{
		yield return new WaitForSecondsRealtime(0.75f);

		NewBestHighScore.enabled = false;

		yield return new WaitForSecondsRealtime(0.75f);

		NewBestHighScore.enabled = true;

		StartCoroutine(BlinkText());
	}

	public void SetScore()
	{
		// game score
		ScoreValue = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreAndTime>().score;
		GameScore.SetText(ScoreValue.ToString());

		// time score
		TimeSurvivedValue = Mathf.RoundToInt(GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreAndTime>().timer) * 20;
		TimeScore.SetText(TimeSurvivedValue.ToString());

		// total score
		TotalPointsValue = ScoreValue + TimeSurvivedValue;
		TotalScore.SetText(TotalPointsValue.ToString());

		// best high score
		if (TotalPointsValue > PlayerPrefs.GetFloat("Best Score"))
		{
			NewBestHighScore.SetText("New Best High Score!");

			PlayerPrefs.SetFloat("Best Score", TotalPointsValue);
			PlayerPrefs.Save();

			StartCoroutine(BlinkText());
		}
		BestHighScore.SetText(PlayerPrefs.GetFloat("Best Score").ToString());
	}

	// Start is called before the first frame update
	void Start()
	{
		if (!PlayerPrefs.HasKey("Best Score"))
		{
			PlayerPrefs.SetFloat("Best Score", 0f);
			PlayerPrefs.Save();
		}
	}

}
