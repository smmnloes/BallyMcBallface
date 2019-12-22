using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected bool isDead;
    public int upForceWhenHit;
    public int direction = -1;
    public bool killable = true;
    protected Rigidbody2D rigid;

    public float horizontalSpeed;

    protected static readonly int IsHit = Animator.StringToHash("isHit");

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        if (isDead) return;
        
        var absVelocityX = Mathf.Abs(rigid.velocity.x);

        if (absVelocityX < 0.01) //Avoid getting stuck
            direction *= -1;

        rigid.velocity = new Vector2(horizontalSpeed * direction, 0);
        transform.rotation = Quaternion.AngleAxis(rigid.velocity.x < 0 ? 0 : 180, Vector3.up);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Change Direction/Kill

        if (isDead) return;
        foreach (var cPoint in collision.contacts)
        {
            var colliderHalfHeight = GetComponent<Collider2D>().bounds.size.y / 2;
            var cPointCollider = cPoint.collider;
            var cPointY = cPoint.point.y;
            var positionY = transform.position.y;

            if (cPointY - positionY > colliderHalfHeight * 0.5 && killable)
            {
                //Hit from Top
                if (cPointCollider.gameObject != Globals.ball || Globals.playerControl.isDead) continue;

                //Hit by Player
                Death();
                cPointCollider.attachedRigidbody.AddForce(new Vector2(0,
                    upForceWhenHit)); //Apply upforce to player
            }
            else
            {
                var colliderHalfWidth = GetComponent<Collider2D>().bounds.size.x / 2;
                var cPointX = cPoint.point.x;
                var positionX = transform.position.x;
                if ((cPointX - positionX) < -colliderHalfWidth * 0.7)
                {
                    direction = 1;
                }

                if ((cPointX - positionX) > colliderHalfWidth * 0.7)
                {
                    direction = -1;
                }

                if (cPointCollider.gameObject == Globals.ball)
                    //Kill Player
                    Globals.playerControl.Death();
            }
        }
    }

    protected virtual void Death()
    {
    }
}