using UnityEngine;
using System.Collections;

public class GlobalSettings : MonoBehaviour {
	
	public float spawnDistance = 20.0f;
	public int gridDepth = 10;
	
	
	public float startBeat = 1.0f;
	public float dodgeRadius = 2.0f;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public float GetBeat() {
		return startBeat;
	}
	
	public float GetObstacleSpeed() {
		return GetBeat() * GetGridDistance();
	}
	
	public float GetGridDistance() {
		return spawnDistance / gridDepth;
	}
	
	public float GetPlayerSpeed() {
		return 5.0f * dodgeRadius / GetBeat();
	}
}
