using UnityEngine;
using System.Collections;

public class DefaultObstacle : MonoBehaviour {
	
	public float speed=1.0f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos;
		pos = transform.position;
		pos.z -= speed * Time.deltaTime;
		transform.position = pos;
	}
}
