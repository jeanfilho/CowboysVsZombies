using UnityEngine;
using System.Collections;

/*
 * This class is used for:
 * -Spawn zombies
 * 
 */
public class ZombieSpawner : MonoBehaviour {

    private bool active = false;

    public GameObject zombie;

    public GameObject playerCharacter;

    public float spawnRate = 1.0f;
    private float spawnRateCounter = 100f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.U))
        {
            active = true;
        }

        if (active)
        {
            spawnRateCounter = spawnRateCounter + Time.deltaTime;
			if (spawnRateCounter >= 10.0f * spawnRate * GameData.Instance.getSpawnRateAdjustment())
            {
                spawnZombie();
                spawnRateCounter = 0;
            }
        }
	}

    public void spawnZombie()
    {
        int randomSize = Random.Range(0, 10);
        float randomScale = 4 + 0.2f * randomSize;

        GameObject zombieSpawn = Instantiate(zombie, this.transform.position, this.transform.rotation) as GameObject;
        zombieSpawn.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        zombieSpawn.GetComponent<SimpleNavMeshAgent>().initialize(playerCharacter);
		zombieSpawn.GetComponent<Zombie> ().setHealth (Mathf.FloorToInt(100 * GameData.Instance.getZombieHPModifier ()));
    }

    public void initialize(GameObject player)
    {
        playerCharacter = player;
        active = true;
    }
}
