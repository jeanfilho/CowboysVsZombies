using UnityEngine;
using System.Collections;

public class MenuZombies : MonoBehaviour {

	Transform startTransform;

	// Use this for initialization
	void Awake () {
		startTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	float timer;
	void Update () {
		gameObject.transform.position = startTransform.position;
		gameObject.transform.rotation = startTransform.rotation;
	
	}
}
