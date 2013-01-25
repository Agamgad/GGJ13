using UnityEngine;
using System.Collections;

public class DefaultObstacle : MonoBehaviour {
	
	public string pattern = "F";
	public AnimationCurve transition;
	
	private GlobalSettings settings;
	private float progression;
	
	private Vector3 refPosition;
	private Vector3 nextPosition;
	private int patternPosition;
	
	// Use this for initialization
	void Start () {
		settings = GameObject.Find("GlobalSettings").GetComponent<GlobalSettings>();
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
				nextPosition.y += settings.GetGridDistance();
				break;
			case 'D':
				nextPosition.y -= settings.GetGridDistance();
				break;
			case 'L':
				nextPosition.x -= settings.GetGridDistance();
				break;
			case 'R':
				nextPosition.x += settings.GetGridDistance();
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
		progression += settings.GetBeat() * Time.deltaTime;
		if (progression > 1.0f) {	
			progression -= 1.0f;
			ToNextPosition();
		}
		
		float curT = transition.Evaluate(progression);
		
		transform.position = Vector3.Lerp(refPosition,nextPosition,curT);
		
		if (transform.position.z < 0.0f)
			Destroy(gameObject);
	}
}
