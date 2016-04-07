
public class GameData
{

    private static GameData instance;

    private GameData()
    {
        if (instance != null) return;
        instance = this;
    }

    public static GameData Instance
    {
        get
        {
            if (instance == null)
            {
                new GameData();
            }

            return instance;
        }
    }


    private float spawnRateAdjustment = 1.0f;
    public float getSpawnRateAdjustment()
    {
        return spawnRateAdjustment;
    }
    public void setSpawnRateAdjustment(float value)
    {
        spawnRateAdjustment = value;
    }

    private int playerHealth = 50;
    public float getPlayerHealth()
    {
        return playerHealth;
    }
    public void setPlayerHealth(int value)
    {
        playerHealth = value;
    }

    public void changePlayerHealth(int value)
    {
        playerHealth = playerHealth + value;
        if (playerHealth > 100)
        {
            playerHealth = 100;
        }
        else if (playerHealth < 0)
        {
            playerHealth = 0;
        }
    }

	private float zombieHPModifier = 1.0f;
	public float getZombieHPModifier(){
		return zombieHPModifier;
	}
	public void setZombieHPModifier(float value){
		zombieHPModifier = value;
	}
	private float zombieDMGModifier = 1.0f;
	public float getZombieDMGModifier(){
		return zombieDMGModifier;
	}
	public void setZombieDMGModifier(float value){
		zombieDMGModifier = value;
	}

}

