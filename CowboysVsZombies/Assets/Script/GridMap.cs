using UnityEngine;
using System.Collections;

public class GridMap : MonoBehaviour {
	public GameObject spawner;
	private bool [,] grid;
	private float [,] avg_hr_grid;
	private int spawners;
	public GameObject player;
	// Use this for initialization
	void Start () {
		grid = new bool[40,40];
		avg_hr_grid = new float[40,40];

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.H)){
			SpawnAtRandom();
		}
	}

	void SpawnAtRandom(){
		int posX = Random.Range (0, 40);
		int posY = Random.Range (0, 40);
		if (!grid [posX,posY]) {
			GameObject spawnInst = (GameObject)Instantiate (spawner, new Vector3 ((posX-20) * 2 + 1, 0, (posY-20) * 2 + 1), Quaternion.identity);
            spawnInst.GetComponent<ZombieSpawner>().initialize(player);
			grid [posX,posY] = true;
		}
	}

	void SpawnSpawner(){
		// find lowest point
		int lowestX = 0;
		int lowestY = 0;
		float lowest = 0;
		for (int i = 0; i < avg_hr_grid.GetLength(0); i++) {
			for (int j = 0; j < avg_hr_grid.GetLength (1); j++) {
				if (avg_hr_grid [i, j] <= lowest) {
					lowest = avg_hr_grid [i, j];
					lowestX = i;
					lowestY = j;
				}
			}
		}

		if (!grid [lowestX,lowestY]) {
			// replace new Vector3(...) with NavMeshHit hit; NavMesh.SamplePosition(Vector3 ((lowestX-20) * 2 + 1, 0, (lowestY-20) * 2 + 1), hit, 4,NavMesh.AllAreas)
			GameObject spawnInst = (GameObject)Instantiate (spawner, new Vector3 ((lowestX-20) * 2 + 1, 0, (lowestY-20) * 2 + 1), Quaternion.identity);
			spawnInst.GetComponent<ZombieSpawner> ().initialize (player);
			grid [lowestX,lowestY] = true;
		}



	}
}
