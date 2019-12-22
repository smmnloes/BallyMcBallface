using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour {
	public GameObject Layer1;
	public GameObject Layer2;
	public GameObject Layer3;
	public GameObject Layer4;
	public GameObject Layer5;

	GameObject player;

	float playerSpeed;

	public float speedfactorLayer1;
	public float speedfactorLayer2;
	public float speedfactorLayer3;
	public float speedfactorLayer4;
	public float speedfactorLayer5;

	// Use this for initialization
	void Start () {

		player = GameObject.Find ("Ball");

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		playerSpeed = player.GetComponent<Rigidbody2D> ().velocity.x;


		Layer1.GetComponent<Renderer> ().material.mainTextureOffset += new Vector2(playerSpeed * speedfactorLayer1 * Time.deltaTime / 1000,0);
		Layer2.GetComponent<Renderer> ().material.mainTextureOffset += new Vector2(playerSpeed * speedfactorLayer2 * Time.deltaTime / 1000,0);
		Layer3.GetComponent<Renderer> ().material.mainTextureOffset += new Vector2(playerSpeed * speedfactorLayer3 * Time.deltaTime / 1000,0);
		Layer4.GetComponent<Renderer> ().material.mainTextureOffset += new Vector2(playerSpeed * speedfactorLayer4 * Time.deltaTime / 1000,0);
		Layer5.GetComponent<Renderer> ().material.mainTextureOffset += new Vector2(playerSpeed * speedfactorLayer5 * Time.deltaTime / 1000,0);
	}
}
