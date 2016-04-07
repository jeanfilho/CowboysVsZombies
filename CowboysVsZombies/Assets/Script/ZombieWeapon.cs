using UnityEngine;
using System.Collections;

public class ZombieWeapon : MonoBehaviour {

    public Zombie zombie;

    public int weaponID = 0;

    public int attackDamage = 0;


	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (zombie.getAttackMode() && zombie.getAttacking() && zombie.getAttackID() == weaponID)
            {
                other.gameObject.GetComponent<Player>().changePlayerHealth(-1 * attackDamage);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        
    }
}
