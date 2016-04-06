using UnityEngine;
using System.Collections;
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

	public GameObject player;
	public DataCollector dataCollector;
	public GameObject spawnPoint;


	void OnStart()
	{
		Cursor.visible = true;
	}

	public void startGame()
	{
		player.transform.position = spawnPoint.transform.position;
		player.transform.rotation = spawnPoint.transform.rotation;
		player.SetActive (true);
		isReplay = false;
		isPaused = false;
		Cursor.visible = false;
		GetComponent<Camera> ().enabled = false;
	}

	public void replayGame()
	{
		isReplay = true;
	}

	public void exit()
	{
		Debug.Log ("Exit");
		Application.Quit ();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if (!isPaused)
			{
				isPaused = true;
				Cursor.visible = true;
				GetComponent<Camera> ().enabled = true;
				player.SetActive (false);

			} else
			{
				isPaused = false;
				Cursor.visible = false;
				GetComponent<Camera> ().enabled = false;
				player.SetActive (true);
			}
		}
	}


}
