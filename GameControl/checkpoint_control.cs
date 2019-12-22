using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint_control : MonoBehaviour {
	public int index;
	public ParticleSystem particles;

	bool reached;

	UIControl uicontrol;
	Animator animator;
	AudioSource audiosource;


	void Start () {
		
		uicontrol = GameObject.Find ("UIControl_Gamestats").GetComponent<UIControl> ();
		animator = GetComponent<Animator> ();
		audiosource = GetComponent<AudioSource> ();

	}

		
	void OnTriggerEnter2D (Collider2D collider) {
		
		if (collider.name == "Ball" &&!reached) {
			
				reached = true;
				particles.Emit (70);
				audiosource.Play ();

			animator.SetBool ("reached", true);

			if (uicontrol.currentCheckpoint < index) {
				uicontrol.currentCheckpoint = index;
			}

		}
	}
}
