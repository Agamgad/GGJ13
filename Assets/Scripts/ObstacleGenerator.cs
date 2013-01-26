using UnityEngine;
using System.Collections;

public class ObstacleGenerator : MonoBehaviour {
	
	public Transform toGenerate;
	
	private GlobalSettings settings;
	
	private float lastBeat;
	
	// Use this for initialization
	void Start () {
		settings = GameObject.Find("GlobalObject").GetComponent<GlobalSettings>();
		lastBeat = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		float nextBeat = settings.GetBeatProgression();
		if (nextBeat < lastBeat)
		{
			Instantiate(toGenerate, new Vector3(0.0f,0.0f, settings.spawnDistance), new Quaternion());
		}
		lastBeat = nextBeat;
	}
}
