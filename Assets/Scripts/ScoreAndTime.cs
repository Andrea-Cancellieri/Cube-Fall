using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ScoreAndTime : MonoBehaviour
{
	// store the references of the score and time text
	public TextMeshProUGUI ScoreText;
	public TextMeshProUGUI TimeText;

	// stores the score value
	[HideInInspector] public int score = 0;

	// used for time delay at startup
	[HideInInspector] public bool StartTime = false;

	// variables for displaying time
	[HideInInspector] public float timer = 0f;
	int minutes;
	int seconds;
	string TimeString;


	public void UpdateScore(int points)
	{
		score += points;
		ScoreText.SetText(score.ToString());
	}


	// Update is called once per frame
	void Update()
	{
		if (StartTime)
		{
			timer += Time.deltaTime;
			minutes = Mathf.FloorToInt(timer / 60f);
			seconds = Mathf.FloorToInt(timer - minutes * 60);
			TimeString = string.Format("{00:00}:{1:00}", minutes, seconds);

			TimeText.SetText(TimeString);
		}
	}
}
