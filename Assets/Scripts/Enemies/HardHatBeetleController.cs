using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardHatBeetleController : Enemy
{
    [SerializeField] float speed;
    [SerializeField] GameObject player;
    [Range(0f, 1f)][SerializeField] float knockBackDistance;
    private bool knockBackActive;

    Rigidbody2D rb;
    enum EnemyStates { WAITING, ATTACKING, GET_HURT }
    EnemyStates state;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        else if (knockBackActive)
        {
            state = EnemyStates.GET_HURT;
        }

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void isGettingHurt()
    {
        if (!knockBackActive)
        {
            state = EnemyStates.WAITING;
        }

        Vector3 awayFromMe = player.transform.position - transform.position;
        awayFromMe.Normalize();
        rb.AddForce(new Vector2(-awayFromMe.x * knockBackDistance, -awayFromMe.y * knockBackDistance), ForceMode2D.Impulse);

        StartCoroutine(StopKnockBack());
    }

    public IEnumerator StopKnockBack()
    {
        yield return new WaitForSeconds(0.2f);
        knockBackActive = false;
        state = EnemyStates.WAITING;
    }
    public override void Attack()
    {
        damage -= 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            knockBackActive = true;
        }
    }
}
