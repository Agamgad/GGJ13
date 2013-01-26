using UnityEngine;
using System.Collections;

public class TempCamControl : MonoBehaviour {
	
	private GlobalSettings settings;
	
	// Use this for initialization
	void Start () {
		settings = GameObject.Find("GlobalObject").GetComponent<GlobalSettings>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 target = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0.0f);
		target *= settings.dodgeRadius;
		
		transform.position = Vector3.MoveTowards(transform.position,target,settings.GetPlayerSpeed()*Time.deltaTime);
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
