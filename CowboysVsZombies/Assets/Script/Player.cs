using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * This class holds player related variables and actions:
 * -HP
 * -Movement
 * -Weapon Use
 * 
 */

public class Player : MonoBehaviour {

    public Text healthText;

	// Use this for initialization
	void Start () {

        // UI:
        healthText.text = "Health: " + GameData.Instance.getPlayerHealth();
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    // #################### UI ####################
    public void changePlayerHealth(int value)
    {
        GameData.Instance.changePlayerHealth(value);
        healthText.text = "Health: " + GameData.Instance.getPlayerHealth();
    }

}
