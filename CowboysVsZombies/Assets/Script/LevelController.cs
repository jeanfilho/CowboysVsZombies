using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityStandardAssets.Characters.FirstPerson;

/*
 * This class is used for:
 * -Game Logic
 * -Create zombie spawners
 * -Count score points
 * -Difficulty level correlated to the heartbeat
 * 
 */
public class LevelController : MonoBehaviour
{
	
	[HideInInspector]
	public static float ScorePoints;
	[HideInInspector]
	public static bool isPaused;
	[HideInInspector]
	public static bool isReplay;
	[HideInInspector]
	public static bool isGame;
	[HideInInspector]
	public static string replaySession;

	public GameObject player;
	public GameObject stratCamera;
	public DataCollector dataCollector;
	public GameObject spawnPoint;
	public Canvas mainMenu;
	public Canvas replayMenu;
	public Canvas pauseMenu;
	public Dropdown sessions;

	public HeartMonitor heartMonitor;
	public GridMap grid;

	public float spawnSpawnerTime = 5;

	void OnStart ()
	{
		Cursor.visible = true;
	}

	void OnLevelWasLoaded (int level)
	{
		Cursor.visible = true;
	}

	public void startGame ()
	{
		isGame = true;
		dataCollector.createSampleFile ();
		spawnPlayer ();
	}

	public void selectReplay ()
	{
		mainMenu.enabled = false;
		replayMenu.enabled = true;
		sessions.ClearOptions ();
		sessions.AddOptions (new List<string> (Directory.GetFiles ("Data/")));
	}

	public void backToMainMenu (Canvas current)
	{
		current.enabled = false;
		mainMenu.enabled = true;
	}

	public void replayGame (Canvas current)
	{
		LevelController.replaySession = sessions.options [sessions.value].text;
		Debug.Log ("Current session: " + LevelController.replaySession);
		backToMainMenu (current);
		isReplay = true;
		dataCollector.loadSamples ();
		spawnPlayer ();
	}

	public void exit ()
	{
		Debug.Log ("Exit");
		Application.Quit ();
	}


	float spawnSpawnerTime_counter = 0;
	bool backToRTSCamera;
	void Update ()
	{
		//Pausing
		if (Input.GetKeyDown (KeyCode.Escape) && (isGame || isReplay))
		{
			if (!isPaused)
			{
				isPaused = true;
				Cursor.visible = true;
				GetComponent<Camera> ().enabled = true;
				mainMenu.enabled = false;
				pauseMenu.enabled = true;
				if (stratCamera.activeSelf)
				{
					stratCamera.SetActive (false);
					backToRTSCamera = true;

				} else
				{
					player.SetActive (false);
				}

			} else
			{
				isPaused = false;
				Cursor.visible = false;
				GetComponent<Camera> ().enabled = false;
				mainMenu.enabled = true;
				pauseMenu.enabled = false;
				if (backToRTSCamera)
				{
					stratCamera.SetActive (true);
					backToRTSCamera = false;

				} else
				{
					player.SetActive(true);
				}
			}
		} else if (Input.GetKeyDown (KeyCode.Tab) && !isPaused && isReplay)
		{
			if (stratCamera.activeSelf)
			{
				stratCamera.SetActive (false);
				player.SetActive (true);

			} else
			{
				stratCamera.SetActive (true);
				player.SetActive (false);
			}
		}

		//Difficulty
				
		//TODO: Finetuning of difficulty formulas, fitting to actual heartrate values
		if (isGame) {
			float hr = heartMonitor.getHeartRate ();

			//Add heartrate to grid to determine spawning place
			grid.addHeartRateInput (hr);

			//Spawnrate of Spawners
			spawnSpawnerTime_counter += Time.deltaTime;
			if (spawnSpawnerTime_counter >= spawnSpawnerTime) {
				grid.SpawnSpawner ();
				spawnSpawnerTime_counter = 0;
			}

			//ZombieSpawnrate
			GameData.Instance.setSpawnRateAdjustment(hr/60);

			//ZombieStrength
			GameData.Instance.setZombieDMGModifier(hr/60);
			GameData.Instance.setZombieHPModifier (hr/60);

			//Player Speed
			player.GetComponent<FirstPersonController>().m_WalkSpeed = 5 * hr/60;
			player.GetComponent<FirstPersonController>().m_RunSpeed = 10 * hr/60;

		}

	}

	void spawnPlayer ()
	{
		player.transform.position = spawnPoint.transform.position;
		player.transform.rotation = spawnPoint.transform.rotation;
		player.SetActive (true);
		isPaused = false;
		Cursor.visible = false;
		GetComponent<Camera> ().enabled = false;
	}

	public void resetGame ()
	{
		dataCollector.reset ();
		isPaused = false;
		isGame = false;
		isReplay = false;
		SceneManager.LoadScene ("Level01", LoadSceneMode.Single);
	}

}
