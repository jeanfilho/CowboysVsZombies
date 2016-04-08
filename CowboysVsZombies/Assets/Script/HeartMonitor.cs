
using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class HeartMonitor : MonoBehaviour {

	SerialPort port;
	bool isReady = false;
	float timer = 1;

	[HideInInspector]
	public float heartrate = 0;

	private float heartrate_falling=120;
	private float heartrate_rising=60;
	private float heartrate_alternating= 90;
	private bool  heartrate_alternating_up=true;

	void Start () {
		// Start communication with heart monitor
		try
		{
			port = new SerialPort("\\\\.\\COM13", 115200, Parity.None, 8, StopBits.One);
			port.Open ();
			port.ReadTimeout = 1;
			port.WriteTimeout = 1;
			byte[] msg1 = { 0x02, 0x14, 1, 1, crc8PushByte (1, 0), 0x03 };
			byte[] msg2 = { 0x02, 0x15, 1, 0, crc8PushByte (0, 0), 0x03 };
			byte[] msg3 = { 0x02, 0x16, 1, 0, crc8PushByte (0, 0), 0x03 };
			byte[] msg4 = { 0x02, 0x19, 1, 0, crc8PushByte (0, 0), 0x03 };
			byte[] msg5 = { 0x02, 0x1E, 1, 0, crc8PushByte (0, 0), 0x03 };
			byte[] msg6 = { 0x02, 0xBD, 1, 0, crc8PushByte (0, 0), 0x03 };

			byte[] ans = new byte[1024];
			port.Write (msg1, 0, 6);
			port.Write (msg2, 0, 6);
			port.Write (msg3, 0, 6);
			port.Write (msg4, 0, 6);
			port.Write (msg5, 0, 6);
			port.Write (msg6, 0, 6);
		}catch { }

		isReady = true;
	}
	
	void OnDestroy()
	{
		port.Close ();
	}

	void Update()
	{
		if (isReady)
		{
			if (timer <= 0)
			{
				heartrate = sampleHeartRate ();
				timer = 1;

			} else
			{
				timer -= Time.deltaTime;
			}
		}
	}

	// Get samples from hear monitor
	public float sampleHeartRate()
	{
		float value;

		if (port.IsOpen)
		{
			byte[] ans = new byte[1024];
			try
			{
				int br = port.Read (ans, 0, 1024);
				string response = "";
				for (int i = 0; i < br; i++)
				{
					if(i % 8 == 0)
						response = response + " ";
					if (i % 16 == 0)
						response = response + "\n";
					response = response + " " + ans[i].ToString("X");

				}
				//Debug.Log ("Bytes read: " +  br +" | "+ response);
			}
			catch
			{
				Debug.Log ("No info received");
			}
			if (ans [1] == 0x20)
				value = ans [12];
			else
				value = heartrate;

			Debug.Log ("Heartrate: " + value);

		} else
		{
			value = Random.Range (80.0f, 120.0f);
		}
		return value;
	}

	byte crc8PushByte(byte crc, byte ch)
	{
		byte i;
		crc = (byte)(crc ^ ch);
		for(i = 0; i < 8; i++)
		{
			if ((crc & 1) == 1)
				crc = (byte)((crc >> 1) ^ 0x8c);
			else
				crc = (byte)(crc >> 1);
		}
		return crc;
	}




	public float sampleHeartRate_constantFalling(){
		heartrate_rising-=0.1f;
		if (heartrate_falling <= 60)
			heartrate_falling = 120;
		return heartrate_rising;
	}
	public float sampleHeartRate_constantRising(){
		heartrate_rising+=0.1f;
		if (heartrate_falling >= 120)
			heartrate_falling = 60;
		return heartrate_rising;
	}
	public float sampleHeartRate_alternating(){
		if (heartrate_alternating_up) {
			heartrate_alternating += 0.1f;
			if (heartrate_alternating >= 120)
				heartrate_alternating_up = false;
		} else {
			heartrate_alternating-=0.1f;
			if (heartrate_alternating <= 60)
				heartrate_alternating_up = true;
		}
		return heartrate_alternating;
	}
}
