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

    [SerializeField] Collider2D headCollider;
    [SerializeField] private float distanceThresholdForWiggling = 0.3f;
    [SerializeField] private float maxTimeToWiggleAway = 1.0f;
    float timeTryingToMoveInADir = 0.0f;
    Vector3 lastPosition;
    enum EnemyStates { WAITING, MOVING, STUNNED };
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
        if (Vector3.Distance(lastPosition, transform.position) < distanceThresholdForWiggling)
        {

            timeTryingToMoveInADir += Time.deltaTime;
            if (timeTryingToMoveInADir > maxTimeToWiggleAway)
            {
                DoWiggle();
                timeTryingToMoveInADir = 0;
            }
        }
        else
        {
            lastPosition = transform.position;
            timeTryingToMoveInADir = 0;
        }

        EnableLayer();
        rb.velocity = movementDirection * speed;
    }

    void DoWiggle()
    {
        movementDirection *= -1;
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
        Vector2 newDirection = Vector2.Reflect(movementDirection, normal).normalized;

        if (newDirection == Vector2.zero || newDirection.magnitude < 0.1f)
        {
            newDirection = Random.insideUnitCircle.normalized;
        }

        movementDirection = newDirection;

        transform.position += (Vector3)normal * 0.05f;
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
        if (collision.tag == "Attack")
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.enemyHit);
            GetKnockBack(collision);
            GetHurt();
        }
        if (collision.tag == "Shield")
        {
            GetKnockBack(collision);
        }
    }

    public void GetKnockBack(Collider2D collision)
    {
        state = EnemyStates.STUNNED;
        ator.speed = 0;
        Vector3 awayFromMe = transform.position - collision.transform.position;
        awayFromMe.Normalize();
        rb.AddForce(new Vector2(awayFromMe.x, awayFromMe.y) * knockBackPower, ForceMode2D.Impulse);
    }

    /*private void OnDestroy()
    {
        GameManager.instance.IncrementKills();
        GameManager.instance.IncrementScore(100);
    }*/
}
