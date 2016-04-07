using UnityEngine;
using System.Collections;

/*
 * This script is used for:
 * -Updating Material, Position and Scale of this data column based
 * on the current minimum and maximum heartrate data
 * 
 */

public class DataColumn : MonoBehaviour {

	public DataCollector dc;
	public float value;

	float ttl = 30;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (dc != null)
		{
			GetComponent<Renderer> ().material.color = dc.dataToColor (value);

			transform.localScale = dc.dataToScale (value);

			Vector3 newPos = transform.position;
			newPos.y = transform.localScale.y / 2;
			transform.position = newPos;
		}

		if (ttl <= 0)
			Destroy (gameObject);
		else
			ttl -= Time.deltaTime;
	}

	public void setDataCollector(DataCollector dc)
	{
		this.dc = dc;
	}

	public void setValue(float value)
	{
		this.value = value;
	}
}
