using UnityEngine;
using System.Collections;

public class GlobalSettings : MonoBehaviour {
	
	public float spawnDistance = 20.0f;
	public int gridDepth = 10;
	
	public float acceptance = 0.0f;
	public float avoidance = 0.0f;
	public float accepted = 0;
	public float avoided = 0;
	
	public float dodgeRadius = 1.5f;
	
	public AudioSource source;
	public int nbBeats;
	
	public AudioClip[] clips;
	public GameObject[] generators;
	
	public AudioSource moai;
	
	public int currentStage = 0;
	public bool toNextStageAtEnd = false;
	private int lastTimeSamples = 0;
	
	// Use this for initialization
	void Start () {
		currentStage = -1;
		ToNextStage();
	}
	
	// Update is called once per frame
	void Update () {
	
		
		int curTimeSamples = source.timeSamples;
		if (curTimeSamples < lastTimeSamples && toNextStageAtEnd) {
			ToNextStage();
		}
		
		lastTimeSamples = curTimeSamples;
	}
	
	void ToNextStage() {
		// transition
		if (currentStage < clips.Length-1)
		{
			++currentStage;
			
			source.Stop();
			source.clip = clips[currentStage];
			source.Play();
			
			if (currentStage != 0)
				generators[currentStage-1].SetActive(false);
			generators[currentStage].SetActive(true);
			
			toNextStageAtEnd = false;
			
			//swipe all current cubes
			GameObject[] moais = GameObject.FindGameObjectsWithTag("Moai");
			for(int i=0; i<moais.Length; ++i) {
				moais[i].GetComponent<DefaultObstacle>().Vanish();
			}
		}
	}
	
	public float GetBeatProgression(int measure = 1) {
		float beatLength = ((float)audio.clip.samples) / ((float)(nbBeats/measure));
		float ret = Mathf.Repeat(((float)audio.timeSamples),beatLength) / beatLength;
		return ret;
	}
	
	public bool IsFirstBeat() {
		return audio.timeSamples < audio.clip.samples / nbBeats;
	}
	
	public int GetCurrentBeat(int measure = 1) {
		int curBeat = audio.timeSamples /(audio.clip.samples / nbBeats);
		return curBeat % measure;
	}
	
	public float GetGridDistance() {
		return spawnDistance / gridDepth;
	}
}
