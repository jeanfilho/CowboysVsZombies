using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;
/*
 * This class is used for:
 * -Game Logic
 * -Create zombie spawners
 * -Count score points
 * -Difficulty level correlated to the heartbeat
 * 
 */
public class LevelController : MonoBehaviour {
	
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
	public DataCollector dataCollector;
	public GameObject spawnPoint;
	public Canvas mainMenu;
	public Canvas replayMenu;
	public Canvas pauseMenu;
	public Dropdown sessions;

	void OnStart()
	{
		Cursor.visible = true;
	}

	void OnLevelWasLoaded(int level)
	{
		Cursor.visible = true;
	}

	public void startGame()
	{
		isGame = true;
		dataCollector.createSampleFile ();
		spawnPlayer ();
	}

	public void selectReplay()
	{
		mainMenu.enabled = false;
		replayMenu.enabled = true;
		sessions.ClearOptions();
		sessions.AddOptions (new List<string> (Directory.GetFiles ("Data/")));
	}

	public void backToMainMenu(Canvas current)
	{
		current.enabled = false;
		mainMenu.enabled = true;
	}

	public void replayGame(Canvas current)
	{
		LevelController.replaySession = sessions.options [sessions.value].text;
		Debug.Log ("Current session: " + LevelController.replaySession);
		backToMainMenu (current);
		isReplay = true;
		dataCollector.loadSamples ();
		spawnPlayer ();
	}

	public void exit()
	{
		Debug.Log ("Exit");
		Application.Quit ();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape) && (isGame || isReplay))
		{
			if (!isPaused)
			{
				isPaused = true;
				Cursor.visible = true;
				GetComponent<Camera> ().enabled = true;
				mainMenu.enabled = false;
				pauseMenu.enabled = true;
				player.SetActive (false);

			} else
			{
				isPaused = false;
				Cursor.visible = false;
				GetComponent<Camera> ().enabled = false;
				mainMenu.enabled = true;
				pauseMenu.enabled = false;
				player.SetActive (true);
			}
		}
	}

	void spawnPlayer()
	{
		player.transform.position = spawnPoint.transform.position;
		player.transform.rotation = spawnPoint.transform.rotation;
		player.SetActive (true);
		isPaused = false;
		Cursor.visible = false;
		GetComponent<Camera> ().enabled = false;
	}

	public void resetGame()
	{
		dataCollector.reset ();
		isPaused = false;
		isGame = false;
		isReplay = false;
		SceneManager.LoadScene ("Level01", LoadSceneMode.Single);
	}

}
