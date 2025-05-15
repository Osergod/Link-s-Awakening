using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class MoldormController : Enemy
{
    [SerializeField] float speed;
    [SerializeField] float bodyDestructionDelay;
    [SerializeField] float secondsToRecover;
    [SerializeField] float secondsToCalmDown;
    [SerializeField] float angleVariationScale;
    Rigidbody2D rb;
    Animator ator;
    SpriteRenderer spriteRenderer;
    MoldormBodyController[] bodyParts;
    Vector2 movementDirection, lastMovementDirection;
    MoldormTailController tail;
    private bool gotHurt = false;
    private bool isRecovering = false;
    private bool isDead = false;
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
        RotateHead();
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

            StopBodyMovement();

            if (!isDead)
            {
                StartCoroutine(Recover());
            }
        }
    }

    public IEnumerator Recover()
    {
        yield return new WaitForSeconds(secondsToRecover);
        tail.ResumeMovement();

        ResumeBodyMovement();
        movementDirection = lastMovementDirection;
        state = EnemyStates.ANGRY;
        gotHurt = false;
        isRecovering = false;
        ator.SetBool("isHurt", false);
    }
    public void isAngry()
    {
        RotateHead();
        rb.velocity = movementDirection * (speed * 2);
        StartCoroutine(CalmDown());
    }

    public IEnumerator CalmDown()
    {
        yield return new WaitForSeconds(secondsToCalmDown);
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
            Vector2 newDirection = Vector2.Reflect(movementDirection.normalized, contact.normal);
            movementDirection = newDirection.normalized;
            Debug.Log("Collided");
            //StartCoroutine(ChangeDirection());
        }
    }
    public IEnumerator ChangeDirection()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f)).normalized;

        yield return new WaitForSeconds(0.1f);
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

    public void StopBodyMovement()
    {
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].StopMovement();
        }
    }

    public void ResumeBodyMovement()
    {
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].ResumeMovement();
        }
    }
    public override void Die()
    {
        isDead = true;
        movementDirection = Vector2.zero;
        ator.SetBool("isHurt", true);

        StopBodyMovement();

        tail.StopMovement();

        StartCoroutine(DestroyBody());
    }

    public IEnumerator DestroyBody()
    {
        float effectDistance = 1;
        float effectDelay = 0.1f;
        float effectSeparation = 1.5f;
        float effectToCenter = 0.05f;
        int numEffects = 15;

        yield return new WaitForSeconds(bodyDestructionDelay);
        tail.Eliminate();
        
        for (int i = bodyParts.Length; i > 0; i--)
        {
            yield return new WaitForSeconds(bodyDestructionDelay);
            bodyParts[i - 1].Eliminate();
        }

        yield return new WaitForSeconds(bodyDestructionDelay);

        for (int i = 0; i < numEffects; i++)
        {
            float angle = (i * effectSeparation) * Mathf.PI * 2 / numEffects;
            float x = Mathf.Cos(angle) * effectDistance;
            float y = Mathf.Sin(angle) * effectDistance;

            yield return new WaitForSeconds(effectDelay);
            Vector3 explosionPosition = new Vector3(x, y, 0) + transform.position;
            Instantiate(effect, explosionPosition, Quaternion.identity);
            effectDistance -= effectToCenter;
        }

        Destroy(gameObject);
    }
}
