using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointControl : MonoBehaviour {
	public int index;
	public ParticleSystem particles;

	private bool _reached;

	private Animator _animator;
	private AudioSource _audiosource;
	private static readonly int Reached = Animator.StringToHash("reached");


	void Start () {
		_animator = GetComponent<Animator> ();
		_audiosource = GetComponent<AudioSource> ();
	}

		
	void OnTriggerEnter2D (Collider2D col) {
		
		if (col.name == "Ball" &&!_reached) {
			
				_reached = true;
				particles.Emit (70);
				_audiosource.Play ();

			_animator.SetBool (Reached, true);

			if (Globals.uiStats.currentCheckpoint < index) {
				Globals.uiStats.currentCheckpoint = index;
			}

		}
	}
}
