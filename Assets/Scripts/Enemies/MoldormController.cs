using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoldormController : Enemy
{
    [SerializeField] float speed;
    Rigidbody2D rb;
    Animator ator;
    SpriteRenderer spriteRenderer;
    Vector2 movementDirection;
    Vector2 lastMovementDirection;
    MoldormTailController tail;
    private bool gotHurt = false;
    private bool isRecovering = false;
    MoldormBodyController[] bodyParts;
    enum EnemyStates { WAITING, MOVING, STUNNED, ANGRY };
    EnemyStates state = EnemyStates.MOVING;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        tail = GetComponentInChildren<MoldormTailController>();
        movementDirection = new Vector2(1f, 1f);
        bodyParts = GetComponentsInChildren<MoldormBodyController>();
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
            case EnemyStates.ANGRY:
                isAngry();
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
        if (gotHurt)
        {
            state = EnemyStates.STUNNED;
        }
        rb.velocity = movementDirection * speed;
    }

    public void isStunned()
    {
        if (!isRecovering)
        {
            isRecovering = true;
            lastMovementDirection = movementDirection;
            movementDirection = Vector2.zero;
            ator.SetBool("isHurt", true);
            rb.velocity = Vector2.zero;
            tail.StopMovement();

            for (int i = 0; i < bodyParts.Length; i++)
            {
                bodyParts[i].StopMovement();
            }

            StartCoroutine(Recover());
        }
    }

    public IEnumerator Recover()
    {
        yield return new WaitForSeconds(1);
        tail.ResumeMovement();

        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].ResumeMovement();
        }
        movementDirection = lastMovementDirection;
        state = EnemyStates.ANGRY;
        gotHurt = false;
        isRecovering = false;
        ator.SetBool("isHurt", false);
    }
    public void isAngry()
    {
        rb.velocity = movementDirection * (speed * 2);
        StartCoroutine(CalmDown());
    }

    public IEnumerator CalmDown()
    {
        yield return new WaitForSeconds(3);
        state = EnemyStates.MOVING;
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gotHurt)
        {
            ContactPoint2D contact = collision.contacts[0];
            Vector2 normal = contact.normal;
            movementDirection = Vector2.Reflect(movementDirection, normal).normalized;

            StartCoroutine(ChangeDirection());
        }
    }

    public IEnumerator ChangeDirection()
    {
        float angleVariation = Random.Range(-45f, 45f);
        Quaternion rotation = Quaternion.Euler(0, 0, angleVariation);

        yield return new WaitForSeconds(0.5f);
        if (Random.Range(0,50) > 25 && !gotHurt)
        {
            movementDirection = rotation * movementDirection;
        }
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

    public void SetGotHurt(bool gotHurt)
    {
        if (state != EnemyStates.STUNNED)
        {
            this.gotHurt = gotHurt;
        }
        
    }

    public bool GetGotHurt()
    {
        return gotHurt;
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
        state = EnemyStates.STUNNED;
    }
}
