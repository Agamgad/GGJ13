using UnityEngine;
using System.Collections;

public class ObstacleGenerator : MonoBehaviour {
	
	public Transform toGenerate;
	public string patternGeneration;
	
	private GlobalSettings settings;
	
	private float lastBeat;
	private int patternPosition;
	private bool isStarted;
	private float timeCreated;
	
	// Use this for initialization
	void Start () {
		settings = GameObject.Find("GlobalObject").GetComponent<GlobalSettings>();
		lastBeat = 1.0f;
		patternPosition = 0;
		isStarted = false;
		timeCreated = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isStarted && Time.time > timeCreated+2.0f && settings.IsFirstBeat())
			isStarted = true;
		
		if (isStarted)
		{
			float nextBeat = settings.GetBeatProgression();
			if (nextBeat < lastBeat)
			{
				if (patternGeneration[patternPosition] != '.')
					RandomSpawnMoai();
				
				++patternPosition;
				if (patternPosition >= patternGeneration.Length)
					patternPosition = 0;
			}
			lastBeat = nextBeat;
		}
	}
	
	void SpawnMoai(int x, int y) {
		Transform obj = Instantiate(toGenerate) as Transform;
		obj.transform.position = new Vector3(x * settings.dodgeRadius,y * settings.dodgeRadius, settings.spawnDistance);
		obj.GetComponent<DefaultObstacle>().JumpToPatternPosition(patternPosition);
	}
	
	void RandomSpawnMoai() {
		SpawnMoai(Random.Range(0,3)-1,Random.Range(0,3)-1);
	}
}
