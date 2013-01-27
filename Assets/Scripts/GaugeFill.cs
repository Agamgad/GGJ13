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
		
		int limit = 0;
		settings.acceptance =  settings.accepted;
		settings.avoidance = settings.avoided;
		
		switch (settings.currentStage) {
		case 0:
			limit = 20;
			break;
		case 1:
			limit = 40;
			break;
		case 2:
			limit = 60;
			break;
		case 3:
			limit = 80;
			break;
		case 4:
			limit = 100;
			break;
		};
		
		if (settings.acceptance > limit){
			settings.moai.Play();
			settings.toNextStageAtEnd = true;			
			settings.acceptance = limit;
				
		}
		if (settings.avoidance > limit){
			settings.moai.Play();
			settings.toNextStageAtEnd = true;
			settings.avoidance = limit;
		}
		
		if (settings.acceptance < 0){
			settings.acceptance = 0;
		}
		if (settings.avoidance < 0){
			settings.avoidance = 0;
		}
	//	Debug.Log(settings.accepted + " Mo'Ai Accepted, " + settings.avoided + " Mo'Ai avoided");
	//	Debug.Log("For an acceptance ratio of " + settings.acceptance*0.1f);
		
		GOacceptance.transform.localScale = new Vector3(0.06f,0.1f,settings.acceptance*0.005f);
		GOavoidance.transform.localScale = new Vector3(0.06f,0.1f,settings.avoidance*0.005f);
	}
}
