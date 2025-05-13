using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldormController : Enemy
{
    [SerializeField] float speed;
    Rigidbody2D rb;
    Animator ator;
    SpriteRenderer spriteRenderer;
    Vector2 movementDirection;

    enum EnemyStates { WAITING, MOVING, STUNNED, ANGRY };
    EnemyStates state = EnemyStates.MOVING;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        movementDirection = new Vector2(1f, 1f);
    }

    private void Update()
    {
        switch (state)
        {
            case EnemyStates.WAITING:
                isWaiting();
                break;
            case EnemyStates.MOVING:
                isMoving();
                break;
            case EnemyStates.STUNNED:
                isStunned();
                break;
        }
        ator.SetFloat("X", movementDirection.x);
        ator.SetFloat("Y", movementDirection.y);

        RotateHead();
    }

    public void isWaiting()
    {
        if (player != null)
        {
            state = EnemyStates.MOVING;
        }

        rb.velocity = Vector3.zero;
    }

    public void isMoving()
    {
        EnableLayer();
        rb.velocity = movementDirection * speed;
    }

    public void isStunned()
    {
        DisableLayer();
        StartCoroutine(StopKnockBack());
    }

    public IEnumerator StopKnockBack()
    {
        yield return new WaitForSeconds(0.2f);
        state = EnemyStates.WAITING;
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        Vector2 normal = contact.normal;
        movementDirection = Vector2.Reflect(movementDirection, normal).normalized;
        ator.SetFloat("Y", movementDirection.y);
    }

    public void RotateHead()
    {
        if (movementDirection.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        if (movementDirection.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        if (movementDirection.y > 0)
        {
            spriteRenderer.flipY = true;
        }
        if (movementDirection.y < 0)
        {
            spriteRenderer.flipY = false;
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ator.speed = 0;
            Vector3 awayFromMe = transform.position - collision.transform.position;
            awayFromMe.Normalize();
            rb.AddForce(new Vector2(awayFromMe.x, awayFromMe.y) * knockBackPower, ForceMode2D.Impulse);
            state = EnemyStates.STUNNED;
            GetHurt();
        }
    }
}
