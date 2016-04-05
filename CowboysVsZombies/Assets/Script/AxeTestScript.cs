using UnityEngine;
using System.Collections;

public class AxeTestScript : MonoBehaviour {
	private bool spin;
	// Use this for initialization
	void Start () {
		spin = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (spin) {
			this.transform.RotateAround (new Vector3 (0, 0, 0), new Vector3 (0, 1, 0), 20);
		}
	}
	public void spinTrue(){
		spin = true;
		Debug.Log ("SPIN");
	}
}
