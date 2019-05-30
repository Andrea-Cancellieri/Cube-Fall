using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MenuController : MonoBehaviour
{

	[Header("Cloud prefabs")]
	public GameObject Cloud1;
	public GameObject Cloud2;

	[Header(" ")]
	public Animator fade_anim;
	public Image fade_image;

	public GameObject last_hierarchy_index;

	private string chosen_power_up;


	// Instantiate instances of cloud sprites at random positions
	void CloudGenerator(int begin, int end)
	{
		for (int i = begin; i != end;)
		{
			// random sprite generator
			int randomInt = 0;
			float randomFloat = Random.value;
			if (randomFloat < 0.5f)
				randomInt = 0;
			else
				randomInt = 1;

			// random position and gameobject generator
			float RandomPosX = Random.Range(-20f, 20f);
			GameObject CloudInstance;
			switch (randomInt)
			{
				case 0:
					CloudInstance = Instantiate(Cloud1, new Vector3(RandomPosX, i, 0f), Quaternion.identity);
					break;
				case 1:
					CloudInstance = Instantiate(Cloud2, new Vector3(RandomPosX, i, 0f), Quaternion.identity);
					break;
				default:
					break;
			}

			// cycle through the height
			if (end > 0)
				i += 5;
			else
				i -= 5;
		}
	}


	public void StartPlay()
	{
		GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioController>().FadeAudioOut();
		StartCoroutine(FadeAndLoad());
	}

	IEnumerator FadeAndLoad ()
	{
		fade_image.transform.SetSiblingIndex(last_hierarchy_index.transform.GetSiblingIndex());
		fade_anim.SetBool("Fade", true);

		yield return new WaitUntil(() => fade_image.color.a == 1);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

	}


	public void QuitGame ()
	{
		Application.Quit();
	}


	void Start()
	{
		CloudGenerator(0, 20);
		CloudGenerator(-5, -20);

		// spawn normal cubes
		GetComponent<MenuSpawnCube>().Invoke("Spawn", 1f);
	}

}
