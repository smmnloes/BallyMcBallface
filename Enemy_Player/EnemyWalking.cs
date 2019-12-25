using UnityEngine;

public class EnemyWalking : Enemy
{
    // Use this for initialization

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }


    protected override void Death()
    {
        Globals.uiStats.ChangeScore(10);
        isDead = true;
        rigid.velocity = new Vector2(0, 0);
        rigid.isKinematic = true;

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().SetBool(IsHit, true);

        Destroy(gameObject, 2);
    }
}