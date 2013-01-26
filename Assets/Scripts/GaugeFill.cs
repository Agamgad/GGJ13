using UnityEngine;
using System.Collections;

public class GaugeFill : MonoBehaviour {
	
	private GlobalSettings settings;
	private GameObject GOacceptance;
	private GameObject GOavoidance;
	
	// Use this for initialization
	void Start () {
		settings = GameObject.Find("GlobalObject").GetComponent<GlobalSettings>();
		GOacceptance = GameObject.FindGameObjectWithTag("Acceptance");
		GOavoidance = GameObject.FindGameObjectWithTag("Avoidance");
	}
	
	// Update is called once per frame
	void Update () {
		
		settings.acceptance =  settings.accepted - (settings.avoided+1) ;
		if (settings.acceptance < 0){
			settings.acceptance = 0;
		}
		if (settings.acceptance > 10){
			settings.acceptance = 10;
		}
		settings.avoidance = settings.avoided - (settings.accepted+1) ;
		if (settings.avoidance < 0){
			settings.avoidance = 0;
		}
		if (settings.avoidance > 10){
			settings.avoidance = 10;
		}
//		Debug.Log(settings.accepted + " Mo'Ai Accepted, " + settings.avoided + " Mo'Ai avoided");
//		Debug.Log("For an acceptance ratio of " + settings.acceptance*0.1f);
		
		GOacceptance.transform.localScale = new Vector3(0.1f,0.1f,settings.acceptance*0.01f);
		GOavoidance.transform.localScale = new Vector3(0.1f,0.1f,settings.avoidance*0.01f);
	}
}
