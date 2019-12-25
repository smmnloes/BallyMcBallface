using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetController : MonoBehaviour {
	Vector2[] positions;


	void Start () {

		positions = new Vector2[transform.childCount];

		for (int i = 0; i < transform.childCount; i++) {
			positions [i] = transform.GetChild (i).position;	
		}

	}


	public void Reset() {
	
		for (int i = 0; i < transform.childCount; i++) {

			Transform currentChild = transform.GetChild (i);

			Rigidbody2D currentChildRigid = currentChild.GetComponent<Rigidbody2D> ();

			currentChildRigid.velocity = Vector3.zero;
			currentChild.rotation = Quaternion.Euler(0,0,0);
			currentChildRigid.angularVelocity = 0;

			currentChild.position = positions [i];


		
		}
	
	}
}
