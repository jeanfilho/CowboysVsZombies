using UnityEngine;
using System.Collections;

public class HeartMonitor : MonoBehaviour {

	float heartrate;

	void Start () {
		// Start communication with heart monitor
		//TODO
	
	}
	

	void FixedUpdate () {
		heartrate = sampleHeartRate();
	
	}

	public float getHeartRate()
	{
		return heartrate;
	}

	// Get samples from hear monitor
	float sampleHeartRate()
	{
		float value = 0;

		//TODO
		value = Random.Range(80.0f, 130.0f);

		return value;
	}
}
