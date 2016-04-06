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

	public Weapon Shotgun;
	public Weapon Revolver;
	public Weapon Rifle;
	public GameObject WeaponSpawn;

	public Weapon actualWeapon;
	private bool shootable=true;
	private bool reload=false;
	private string weaponSelected;



    // UI:
    public Text healthText;

	// Use this for initialization
	void Start () 
	{
		weaponSelected="Shotgun";
		actualWeapon= (Weapon)Instantiate(getactualWeapon(), WeaponSpawn.transform.position, WeaponSpawn.transform.rotation);
		actualWeapon.transform.parent = WeaponSpawn.transform;
		Shotgun = actualWeapon;
		Revolver= (Weapon)Instantiate(Revolver, WeaponSpawn.transform.position, WeaponSpawn.transform.rotation);
		Revolver.transform.parent = WeaponSpawn.transform;
		Rifle= (Weapon)Instantiate(Rifle, WeaponSpawn.transform.position, WeaponSpawn.transform.rotation);
		Rifle.transform.parent = WeaponSpawn.transform;

        // UI:
        healthText.text = "Health: " + GameData.Instance.getPlayerHealth();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.C)) 
		{
			if (weaponSelected.Equals ("Shotgun")) 
			{
				Shotgun.gameObject.SetActive (false);
				changeWeapon ("Revolver");
				Revolver.gameObject.SetActive (true);
			}
		 	else if (weaponSelected.Equals ("Revolver"))
		    {
				Revolver.gameObject.SetActive (false);
				changeWeapon ("Rifle");
				Rifle.gameObject.SetActive (true);
				
			} 
			else 
			{
				Rifle.gameObject.SetActive (false);
				changeWeapon ("Shotgun");
				Shotgun.gameObject.SetActive (true);
			}
		}
	}

	void FixedUpdate ()
	{
		reloadWeapon ();
		shoot ();
	}

	void changeWeapon(string newWeapon)
	{
		if (weaponSelected.Equals (newWeapon)) {
			;
		} 
		else {
			weaponSelected = newWeapon;
			actualWeapon=getactualWeapon();
			/*
			weaponSelected = newWeapon;
			actualWeapon= (Weapon)Instantiate(getactualWeapon(), WeaponSpawn.transform.position, WeaponSpawn.transform.rotation);
			actualWeapon.transform.parent = WeaponSpawn.transform;
			*/
		}
	}

	Weapon getactualWeapon()
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


	private void shoot()
	{
		
		if (Input.GetMouseButtonDown(0)&&shootable && !reload) 
		{
			shootable = false;
			if (actualWeapon.shoot () == true) 
			{
				
				shootable = true;	
			} 
			else 
			{
				//Do Something
			}

		}
	}

	private void reloadWeapon()
	{
		if (actualWeapon.isEmpty () || Input.GetKeyDown (KeyCode.R)) 
		{
			shootable = false;
			reload = true;
			if (actualWeapon.reload()) 
			{
				//actualWeapon.GetComponent<Animation>.Play (actualWeapon.GetComponent<Animation>.ReloadAnimation);
				/*	if (actualWeapon.animation [actualWeapon.ReloadAnimation.name].time >= actualWeapon.animation [actualWeapon.ReloadAnimation.name].length - 0.3) 
				{*/
					shootable = true;
					reload = false;
				//}
			} 
			else 
			{
				this.changeWeapon ("Revolver");
				shootable = true;
				reload = false;
			}
		}
	}


}
