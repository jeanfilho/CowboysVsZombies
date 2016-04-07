using UnityEngine;
using System.Collections;

public class HeartMonitor : MonoBehaviour {

	float heartrate;

	private float heartrate_falling=120;
	private float heartrate_rising=30;
	private float heartrate_alternating= 60;
	private bool  heartrate_alternating_up=true;

	void Start () {
		// Start communication with heart monitor
		//TODO
	
	}
	

	void FixedUpdate () {
		heartrate = sampleHeartRate_alternating();
		if (Input.GetKey (KeyCode.V))
			heartrate = 30;
	
	}

	public float getHeartRate()
	{
		return heartrate;
	}

	// Get samples from hear monitor
	float sampleHeartRate_random()
	{
		float value = 0;

		//TODO
		value = Random.Range(30.0f, 120.0f);

		return value;
	}

	float sampleHeartRate_constantFalling(){
		heartrate_rising-=0.1f;
		if (heartrate_falling <= 30)
			heartrate_falling = 120;
		return heartrate_rising;
	}
	float sampleHeartRate_constantRising(){
		heartrate_rising+=0.1f;
		if (heartrate_falling >= 120)
			heartrate_falling = 30;
		return heartrate_rising;
	}
	float sampleHeartRate_alternating(){
		if (heartrate_alternating_up) {
			heartrate_alternating += 0.1f;
			if (heartrate_alternating >= 120)
				heartrate_alternating_up = false;
		} else {
			heartrate_alternating-=0.1f;
			if (heartrate_alternating <= 30)
				heartrate_alternating_up = true;
		}
		return heartrate_alternating;
	}

}
