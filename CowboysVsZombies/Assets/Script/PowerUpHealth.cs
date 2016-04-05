using UnityEngine;
using System.Collections;

public class PowerUpHealth : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().changePlayerHealth(40);
            Destroy(this.gameObject);
        }
    }
}
