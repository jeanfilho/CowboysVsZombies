using UnityEngine;
using System.Collections;
/*
 * This class is used as a template for:
 * -Weapon shoot
 * -Damage
 * -Accuracy
 * -Stability
 * -Rate of fire
 * -Ammo
 * -Weapon Level
 * 
 */
public abstract class Weapon : MonoBehaviour {


	public AnimationClip ReloadAnimation;
	public AnimationClip ShootAnimation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public abstract int getMunition();

	public abstract int getMunitionAll();

	public abstract bool isEmpty ();

	public abstract bool shoot ();

	public abstract bool reload ();

	public abstract int getDamage ();
}
