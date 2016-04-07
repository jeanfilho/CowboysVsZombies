using UnityEngine;
using System.Collections;

/*
 * This class is used for:
 * -HP
 * -Attack Damage
 * -Animations
 * -Appeaerance randomness
 * -Difficulty Level
 * -Pathfinding
 * 
 */
public class Zombie : MonoBehaviour 
{

	private int health = 100;
	private float counter = 0;
	private bool counterActive=false;


	// Use this for initialization
	void Start () 
	{
		this.gameObject.GetComponent<Animator>().SetBool("Dead",false);
		this.gameObject.GetComponent<Animator>().SetBool("isWalking",false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (counterActive) {
			counter += Time.deltaTime;
			if (this.gameObject.GetComponent<Animator> ().GetBool ("Dead") == true) 
			{
				if (counter >= 3.0) 
				{
					Destroy (this.gameObject);
				}
			}
		}


		checkLifeHealth ();
	}

	public int getHealth()
	{
		return health;
	}

	public void setDamage(int Damage)
	{
		this.health -= Damage;
	}

	private void attack ()
	{
		//TODO
	}

	public void checkLifeHealth()
	{
		if (this.health <= 0) 
		{
			this.gameObject.GetComponent<Animator>().SetBool("Dead",true);
			counterActive = true;
		}
	}
}
