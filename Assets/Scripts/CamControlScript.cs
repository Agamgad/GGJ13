using UnityEngine;
using System.Collections;

public class CamControlScript : MonoBehaviour {
	
	public AnimationCurve transition;
	public Vector3 MoveDirection = Vector3.zero;
	private GlobalSettings settings;
	private GUITexture pauseText;
	private Vector3 refCurPos;
	
	// Use this for initialization
	void Start () {
		settings = GameObject.Find("GlobalObject").GetComponent<GlobalSettings>();
		pauseText = GameObject.Find("PauseTexture").GetComponent<GUITexture>();
		refCurPos = transform.position;
	}
	// Update is called once per frame
	void Update () {
		
		if (settings.isPaused){
			if (Input.GetButtonDown ("Pause")) {
				settings.isPaused = false;
				pauseText.enabled = false;
				settings.didge.Play();
				settings.music.Play();
				Time.timeScale = 1.0f;
			}
		} else {
			var hor = 0f;
			var ver = 0f;
			float smooth = 4.0f;
			float smoothAngle = 2.0f;
			float tiltAngle = 20.0f;
			float curT = transition.Evaluate(settings.GetBeatProgression(4));
			curT = curT*2.0f-1.0f;
			
			// GESTION DE L'INPUT
	   		if(Input.GetAxis("Horizontal") < 0 ){
				hor = Mathf.Floor(Input.GetAxis("Horizontal"));
			} else {
				hor = Mathf.Ceil(Input.GetAxis("Horizontal"));
			}
			if(Input.GetAxis("Vertical") < 0 ){
				ver = Mathf.Floor(Input.GetAxis("Vertical"));
			} else {
				ver = Mathf.Ceil(Input.GetAxis("Vertical"));
			}
			
			hor *= settings.dodgeRadius;
			ver *= settings.dodgeRadius;
			
			refCurPos = Vector3.MoveTowards(refCurPos , new Vector3(hor, ver , 0.0f ), Time.deltaTime * smooth * settings.dodgeRadius);
			transform.position = refCurPos;
			
			if (Input.GetButtonDown ("Pause")) {
				settings.isPaused = true;
				pauseText.enabled = true;
				settings.didge.Pause();
				settings.music.Pause();
				settings.gapaga.Play();
		        Time.timeScale = 0.0f;
			}
			
			// GESTION DE LA CAMERA
			// Oscillation
			Vector3 refCurCamPos = transform.position;
			refCurCamPos.y += curT * settings.dodgeRadius * 0.2f;
			transform.position = refCurCamPos;
			
			Quaternion refCurCamRot = transform.rotation;
			refCurCamRot.x += curT * 0.01f;
			transform.rotation = refCurCamRot;
			
			// Angle
			float tiltAroundY = Input.GetAxis("Horizontal") * tiltAngle;
	    	float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;
	    	var target = Quaternion.Euler (tiltAroundX, -tiltAroundY, 0.0f);
	    	
	    	transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smoothAngle);
		}
	}
	
	public void NotifyCubeDeath(Transform cube) {
		Vector3 dist = cube.position - transform.position;
		dist.z = 0.0f;
		
		if (dist.sqrMagnitude < settings.dodgeRadius * settings.dodgeRadius * 0.25f){
			Debug.Log((int)Time.time+": CUBE ACCEPTED");
			settings.accepted ++;
			settings.avoided --;
			
		}else{
			Debug.Log((int)Time.time+": CUBE AVOIDED");
			settings.avoided ++;
			settings.accepted --;
		}
	}
}
