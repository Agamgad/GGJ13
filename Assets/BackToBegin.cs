using UnityEngine;
using System.Collections;

public class BackToBegin : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetButtonDown("Cancel"))
			Application.LoadLevel("Menu");
	}
}
