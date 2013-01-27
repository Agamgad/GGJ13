using UnityEngine;
using System.Collections;

public class GaugeFill : MonoBehaviour {
	
	private GlobalSettings settings;
	public GameObject GOacceptance1;
	public GameObject GOacceptance2;
	public GameObject GOacceptance3;
	public GameObject GOavoidance1;
	public GameObject GOavoidance2;
	public GameObject GOavoidance3;
	private int curAvoidGauge = 0;
	private int curAcceptGauge = 0;
	
	// Use this for initialization
	void Start () {
		settings = GameObject.Find("GlobalObject").GetComponent<GlobalSettings>();
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
			curAcceptGauge ++;
			settings.toNextStageAtEnd = true;			
			settings.acceptance = limit;
				
		}
		if (settings.avoidance > limit){
			settings.moai.Play();
			curAvoidGauge ++;
			settings.toNextStageAtEnd = true;
			settings.avoidance = limit;
		}
		
		switch (curAcceptGauge) {
		case 0:
			GOacceptance1.transform.localScale = new Vector3(0.055f,0.1f,(settings.acceptance)*0.005f);
			//GOacceptance1.transform.position = new Vector3(0.0f,(settings.acceptance)*0.01f,0.0f);
			break;
		case 1:
			GOacceptance2.transform.localScale = new Vector3(0.055f,0.1f,(settings.acceptance-20)*0.0025f);
			//GOacceptance2.transform.position = new Vector3(0.0f,(settings.acceptance-20)*0.005f,0.0f);
			break;
		case 2:
			GOacceptance3.transform.localScale = new Vector3(0.055f,0.1f,(settings.acceptance-60)*0.0025f);
			//GOacceptance3.transform.position = new Vector3(0.0f,(settings.acceptance-60)*0.005f,0.0f);
			break;
		};
		
		switch (curAvoidGauge) {
		case 0:
			GOavoidance1.transform.localScale = new Vector3(0.0375f,0.1f,(settings.avoidance)*0.005f);
			//GOavoidance1.transform.position = new Vector3(0.0f,(settings.acceptance)*0.01f,0.0f);
			break;
		case 1:
			GOavoidance2.transform.localScale = new Vector3(0.0375f,0.1f,(settings.avoidance-20)*0.0025f);
			//GOavoidance2.transform.position = new Vector3(0.0f,(settings.avoidance-20)*0.005f,0.0f);
			break;
		case 2:
			GOavoidance3.transform.localScale = new Vector3(0.0375f,0.1f,(settings.avoidance-60)*0.005f);
			//GOavoidance3.transform.position = new Vector3(0.0f,(settings.avoidance-60)*0.005f,0.0f);
			break;
		};

	}
}
