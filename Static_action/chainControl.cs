using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class chainControl : MonoBehaviour {	
	public bool deadly;

	Rigidbody2D rigid;

	Player playerControl;
	GameObject playerObject;


	void Start () {

		playerObject = GameObject.Find ("Ball");
		playerControl = playerObject.GetComponent<Player> ();
		rigid = GetComponent<Rigidbody2D> ();

	}
	

	void Update () {
		
		if (deadly) {
			rigid.AddForce (new Vector2 (Mathf.Sign (rigid.velocity.x) * 2, 0), ForceMode2D.Force);		//keep swinging
		}		
	}


	void OnTriggerEnter2D (Collider2D collider) {
		
	
		if (collider.gameObject == playerObject) {	
			if (deadly) {

				playerControl.Death ();
			
			} else {
			
				playerControl.Attach (transform);

			}
		
		}
	}



}
