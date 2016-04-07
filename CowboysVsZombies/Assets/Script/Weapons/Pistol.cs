using UnityEngine;
using System.Collections;

public class Pistol :Weapon {


	private int reloadTime=2;
	private int magazineSize=6;
	private int remainingBulletsOverall=100;
	public int remainingBullets;
	private int damage=30;
	private bool shootable=true;

	// Use this for initialization
	void Start () 
	{
		remainingBullets = magazineSize;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int ReloadTime {
		get {
			return reloadTime;
		}
	}


	public override bool shoot()
	{
		if (this.shootable == true) 
		{
			remainingBullets -= 1;
			return true;
		} 
		else 
		{
			return false;
		}
	}



	public override bool reload()
	{
		this.shootable = false;
		if (this.remainingBulletsOverall > 0) {
			while ((magazineSize - remainingBullets) > 0 && remainingBulletsOverall > 0) {
				remainingBulletsOverall -= 1;
				remainingBullets += 1;
			}
			this.shootable = true;
			return true;
		} 
		else 
		{
			return false;
		}
			
	}


	public override bool isEmpty()
	{
		return this.remainingBullets == 0;
	}

	public override int getMunition()
	{
		return remainingBullets;
	}

	public override int getMunitionAll()
	{
		return remainingBulletsOverall;
	}

	public override int getDamage ()
	{
		return this.damage;
	}
}
