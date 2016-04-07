using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour {

    public Image healthBarHUD;
    public Text ammoLoaded;
    public Text ammoCurrent;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
     //   updateHealth();
     //   updateAmmo();
	}

    public void updateHealth()
    {
        healthBarHUD.GetComponent<Image>().fillAmount = (GameData.Instance.getPlayerHealth()) / 100.0f;
    }

    public void updateAmmo()
    {
    //    ammoLoaded.text = ;
    //    ammoCurrent.text = ;
    }
}
