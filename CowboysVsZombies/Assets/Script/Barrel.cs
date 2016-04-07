using UnityEngine;
using System.Collections;

public class Barrel : MonoBehaviour {

	public ParticleSystem explosion;

	private Vector3 location;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		location=transform.position;
	}

	public void applyShoot()
	{
		this.gameObject.GetComponent<Collider>().enabled=false;
		float radius = 2.0f;
		Collider col;
		Barrel barrel;
		Zombie zombie;
		Collider[] objectsInRange = Physics.OverlapSphere(location, radius);
		Debug.Log (objectsInRange.Length);
		for (int i=0;i<objectsInRange.Length;i++) 
		{
			col = objectsInRange [i];
			if (col.gameObject.tag.Equals("Barrel") && col.gameObject!=this.gameObject)
			{
				barrel = col.GetComponent<Barrel> ();
				barrel.SendMessage ("applyShoot");
			}

			else if (col.gameObject.tag.Equals("Enemy") && col.gameObject!=this.gameObject) 
			{
				zombie= col.GetComponent<Zombie> ();
				// linear falloff of effect
				float proximity = (transform.position - zombie.transform.position).magnitude;
				float effect = 1 - (proximity / radius);
				zombie.setDamage (1000);
				//zombie.SendMessage ("setDamage ((int)(1000 ))");
			}
				
		}
		Instantiate (explosion, transform.position, Quaternion.identity);
		Destroy (this.gameObject);
	}
}
