using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectableLife : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (0, 1, 0) * Mathf.Sin (Time.time*5)/200;
		
	}
}
