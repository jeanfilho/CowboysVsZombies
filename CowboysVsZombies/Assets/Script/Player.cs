﻿using UnityEngine;
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
	public ParticleEmitter Blood;
	public ParticleSystem Explosion;

	public Weapon actualWeapon;
	private bool shootable=true;
	public bool reload=false;
	private string weaponSelected;
	private float counter = 0;
	private bool counterActive=false;

    public GameObject bloodSplatter;
    public GameObject[] actionSign = new GameObject[5];

	public GameObject muzzle;

	public GameObject muzzle_revolver;
	public GameObject muzzle_shotgun_left;
	public GameObject muzzle_shotgun_right;
	public GameObject muzzle_rifle;

	public RawImage bloodScreen;
	public AudioClip pain;


    // UI:
 //   public Text healthText;

	// Use this for initialization
	void Start () 
	{
		weaponSelected="Shotgun";
		actualWeapon= (Weapon)Instantiate(getactualWeapon(), WeaponSpawn.transform.position, WeaponSpawn.transform.rotation);
		actualWeapon.transform.parent = WeaponSpawn.transform;
		Shotgun = actualWeapon;
		Revolver= (Weapon)Instantiate(Revolver, WeaponSpawn.transform.position, WeaponSpawn.transform.rotation);
		Revolver.transform.parent = WeaponSpawn.transform;
		Revolver.gameObject.SetActive (false);
		Rifle= (Weapon)Instantiate(Rifle, WeaponSpawn.transform.position, WeaponSpawn.transform.rotation);
		Rifle.transform.parent = WeaponSpawn.transform;
		Rifle.gameObject.SetActive (false);
		reload = false;
		shootable = true;


        // UI:
        //healthText.text = "Health: " + GameData.Instance.getPlayerHealth();
	}
	
	// Update is called once per frame
	void Update () 
	{

		//Blood screen fade
		if (bloodScreen.color.a > 0)
			bloodScreen.color -= new Color(0,0,0, Time.deltaTime);

		if (counterActive) 
		{
			counter += Time.deltaTime;

			if (actualWeapon.gameObject.GetComponent<Animator> ().GetBool("Reload")==true)
			{
				if (counter >= 1.0) {
					reload = false;
					actualWeapon.gameObject.GetComponent<Animator> ().SetBool ("Reload", false);
					counterActive = false;
					shootable = true;
					counter = 0;
				}
			} 
			else if (actualWeapon.gameObject.GetComponent<Animator> ().GetBool("Shoot")==true)
			{
				if (counter >= 0.49) 
				{
					actualWeapon.gameObject.GetComponent<Animator> ().SetBool ("Shoot", false);
					counterActive = false;
					shootable = true;
					counter = 0;
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.C) && !reload && shootable) 
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
		shoot ();
		reloadWeapon ();
	}

	void changeWeapon(string newWeapon)
	{
		if (!weaponSelected.Equals (newWeapon))
		{
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
		if (value < 0) {
			GetComponent<AudioSource> ().clip = pain;
			GetComponent<AudioSource> ().Play ();
			bloodScreen.color = new Color (1, 1, 1, 1);
		}
		GameData.Instance.changePlayerHealth(value);
   //     healthText.text = "Health: " + GameData.Instance.getPlayerHealth();
    }


	private void shoot()
	{
		
		
		if (Input.GetMouseButtonDown(0) && shootable && reload==false) 
		{

			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Screen.height - Input.mousePosition.y,0));
			RaycastHit hit;

			if (actualWeapon.shoot () == true) 
			{
				actualWeapon.GetComponent<AudioSource> ().Play ();

				if (weaponSelected.Equals("Revolver")) {
					Instantiate(muzzle, muzzle_revolver.transform.position, Quaternion.identity);
				}
				if (weaponSelected.Equals("Shotgun")) {
					Instantiate(muzzle, muzzle_shotgun_left.transform.position, Quaternion.identity);
					Instantiate(muzzle, muzzle_shotgun_right.transform.position, Quaternion.identity);
				}
				if (weaponSelected.Equals("Rifle")) {
					Instantiate(muzzle, muzzle_rifle.transform.position, Quaternion.identity);
				}

				if (Physics.Raycast (ray, out hit, 100, Physics.DefaultRaycastLayers)) 
				{
					//TODO Instantiate Blood Splatter

					GameObject otherObj = hit.collider.gameObject;
					if (otherObj.tag == "Enemy") {
						//Instantiate(par, hit.point, Quaternion.LookRotation(hit.normal));
						otherObj.gameObject.GetComponent<Zombie> ().setDamage (actualWeapon.getDamage ());



                        int actionSignID = Random.Range(0, 4);
                        Instantiate(actionSign[actionSignID], hit.point, this.gameObject.transform.rotation);
                        Instantiate(bloodSplatter, hit.point, otherObj.gameObject.transform.rotation);
					} 
					if (otherObj.tag == "Head") {
						//Instantiate(par, hit.point, Quaternion.LookRotation(hit.normal));
						otherObj.gameObject.GetComponentInParent<Zombie> ().kill();

						Instantiate(actionSign[0], hit.point, this.gameObject.transform.rotation);
						Instantiate(bloodSplatter, hit.point, otherObj.gameObject.transform.rotation);
					} 
					else if (otherObj.tag == "Barrel") 
					{
						//Instantiate(Explosion, hit.point, Quaternion.LookRotation(hit.normal));
						otherObj.gameObject.GetComponent<Barrel> ().applyShoot ();
						//Destroy (otherObj.gameObject);

                        Instantiate(actionSign[2], hit.point, this.gameObject.transform.rotation);
					}
				}




				shootable = false;
				actualWeapon.gameObject.GetComponent<Animator>().SetBool("Shoot",true);
				counterActive = true;
			} 
			else 
			{
				//Do Something
			}

		}
	}

	private void reloadWeapon()
	{
		if ((actualWeapon.isEmpty () || Input.GetKeyDown (KeyCode.R)) && (actualWeapon.gameObject.GetComponent<Animator> ().GetBool("Shoot")==false)&& reload==false)
		{
			shootable = false;
			reload = true;
			if (actualWeapon.reload()) 
			{
				actualWeapon.gameObject.GetComponent<Animator>().SetBool("Reload",true);
				counterActive = true;
			} 
			else 
			{
				this.changeWeapon ("Revolver");
				shootable = true;
				reload = false;
			}
		}
	}

	public int getActualWPBulletsMag()
	{
		return actualWeapon.getMunition ();
	}

	public int getActualWPBulletsOverAll()
	{
		return actualWeapon.getMunitionAll ();
	}


}
