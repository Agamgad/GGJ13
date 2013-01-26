using UnityEngine;
using System.Collections;

public class GlobalSettings : MonoBehaviour {
	
	public float spawnDistance = 20.0f;
	public int gridDepth = 10;
	
	public float collectance = 0.0f;
	public float avoidance = 0.0f;
	public int collected = 0;
	public int avoided = 0;
	
	public float startBeat = 1.0f;
	public float dodgeRadius = 2.0f;
	
	public AudioSource source;
	public int nbBeats;
	
	
	// Use this for initialization
	void Start () {
		startBeat = source.clip.length / ((float)nbBeats);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/*public float GetBeat() {
		return startBeat;
	}*/
	
	public float GetBeatProgression() {
		float beatLength = ((float)audio.clip.samples) / ((float)nbBeats);
		float ret = Mathf.Repeat(((float)audio.timeSamples),beatLength) / beatLength;
		return ret;
	}
	
	/*public float GetObstacleSpeed() {
		return GetBeat() * GetGridDistance();
	}*/
	
	public float GetGridDistance() {
		return spawnDistance / gridDepth;
	}
	
	/*public float GetPlayerSpeed() {
		return 5.0f * dodgeRadius / GetBeat();
	}*/
}
