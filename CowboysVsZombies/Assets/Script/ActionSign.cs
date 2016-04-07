using UnityEngine;
using System.Collections;

public class ActionSign : MonoBehaviour {

    public float timer = 1;
    private float counter = 0;

	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;
        if (counter >= timer)
        {
            Destroy(this.gameObject);
        }
	}
}
