using UnityEngine;
using System.Collections;

public class ObstacleGenerator : MonoBehaviour {
	
	public Transform toGenerate;
	public string[] patterns;
	private int patternGeneration;
	
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
				char pos = patterns[patternGeneration][patternPosition];
				if (pos != '.')
					SpawnMoai(pos);
				
				++patternPosition;
				if (patternPosition >= patterns[patternGeneration].Length) {
					patternPosition = 0;
					patternGeneration = Random.Range(0,patterns.Length);
				}
			}
			lastBeat = nextBeat;
		}
	}
	
	void SpawnMoai(int x, int y) {
		Transform obj = Instantiate(toGenerate) as Transform;
		obj.transform.position = new Vector3(x * settings.dodgeRadius+0.5f,y * settings.dodgeRadius, settings.spawnDistance);
		obj.GetComponent<DefaultObstacle>().JumpToPatternPosition(patternPosition);
	}
	
	void RandomSpawnMoai() {
		SpawnMoai(Random.Range(0,3)-1,Random.Range(0,3)-1);
	}
	
	void SpawnMoai(char pos) {
		switch(pos) {
		case 'U':	SpawnMoai( 0, 1);	break;
		case 'N':	SpawnMoai( 1, 1);	break;
		case 'R':	SpawnMoai( 1, 0);	break;
		case 'E':	SpawnMoai( 1,-1);	break;
		case 'D':	SpawnMoai( 0,-1);	break;
		case 'S':	SpawnMoai(-1,-1);	break;
		case 'L':	SpawnMoai(-1, 0);	break;
		case 'O':	SpawnMoai(-1, 1);	break;
		default:	SpawnMoai( 0, 0);	break;
		}
	}
}
