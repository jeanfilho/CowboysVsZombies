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

    public float attackDuration = 0.1f;
    public float attackCounter = 0;
    public bool attacking = false;
    private int attackID = -1;

    private float attackCooldown = 0.8f;
    private float attackCooldownCounter = 0;
    private bool attackingCooldown = false;

    public bool attackMode = false;


	// Use this for initialization
	void Start () 
	{
		this.gameObject.GetComponent<Animator>().SetBool("Dead",false);
		this.gameObject.GetComponent<Animator>().SetBool("isWalking",true);
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

					GameData.Instance.setScore (GameData.Instance.getScore () + Mathf.FloorToInt(10*GameData.Instance.getZombieHPModifier()));
					Destroy (this.gameObject);

				}
			}
		}

		checkLifeHealth ();

        if (attackMode && !attacking)
        {
            attacking = true;
            attack();              
        }
        else if (attacking)
        {
            attackCounter += Time.deltaTime;
            if (attackCounter >= attackDuration)
            {
                attackCounter = 0;
                attacking = false;
                this.gameObject.GetComponent<Animator>().SetBool("isAttacking", false);
                this.gameObject.GetComponent<Animator>().SetBool("isBiting", false);
                attackID = -1;
                attackingCooldown = true;
            }
        }        
	}

	public int getHealth()
	{
		return health;
	}

	public void setHealth(int value){
		health = value;
	}

	public void setDamage(int Damage)
	{
		this.health -= Damage;
	}

	private void attack ()
	{
        int attackStyle = Random.Range(0, 2);
        attackID = attackStyle;

        if (attackStyle > 0)
        {       
            this.gameObject.GetComponent<Animator>().SetBool("isAttacking", true);
            this.gameObject.GetComponent<Animator>().SetBool("isBiting", false);
        }
        else
        {
            this.gameObject.GetComponent<Animator>().SetBool("isAttacking", false);
            this.gameObject.GetComponent<Animator>().SetBool("isBiting", true);
        }

        
	}

	public void checkLifeHealth()
	{
		if (this.health <= 0) 
		{
			this.gameObject.GetComponent<Animator>().SetBool("Dead",true);
			counterActive = true;
		}
	}

    void OnTriggerEnter(Collider other)
    {
        attackMode = true;
    }

    void OnTriggerExit(Collider other)
    {
        attackMode = false;
    }

    public void setAttackMode(bool state)
    {
        attackMode = state;
    }

    public bool getAttackMode()
    {
        return attackMode;
    }

    public bool getAttacking()
    {
        return attacking;
    }

    public int getAttackID()
    {
        return attackID;
    }
}
