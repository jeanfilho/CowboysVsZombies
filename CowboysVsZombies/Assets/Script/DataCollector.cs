using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DataCollector : MonoBehaviour {

	public GameObject player;
	public float frequency = 1.0f;
	public HeartMonitor heartMonitor;
	public GameObject DataColumn;

	float timer = 0.0f;
	List<Data> storedSamples = new List<Data> ();

	float minimum = 90;
	float maximum = 91;
	string fileName;
	StreamWriter file;

	//Data for image generation later on
	struct Data
	{
		public float heartrate;
		public Vector3 position;
	}

	void OnDestroy()
	{
		reset ();
	}

	void FixedUpdate()
	{
		if (LevelController.isReplay)
			replayData ();
		else if (LevelController.isGame && !LevelController.isPaused)
			storeSamples ();
	}

	//Store samples in the frequency defined before;
	void storeSamples()
	{
		if (timer <= 0)
		{
			float heartrate = heartMonitor.sampleHeartRate ();
			Vector3 position = player.transform.position;

			if (heartrate > maximum)
				maximum = heartrate;
			if (heartrate < minimum)
				minimum = heartrate;
			
			Data sample = new Data ();
			sample.heartrate = heartrate;
			sample.position = position;
			storedSamples.Add (sample);

			file.WriteLine (heartrate + "," + position.x + "," + position.y + "," + position.z);

			timer = frequency;
		} else
		{
			timer -= Time.deltaTime;
		}
	}

	//Replays previously stored Data
	int i = 0;
	void replayData()
	{
		if (timer <= 0 && i < storedSamples.Count)
		{
			Vector3 position = storedSamples [i].position;

			GameObject obj = (GameObject) Instantiate (DataColumn);
			obj.transform.position = position;
			obj.GetComponent<Renderer> ().material = new Material(obj.GetComponent<Renderer> ().sharedMaterial);
			obj.GetComponent<DataColumn> ().setDataCollector (this);
			obj.GetComponent<DataColumn> ().setValue (storedSamples [i].heartrate);

			timer = frequency;
			i++;
		} else
		{
			timer -= Time.deltaTime;
		}
	}

	//Returns a color ranging from green (lowest value) to red (highest) based on current maximum and minimum
	public Color dataToColor(float value)
	{
		Color result;
		float green = 1;
		float red = 0;
		if (maximum - minimum > 0)
		{
			value = (value - minimum) / (maximum - minimum);
			value *= 2; //(From (0,255,0) to (255,255,0) to (255,0,0))

			if (value > 1)
			{
				red = 1;
				green -= (value - 1);
			} else
			{
				red = value;
			}
		}

		result = new Color (red, green, 0, 0.7f);
		return result;
	}

	//Returns a new scale based on current maximum and minimum
	public Vector3 dataToScale(float value)
	{
		if ((maximum - minimum) == 0)
		{
			value = 1;
		} else
		{
			value = (value - minimum) / (maximum - minimum);
			value = value * 5 + 1;
		}

		return new Vector3(0.5f, value, 0.5f);
	}

	public void loadSamples()
	{
		StreamReader read = new StreamReader (LevelController.replaySession);
		while (!read.EndOfStream)
		{
			string[] values = read.ReadLine ().Split (",".ToCharArray(), 4);
			Data data = new Data();
			data.heartrate = float.Parse(values [0]);
			if (data.heartrate > maximum)
				maximum = data.heartrate;
			if (data.heartrate < minimum)
				minimum = data.heartrate;
			data.position = new Vector3 (float.Parse (values [1]), float.Parse (values [2]), float.Parse (values [3]));
			storedSamples.Add (data);
		}
		read.Close ();
	}

	public void createSampleFile()
	{
		int i = 0;
		if(!Directory.Exists("Data/"))
			Directory.CreateDirectory ("Data/");
		while (File.Exists ("Data/Session_" + i.ToString("D3") + ".txt"))
			i++;
		fileName = "Data/Session_" + i.ToString("D3") + ".txt";
		FileStream c = File.Create (fileName);
		c.Close ();
		file = new StreamWriter(fileName);
	}

	public void reset()
	{
		timer = frequency;
		storedSamples.Clear();
		minimum = 90;
		maximum = 91;
		if(file != null)
			file.Close ();
	}
		
}
