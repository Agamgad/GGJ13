using UnityEngine;
using System.Collections;

public class DefaultObstacle : MonoBehaviour {
	
	public string pattern = "F";
	public AnimationCurve transition;
	
	private GlobalSettings settings;
	private CamControlScript player;
	
	private float progression;
	
	private Vector3 refPosition;
	private Vector3 nextPosition;
	private int patternPosition;
	
	// Use this for initialization
	void Start () {
		settings = GameObject.Find("GlobalObject").GetComponent<GlobalSettings>();
		player = GameObject.Find("Player").GetComponent<CamControlScript>();
		progression = 0.0f;
		patternPosition = 0;
		nextPosition = transform.position;
		ToNextPosition();
	}
	
	void ToNextPosition() {
		refPosition = nextPosition;
		
		while(pattern[patternPosition] != '.')
		{
			switch (pattern[patternPosition]) {
			case 'F':
				nextPosition.z -= settings.GetGridDistance();
				break;
			case 'B':
				nextPosition.z += settings.GetGridDistance();
				break;
			case 'U':
				nextPosition.y += settings.dodgeRadius;
				break;
			case 'D':
				nextPosition.y -= settings.dodgeRadius;
				break;
			case 'L':
				nextPosition.x -= settings.dodgeRadius;
				break;
			case 'R':
				nextPosition.x += settings.dodgeRadius;
				break;
			default:break;
			}
		
			patternPosition += 1;
			if (patternPosition >= pattern.Length)
			{
				patternPosition = 0;
				return;
			}
		}
		
		patternPosition += 1;
		if (patternPosition >= pattern.Length)
			patternPosition = 0;
	}
	
	// Update is called once per frame
	void Update () {
		float nextProgression = settings.GetBeatProgression();
		
		if (nextProgression < progression)
			ToNextPosition();
		
		progression = nextProgression;
		
		/*
		progression += Time.deltaTime / settings.GetBeat();
		if (progression >= 1.0f) {	
			progression -= 1.0f;
			ToNextPosition();
		}*/
		
		float curT = transition.Evaluate(progression);
		
		transform.position = Vector3.Lerp(refPosition,nextPosition,curT);
		
		if (transform.position.z <= 0.0f)
		{
			player.NotifyCubeDeath(transform);
			Destroy(gameObject);	
		}
	}
}
