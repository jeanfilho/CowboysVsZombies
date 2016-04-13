using UnityEngine;
using System.Collections;

public class IntroSceneZombieActivator : MonoBehaviour {

    // This script is used to activate the zombies in the intro scene.

    public GameObject[] zombieList = new GameObject[0];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < zombieList.Length; i++)
        {
            zombieList[i].SetActive(true);
        }
    }
}
