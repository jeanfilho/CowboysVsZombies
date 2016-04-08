using UnityEngine;
using System.Collections;

public class GridMap : MonoBehaviour {
	public GameObject spawner;
	private bool [,] grid;
	private float [,] avg_hr_grid;
	private int[,] avg_hr_grid_count;
	private int spawners;
	public GameObject player;
	// Use this for initialization
	void Start () {
		grid = new bool[40,40];
		avg_hr_grid = new float[40,40];
		avg_hr_grid_count = new int[40,40];
		for (int i = 0; i < avg_hr_grid.GetLength(0); i++) {
			for (int j = 0; j < avg_hr_grid.GetLength (1); j++) {
				avg_hr_grid[i,j] = 360;
				avg_hr_grid_count [i,j] = 0;
			}
		}
		initializeMap ();
		SpawnSpawner ();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.H)){
			SpawnAtRandom();
		}
	}

	public void SpawnAtRandom(){
		int posX = Random.Range (1, 39);
		int posY = Random.Range (1, 39);
		if (!grid [posY,posX] && !grid[posY,posX-1] && !grid[posY,posX+1] && !grid[posY-1,posX] && !grid[posY+1,posX]) {
			GameObject spawnInst = (GameObject)Instantiate (spawner, new Vector3 ((posX-20) * 2 + 1, 0, (posY-20) * 2 + 1), Quaternion.AngleAxis(Random.Range(0,360),new Vector3(0,1,0)));
			spawnInst.GetComponent<ZombieSpawner> ().initialize (player);
			grid [posY,posX] = true;
			grid [posY,posX-1] = true;
			grid [posY,posX+1] = true;
			grid [posY-1,posX] = true;
			grid [posY-1,posX+1] = true;
			grid [posY-1,posX-1] = true;
			grid [posY+1,posX] = true;
			grid [posY+1,posX+1] = true;
			grid [posY+1,posX-1] = true;
		}
	}

	int lastSpawnX;
	int lastSpawnY;

	public void SpawnSpawner(){
		// find lowest point
		int lowestX = 20;
		int lowestY = 20;
		float lowest = 360;
		for (int i = 1; i < avg_hr_grid.GetLength(0)-1; i++) {
			for (int j = 1; j < avg_hr_grid.GetLength (1)-1; j++) {
				if (avg_hr_grid [i, j] < lowest && !grid[i,j] ) {
					lowest = avg_hr_grid [i, j];
					lowestX = i;
					lowestY = j;
				}
			}
		}

		Debug.Log (lowest);

		if ((lastSpawnX == lowestX) && (lastSpawnY == lowestY)) {
			SpawnAtRandom ();
			Debug.Log ("Spawned Randomly");
		}
		else{
			if (!grid [lowestY,lowestX] && !grid[lowestY,lowestX-1] && !grid[lowestY,lowestX+1] && !grid[lowestY-1,lowestX] && !grid[lowestY+1,lowestX]) {
				// replace new Vector3(...) with NavMeshHit hit; NavMesh.SamplePosition(Vector3 ((lowestX-20) * 2 + 1, 0, (lowestY-20) * 2 + 1), hit, 4,NavMesh.AllAreas)

				GameObject spawnInst = (GameObject)Instantiate (spawner, new Vector3 ((lowestY-20) * 2 + 1, 0, (lowestX-20) * 2 + 1), Quaternion.AngleAxis(Random.Range(0,360),new Vector3(0,1,0)));
				spawnInst.GetComponent<ZombieSpawner> ().initialize (player);

				lastSpawnX = lowestX;
				lastSpawnY = lowestY;
				Debug.Log ("Spawned at lowest location");
				Debug.Log ("Spawned spawner at " + lowestX + " " + lowestY);
				grid [lowestX,lowestY] = true;
				grid [lowestY,lowestX-1] = true;
				grid [lowestY,lowestX+1] = true;
				grid [lowestY-1,lowestX] = true;
				grid [lowestY-1,lowestX+1] = true;
				grid [lowestY-1,lowestX-1] = true;
				grid [lowestY+1,lowestX] = true;
				grid [lowestY+1,lowestX+1] = true;
				grid [lowestY+1,lowestX-1] = true;
			}
		}

	}

	public void addHeartRateInput(float hr){
		int x = Mathf.Abs (Mathf.FloorToInt ((player.transform.position.z + 40) / 2));
		int y = Mathf.Abs(Mathf.FloorToInt((player.transform.position.x + 40) / 2));
		if (x > 39)
			x = 39;
		if (y > 39)
			y = 39;
		
		avg_hr_grid_count [x, y]++;

		avg_hr_grid [x, y] = ((avg_hr_grid [x, y] * (avg_hr_grid_count [x, y] - 1)) + hr) / avg_hr_grid_count [x, y];

	}

	void initializeMap(){
		grid [0, 19] = true;
		grid [0, 21] = true;
		grid [0, 22] = true;
		grid [0, 23] = true;
		grid [0, 24] = true;
		grid [0, 25] = true;
		grid [0, 26] = true;

		grid [1, 18] = true;
		grid [1, 19] = true;
		grid [1, 20] = true;

		grid [2, 18] = true;
		grid [2, 19] = true;
		grid [2, 20] = true;

		grid [3, 18] = true;
		grid [3, 19] = true;
		grid [3, 20] = true;

		grid [4, 18] = true;
		grid [4, 19] = true;

		grid [5, 18] = true;
		grid [5, 19] = true;

		grid [7, 3] = true;

		grid [8, 1] = true;
		grid [8, 2] = true;
		grid [8, 3] = true;
		grid [8, 6] = true;
		grid [8, 7] = true;

		grid [9, 2] = true;
		grid [9, 3] = true;
		grid [9, 6] = true;
		grid [9, 7] = true;

		grid [10, 3] = true;
		grid [10, 4] = true;
		grid [10, 6] = true;
		grid [10, 7] = true;

		grid [11, 3] = true;

		grid [9, 32] = true;

		grid [13, 30] = true;
		grid [14, 30] = true;
		grid [13, 29] = true;
		grid [14, 29] = true;

		for (int i = 0; i <= 17; i++) {
			grid [15, i] = true;
		}

		grid [17, 3] = true;
		grid [18, 1] = true;
		grid [18, 2] = true;
		grid [18, 3] = true;
		grid [19, 1] = true;
		grid [19, 2] = true;

		grid [21, 13] = true;
		grid [32, 5] = true;

		grid [35, 10] = true;
		grid [35, 11] = true;
		grid [36, 10] = true;
		grid [36, 11] = true;

		grid [28, 18] = true;
		grid [29, 18] = true;
		grid [30, 18] = true;
		grid [31, 18] = true;
		grid [32, 18] = true;
		grid [28, 19] = true;
		grid [29, 19] = true;
		grid [30, 19] = true;
		grid [31, 19] = true;
		grid [32, 19] = true;
		grid [28, 20] = true;
		grid [29, 20] = true;
		grid [30, 20] = true;
		grid [31, 20] = true;
		grid [32, 20] = true;
		grid [28, 21] = true;
		grid [29, 21] = true;
		grid [30, 21] = true;
		grid [31, 21] = true;
		grid [32, 21] = true;
		grid [28, 22] = true;
		grid [29, 22] = true;
		grid [30, 22] = true;
		grid [31, 22] = true;
		grid [32, 22] = true;

		grid [25, 21] = true;
		grid [25, 22] = true;
		grid [26, 21] = true;

		grid [29, 27] = true;
		grid [28, 27] = true;

		for (int i = 23; i < 40; i++) {
			grid [28, i] = true;
		}

		grid [30, 33] = true;
		grid [31, 33] = true;
		grid [31, 32] = true;
		grid [32, 33] = true;
		grid [32, 32] = true;
		grid [33, 32] = true;
		grid [33, 33] = true;
		grid [33, 34] = true;
		grid [33, 35] = true;
		grid [34, 34] = true;
		grid [34, 33] = true;

		grid [25, 35] = true;
		grid [26, 35] = true;

		grid [24, 35] = true;
		grid [24, 36] = true;
		grid [24, 37] = true;
		grid [25, 36] = true;

		grid [20, 38] = true;
		grid [20, 39] = true;
		grid [21, 38] = true;
		grid [21, 39] = true;
		grid [22, 38] = true;
		grid [22, 39] = true;
	}

}
