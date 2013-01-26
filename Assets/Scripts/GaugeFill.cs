using UnityEngine;
using System.Collections;

public class GaugeFill : MonoBehaviour {
	
	private GlobalSettings settings;
	private GameObject acceptance;
	private GameObject avoidance;
	
	// Use this for initialization
	void Start () {
		settings = GameObject.Find("GlobalObject").GetComponent<GlobalSettings>();
		acceptance = GameObject.FindGameObjectWithTag("Acceptance");
		avoidance = GameObject.FindGameObjectWithTag("Avoidance");
	}
	
	// Update is called once per frame
	void Update () {
		settings.collectance =  settings.collected / (settings.avoided+1) ;
		settings.avoidance = settings.avoided / (settings.collected+1) ;
		
		
		
	}
}
