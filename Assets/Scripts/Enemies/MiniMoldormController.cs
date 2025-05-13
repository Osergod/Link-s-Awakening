using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMoldormController : Enemy
{
    [SerializeField] float speed;
    Rigidbody2D rb;
    Animator ator;
    SpriteRenderer spriteRenderer;
    Vector2 movementDirection;

    enum EnemyStates { IDLE, MOVING };
    EnemyStates state = EnemyStates.MOVING;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChooseRandomDirection();
    }

    private void Update()
    {
        switch (state)
        {
            case EnemyStates.IDLE:
                isIdle();
                break;
            case EnemyStates.MOVING:
                isMoving();
                break;
        }

        rb.velocity = movementDirection * speed;

        RotateHead();
    }

    public void isIdle()
    {

    }

    public void isMoving()
    {
        
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

    public void ChooseRandomDirection()
    {
        float angle = Random.Range(0, 360f);
        movementDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
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
    }

    public float GetSpeed()
    {
        return speed;
    }
}
