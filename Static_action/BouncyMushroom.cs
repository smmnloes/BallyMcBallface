using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyMushroom : MonoBehaviour
{
    Animator animator;
    GameObject ball;
    public int upForce;
    public CircleCollider2D JumpCollider;

    AudioSource audiosource;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        ball = GameObject.Find("Ball");
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        foreach (ContactPoint2D cpoint in col.contacts)
        {
            if (cpoint.collider.gameObject == ball && cpoint.otherCollider == JumpCollider)
            {
                animator.SetTrigger("isHit");
                cpoint.collider.GetComponent<Rigidbody2D>().AddForce(Vector2.up * upForce);
                if (!audiosource.isPlaying) audiosource.Play();
            }
        }
    }
}