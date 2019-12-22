using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {
	public float yOffset;
	GameObject ToFocus;


	// Use this for initialization
	void Start () {
		ToFocus = GameObject.Find ("Ball");

		transform.position = new Vector3 (ToFocus.transform.position.x, ToFocus.transform.position.y + yOffset, transform.position.z);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		transform.position = new Vector3 (ToFocus.transform.position.x, ToFocus.transform.position.y + yOffset, transform.position.z);

	}


}
