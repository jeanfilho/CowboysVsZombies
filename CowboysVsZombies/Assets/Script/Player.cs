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

	public GameObject Shotgun;
	public GameObject Revolver;
	public GameObject Rifle;
	public GameObject WeaponSpawn;
	private string weaponSelected;
	public GameObject actualWeapon;


    // UI:
    public Text healthText;

	// Use this for initialization
	void Start () 
	{
		weaponSelected="Shotgun";
		actualWeapon= (GameObject)Instantiate(getactualWeapon(), WeaponSpawn.transform.position, Quaternion.identity);
		actualWeapon.transform.parent = WeaponSpawn.transform;

        // UI:
        healthText.text = "Health: " + GameData.Instance.getPlayerHealth();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.C)) 
		{
			Destroy (actualWeapon);
			if (weaponSelected.Equals ("Shotgun")) {
				changeWeapon ("Revolver");
			}
		 	else if (weaponSelected.Equals ("Revolver"))
		    {
				changeWeapon ("Rifle");
				
			} 
			else 
			{
				changeWeapon ("Shotgun");
			}
		}
	}

	void changeWeapon(string newWeapon)
	{
		if (weaponSelected.Equals (newWeapon)) {
			;
		} 
		else {
			weaponSelected = newWeapon;
			actualWeapon= (GameObject)Instantiate(getactualWeapon(), WeaponSpawn.transform.position, Quaternion.identity);
			actualWeapon.transform.parent = transform;
		}
	}

	GameObject getactualWeapon()
	{
		if (weaponSelected.Equals ("Shotgun")) 
		{
			return Shotgun;
		}
		else if (weaponSelected.Equals ("Rifle")) 
		{
			return Rifle;
		}
		else
		{
			return Revolver;
		}
	}

    // #################### UI ####################
    public void changePlayerHealth(int value)
    {
        GameData.Instance.changePlayerHealth(value);
        healthText.text = "Health: " + GameData.Instance.getPlayerHealth();
    }
}
