using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanUp: MonoBehaviour {
	public LayerMask toHitLayer;
	public float power;
	public float maxDistance;
	// Use this for initialization

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate () {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, maxDistance, toHitLayer);

		//Debug.DrawRay(transform.position, new Vector2(0,100));

		if (hit) {
			float distanceToFan = Vector2.Distance(hit.collider.transform.position, transform.position);
			Vector2 forceToApply = transform.up * ((1 - (distanceToFan / maxDistance)) * power);

			if (distanceToFan < maxDistance)
				hit.collider.attachedRigidbody.AddForce(forceToApply);		//Push object away

			//print("Force: " + forceToApply);
		}
	}
}	
