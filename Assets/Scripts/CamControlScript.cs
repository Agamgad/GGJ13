using UnityEngine;
using System.Collections;

public class CamControlScript : MonoBehaviour {
	
	public AnimationCurve transition;
	public Vector3 MoveDirection = Vector3.zero;
	private GlobalSettings settings;
	private Vector3 refCurPos;
	private GameObject GOacceptance;
	private GameObject GOavoidance;
	
	// Use this for initialization
	void Start () {
		settings = GameObject.Find("GlobalObject").GetComponent<GlobalSettings>();
		refCurPos = transform.position;
		GOacceptance = GameObject.FindGameObjectWithTag("Acceptance");
		GOavoidance = GameObject.FindGameObjectWithTag("Avoidance");
	}
	// Update is called once per frame
	void Update () {

		var hor = 0f;
		var ver = 0f;
		float smooth = 4.0f;
		float smoothAngle = 2.0f;
		float tiltAngle = 20.0f;
		//float curT = transition.Evaluate(Mathf.Repeat(Time.time, settings.GetBeat()));
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
	
	public void NotifyCubeDeath(Transform cube) {
		Vector3 dist = cube.position - transform.position;
		dist.z = 0.0f;
		
		if (dist.sqrMagnitude < settings.dodgeRadius * settings.dodgeRadius * 0.25f){
			Debug.Log((int)Time.time+": CUBE ACCEPTED");
			settings.accepted ++;
			if (settings.accepted > 10){
				settings.accepted = 10;
			}
			settings.avoided --;
			if (settings.avoided < 0){
				settings.avoided = 0;
			}
		}else{
			Debug.Log((int)Time.time+": CUBE AVOIDED");
			settings.avoided ++;
			if (settings.avoided > 10){
				settings.avoided = 10;
			}
			settings.accepted --;
			if (settings.accepted < 0){
				settings.accepted = 0;
			}
		}
	}
}
