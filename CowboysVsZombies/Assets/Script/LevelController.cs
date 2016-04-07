using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
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
	public static bool isPaused = false;
	[HideInInspector]
	public static bool isReplay = false;
	[HideInInspector]
	public static bool isGame = false;
	[HideInInspector]
	public static string replaySession;

	public GameObject player;
	public GameObject stratCamera;
	public GameObject spawnPoint;

	public Canvas mainMenu;
	public Canvas replayMenu;
	public Canvas pauseMenu;

	public EventSystem eventSystem;

	public Text scoreText;

	public DataCollector dataCollector;

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
		GameData.Instance.setScore (0);
		dataCollector.createSampleFile ();
		eventSystem.SetSelectedGameObject (null);
		spawnPlayer ();
	}

	public void selectReplay ()
	{
		showMenu (replayMenu);
		sessions.ClearOptions ();
		if(!Directory.Exists("Data/"))
			Directory.CreateDirectory ("Data/");
		sessions.AddOptions (new List<string> (Directory.GetFiles ("Data/")));
	}

	public void replayGame (Canvas current)
	{
		LevelController.replaySession = sessions.options [sessions.value].text;
		Debug.Log ("Current session: " + LevelController.replaySession);
		showMenu (mainMenu);
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
		//Score UI update
		updateScore ();

		//Player Death
		if (GameData.Instance.getPlayerHealth () <= 0)
			playerDeath ();

		//Input Handling
		if (Input.GetKeyDown (KeyCode.Escape) && (isGame || isReplay))
			pauseToggle ();
		else if (Input.GetKeyDown (KeyCode.Tab) && !isPaused && isReplay)
			stratCameraToggle ();

		//Difficulty
		//TODO: Finetuning of difficulty formulas, fitting to actual heartrate values
		if (isGame) {
			float hr = heartMonitor.sampleHeartRate ();
				
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
		showMenu (mainMenu);
		SceneManager.LoadScene ("Level01", LoadSceneMode.Single);
	}

	void showMenu(Canvas menu)
	{
		mainMenu.enabled = false;
		replayMenu.enabled = false;
		pauseMenu.enabled = false;

		menu.enabled = true;
	}

	void playerDeath()
	{
		//Player Death
		showMenu (pauseMenu);
		isGame = false;
		GetComponent<Camera> ().enabled = true;
		player.SetActive (false);
	}

	void updateScore()
	{
		scoreText.text = "Score: " + GameData.Instance.getScore ();
	}

	void pauseToggle()
	{
		if (!isPaused)
		{
			isPaused = true;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.Confined;
			GetComponent<Camera> ().enabled = true;
			showMenu (pauseMenu);
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
			Cursor.lockState = CursorLockMode.Locked;
			GetComponent<Camera> ().enabled = false;
			showMenu (mainMenu);
			if (backToRTSCamera)
			{
				stratCamera.SetActive (true);
				backToRTSCamera = false;

			} else
			{
				player.SetActive(true);
			}
		}
	}

	void stratCameraToggle()
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
}
