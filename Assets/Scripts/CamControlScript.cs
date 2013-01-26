using UnityEngine;
using System.Collections;

public class CamControlScript : MonoBehaviour {
	
	public Vector3 MoveDirection = Vector3.zero;
	private GlobalSettings settings;
	
	// Use this for initialization
	void Start () {
		settings = GameObject.Find("GlobalObject").GetComponent<GlobalSettings>();
	}
	// Update is called once per frame
	void Update () {
		var hor = 0f;
		var ver = 0f;
		float smooth = 2.0f;
		float tiltAngle = 30.0f;
		
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
		
		transform.position = Vector3.Lerp(transform.position , new Vector3(hor, ver , 0.0f ), Time.deltaTime * smooth);
		//print (ADPosition.x);
		
		float tiltAroundY = Input.GetAxis("Horizontal") * tiltAngle;
    	float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;
    	var target = Quaternion.Euler (tiltAroundX, -tiltAroundY, 0.0f);
    	// Dampen towards the target rotation
    	transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth); 
	}
	
	public void NotifyCubeDeath(Transform cube) {
		Vector3 dist = cube.position - transform.position;
		dist.z = 0.0f;
		
		if (dist.sqrMagnitude < settings.dodgeRadius * settings.dodgeRadius * 0.25f)
			Debug.Log((int)Time.time+": CUBE ACCEPTED");
		else
			Debug.Log((int)Time.time+": CUBE AVOIDED");
	}
	
}
