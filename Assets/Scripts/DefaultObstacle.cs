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
	public int patternPosition;
	
	private float vanishAlpha = 1.0f;
	
	// Use this for initialization
	void Start () {
		settings = GameObject.Find("GlobalObject").GetComponent<GlobalSettings>();
		player = GameObject.Find("Player").GetComponent<CamControlScript>();
		progression = 0.0f;
		nextPosition = transform.position;
		ToNextPosition();
		SetAlpha(0.0f);
	}
	
	public void JumpToPatternPosition(int pos) {
		
		patternPosition = 0;
		for(int i=0; i<pos; ++i)
		{
			do
			{
				patternPosition += 1;
				if (patternPosition >= pattern.Length)
					patternPosition = 0;
			} while(pattern[patternPosition] != '.');
			
			patternPosition += 1;
			if (patternPosition >= pattern.Length)
				patternPosition = 0;
		}
	}
	
	void ToNextPosition() {
		refPosition = nextPosition;
		
		while(pattern[patternPosition] != '.')
		{
			char mvt = pattern[patternPosition];
			
			if ((mvt == 'A' || mvt == 'H')
			&&	(refPosition.x != 0.0f || refPosition.y != 0.0f)) {
				if (refPosition.y < -0.1f) {
					if (refPosition.x < 0.4f)
						mvt = mvt == 'A' ? 'R' : 'U';
					else if (refPosition.x >= 0.4f && refPosition.x <= 0.6f)
						mvt = mvt == 'A' ? 'R' : 'L';
					else if (refPosition.x > 0.6f)
						mvt = mvt == 'A' ? 'U' : 'L';		
				}
				else if (refPosition.y >= -0.1f && refPosition.y <= 0.1f) {
					if (refPosition.x < 0.4f)
						mvt = mvt == 'A' ? 'D' : 'U';
					else if (refPosition.x > 0.6f)
						mvt = mvt == 'A' ? 'U' : 'D';
				}
				else if (refPosition.y > 0.1f) {
					if (refPosition.x < 0.4f)
						mvt = mvt == 'A' ? 'D' : 'R';
					else if (refPosition.x >= 0.4f && refPosition.x <= 0.6f)
						mvt = mvt == 'A' ? 'L' : 'R';
					else if (refPosition.x > 0.6f)
						mvt = mvt == 'A' ? 'L' : 'D';		
				}
			}
			
			switch (mvt) {
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
		if (!settings.isPaused) {
			float nextProgression = settings.GetBeatProgression();
			
			if (nextProgression < progression)
				ToNextPosition();
			
			progression = nextProgression;
			
			float curT = transition.Evaluate(progression);
			
			transform.position = Vector3.Lerp(refPosition,nextPosition,curT);
			
			if (vanishAlpha < 1.0f) {
				
				vanishAlpha -= Time.deltaTime;
				if (vanishAlpha <= 0.0f)
					Destroy(gameObject);
				else {
					SetAlpha(vanishAlpha);
				}
			}
			else if (transform.position.z <= 0.0f) {
				player.NotifyCubeDeath(transform);
				Vanish();
			}
			else {
				float a = Mathf.InverseLerp(settings.spawnDistance,settings.spawnDistance*0.75f,transform.position.z);
				SetAlpha(Mathf.Clamp01(a));
			}
		}
	}
	
	public void Vanish() {
		vanishAlpha -= Time.deltaTime;
	}
	
	void SetAlpha(float val) {
		Color col = gameObject.renderer.material.GetColor("_Color");
		col.a = val;
		gameObject.renderer.material.SetColor("_Color",col);
	}
}
