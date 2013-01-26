using UnityEngine;
using System.Collections;

public class SideSoundsScript : MonoBehaviour {
	
	public float spawnDistance = 20.0f;
	public int gridDepth = 10;
	
	public AudioSource source;
	public int nbBeats;
	
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public float GetBeatProgression(int measure = 1) {
		float beatLength = ((float)audio.clip.samples) / ((float)(nbBeats/measure));
		float ret = Mathf.Repeat(((float)audio.timeSamples),beatLength) / beatLength;
		return ret;
	}
	
	public bool IsFirstBeat() {
		return audio.timeSamples < audio.clip.samples / nbBeats;
	}
	
	public float GetGridDistance() {
		return spawnDistance / gridDepth;
	}
}
