using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardHatBeetleController : Enemy
{
    [SerializeField] float speed;
    private float ogAnimSpeed;

    Rigidbody2D rb;
    Animator ator;
    enum EnemyStates { WAITING, ATTACKING, GET_HURT }
    EnemyStates state;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ator = GetComponent<Animator>();
        ogAnimSpeed = ator.speed;
    }
    void Update()
    {
        switch (state)
        {
            case EnemyStates.WAITING:
                isWaiting();
                break;
            case EnemyStates.ATTACKING:
                isAttacking();
                break;
            case EnemyStates.GET_HURT:
                isGettingHurt();
                break;
        }
    }

    public void isWaiting()
    {
        if (player != null)
        {
            state = EnemyStates.ATTACKING;
        }

        rb.velocity = Vector3.zero;
    }
    public void isAttacking()
    {
        if (player == null)
        {
            state = EnemyStates.WAITING;
        }
        /*else if (knockBackActive)
        {
            state = EnemyStates.GET_HURT;
        }*/

        float towardsPlayerX = player.transform.position.x - transform.position.x;
        float towardsPlayerY = player.transform.position.y - transform.position.y;

        rb.velocity = new Vector2(towardsPlayerX, towardsPlayerY).normalized * speed;
    }

    public void isGettingHurt()
    {
        /*if (!knockBackActive)
        {
            state = EnemyStates.WAITING;
        }*/

        StartCoroutine(StopKnockBack());
    }

    public IEnumerator StopKnockBack()
    {
        yield return new WaitForSeconds(0.2f);
        //knockBackActive = false;
        ator.speed = ogAnimSpeed;
        state = EnemyStates.WAITING;
    }
    public override void Attack()
    {
        damage -= 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state == EnemyStates.ATTACKING && collision.tag == "Player")
        {
            ator.speed = 0;
            Vector3 awayFromMe = transform.position - player.transform.position;
            awayFromMe.Normalize();
            rb.AddForce(new Vector2(awayFromMe.x, awayFromMe.y) * knockBackPower, ForceMode2D.Impulse);

            state = EnemyStates.GET_HURT;
        }
    }
}
