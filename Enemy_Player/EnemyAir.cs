using UnityEngine;

public class EnemyAir : Enemy
{
    protected override void Death()
    {
        Globals.uiStats.ChangeScore(20);
        isDead = true;
        rigid.freezeRotation = false;
        rigid.angularVelocity = 360;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Animator>().SetBool(IsHit, true);

        Destroy(gameObject, 2);
    }
}