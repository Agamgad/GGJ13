using UnityEngine;
using System.Collections;

public class ObstacleGenerator : MonoBehaviour {
	
	public Transform toGenerate;
	
	private GlobalSettings settings;
	
	private float lastGen;
	
	// Use this for initialization
	void Start () {
		settings = GameObject.Find("GlobalSettings").GetComponent<GlobalSettings>();
		lastGen = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (lastGen + settings.GetBeat() < Time.time)
		{
			Instantiate(toGenerate, new Vector3(0.0f,0.0f, settings.spawnDistance), new Quaternion());
			lastGen = Time.time;
		}
	}
}
