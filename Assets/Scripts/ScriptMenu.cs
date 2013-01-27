using UnityEngine;
using System.Collections;

public class ScriptMenu : MonoBehaviour {
	
	public GameObject[] neutral;
	public GameObject[] selected;
	
	public AudioSource sndTxt;
	public AudioSource sndConfirm;
	public AudioSource sndCancel;
	
	public GameObject howTo;
	public GameObject credits;
	
	int cur = 0;
	bool axisWasAtCenter = true;
	
	bool showingImg;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (showingImg) {
			if (Input.GetButton("Cancel")) {
				showingImg = false;
				howTo.SetActive(false);
				credits.SetActive(false);
				sndCancel.Play();
			}
			return;
		}
		
		float vert = Input.GetAxis("Vertical");
		if (axisWasAtCenter)
		{
			if (vert > 0.8f && cur > 0) {
				--cur;
				UpdateCur();
				sndTxt.Play();
			}
			if (vert < -0.8f && cur < 3) {
				++cur;
				UpdateCur();
				sndTxt.Play();
			}
		}
		
		if (Input.GetButtonDown("Confirm")) {
			switch(cur) {
				case 0:
					Application.LoadLevel("test");
					sndConfirm.Play();
					break;
				case 1:
					showingImg = true;
					howTo.SetActive(true);
					sndConfirm.Play();
					break;
				case 2:
					showingImg = true;
					credits.SetActive(true);
					sndConfirm.Play();
					break;
				case 3:
					Application.Quit();
					sndConfirm.Play();
					break;
			}
		}
		
		axisWasAtCenter = (Mathf.Abs(vert) < 0.8f);
	}
	
	void UpdateCur() {
		for(int i=0; i<4; ++i) {
			bool act = (i != cur);
			
			neutral[i].SetActive(act);
			selected[i].SetActive(!act);
		}
	}
}
