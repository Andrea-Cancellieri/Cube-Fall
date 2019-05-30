using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class AudioController : MonoBehaviour
{
	float fadeTime = 1f;
	AudioSource audioSource;

	public AudioClip[] ClipArray;

	public Sprite AudioOnSprite;
	public Sprite AudioOffSprite;

	[HideInInspector] public bool isAudioEnabled = true;


	public void AudioOnOff()
	{
		// turn off
		if (isAudioEnabled)
		{
			isAudioEnabled = false;
			audioSource.Stop();

			PlayerPrefs.SetInt("Audio", 0);
			PlayerPrefs.Save();

			GameObject.FindGameObjectWithTag("AudioButton").GetComponent<Image>().sprite = AudioOffSprite;
		}
		// turn on
		else
		{
			isAudioEnabled = true;
			audioSource.Play();

			PlayerPrefs.SetInt("Audio", 1);
			PlayerPrefs.Save();

			GameObject.FindGameObjectWithTag("AudioButton").GetComponent<Image>().sprite = AudioOnSprite;
		}
	}


	public void FadeAudioOut()
	{
		if (!isAudioEnabled)
			return;

		float startVolume = audioSource.volume;

		while (audioSource.volume > 0)
		{
			audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
		}

		audioSource.Stop();
		audioSource.volume = startVolume;
	}


	public void GameOverSoundEffect()
	{
		if (!isAudioEnabled)
			return;

		audioSource.clip = ClipArray[1];
		audioSource.loop = false;
		audioSource.Play();

		StartCoroutine(GameOverMusic());
	}

	IEnumerator GameOverMusic()
	{
		yield return new WaitForSecondsRealtime(2.5f);

		audioSource.clip = ClipArray[2];
		audioSource.loop = true;
		audioSource.Play();
	}


	public void StartGame()
	{
		if (!isAudioEnabled)
			return;

		audioSource.PlayDelayed(0.75f);
	}


	// Start is called before the first frame update
	void Start()
	{
		if (!PlayerPrefs.HasKey("Audio"))
		{
			PlayerPrefs.SetInt("Audio", 1);
			PlayerPrefs.Save();
		}

		isAudioEnabled = PlayerPrefs.GetInt("Audio") == 1 ? true : false;

		audioSource = GetComponent<AudioSource>();
		audioSource.clip = ClipArray[0];

		if (!isAudioEnabled)
		{
			GameObject.FindGameObjectWithTag("AudioButton").GetComponent<Image>().sprite = AudioOffSprite;
			return;
		}

		if (SceneManager.GetActiveScene().buildIndex == 0)
		{
			audioSource.Play();
		}

	}

}
