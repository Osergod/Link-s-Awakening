using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PokerManController : Enemy
{
    [SerializeField] float speed;
    Rigidbody2D rb;
    Animator ator;
    [SerializeField] float changeDelay;
    [SerializeField] float waitTime;
    PokerManController[] pokerManControllers;
    Vector2 moveDirection;
    SpriteRenderer spriteRenderer;
    private bool stunned;
    private bool changedDirection = false;
    private bool spriteRotating = false;
    private bool isDiamond, isSpade, isClover, isHeart;
    enum EnemyStates { IDLE, MOVE, STUNNED }
    EnemyStates state = EnemyStates.IDLE;

    enum PokerStates { SPADE, DIAMOND, CLOVER, HEART};
    PokerStates pokerState = PokerStates.SPADE;
    PokerStates currentPokerState;

    private void OnDisable()
    {
        state = EnemyStates.IDLE;
        pokerState = PokerStates.SPADE;
        stunned = false;
        spriteRotating = false;
        isHeart = false;
        isSpade = false;
        isDiamond = false;
        isClover = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        pokerManControllers = FindObjectsOfType<PokerManController>();
        moveDirection = Random.insideUnitCircle.normalized;
    }
    
    void Update()
    {
        switch (state)
        {
            case EnemyStates.IDLE:
                isIdle();
                break;
            case EnemyStates.MOVE:
                isMoving();
                break;
            case EnemyStates.STUNNED:
                isStunned();
                break;
        }

        if (!stunned)
        {
            switch (pokerState)
            {
                case PokerStates.SPADE:
                    StartCoroutine(ChangeToDiamond());
                    break;
                case PokerStates.DIAMOND:
                    StartCoroutine(ChangeToClover());
                    break;
                case PokerStates.CLOVER:
                    StartCoroutine(ChangeToHeart());
                    break;
                case PokerStates.HEART:
                    StartCoroutine(ChangeToSpade());
                    break;
            }
        }

        StartCoroutine(RotateSprite());
    }

    public IEnumerator RotateSprite()
    {
        float spriteFlippingDelay = 0.2f;
        if (!spriteRotating)
        {
            spriteRotating = true;
            while (state == EnemyStates.MOVE && !stunned)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
                yield return new WaitForSeconds(spriteFlippingDelay);
            }
            spriteRotating = false;
        }
    }
    public IEnumerator ChangeToDiamond()
    {
        yield return new WaitForSeconds(changeDelay);
        if (!stunned && !isDiamond)
        {
            isSpade = false;
            isClover = false;
            isHeart = false;
            isDiamond = true;
            pokerState = PokerStates.DIAMOND;
            ator.SetTrigger("ToDiamond");
        }
    }

    public IEnumerator ChangeToClover()
    {
        yield return new WaitForSeconds(changeDelay);
        if (!stunned && !isClover)
        {
            isHeart = false;
            isSpade = false;
            isDiamond = false;
            isClover = true;
            pokerState = PokerStates.CLOVER;
            ator.SetTrigger("ToClover");
        }
    }

    public IEnumerator ChangeToHeart()
    {
        yield return new WaitForSeconds(changeDelay);
        if (!stunned && !isHeart)
        {
            isClover = false;
            isSpade = false;
            isDiamond = false;
            isHeart = true;
            pokerState = PokerStates.HEART;
            ator.SetTrigger("ToHeart");
        }
    }

    public IEnumerator ChangeToSpade()
    {
        yield return new WaitForSeconds(changeDelay);
        if (!stunned && !isSpade)
        {
            isHeart = false;
            isDiamond = false;
            isClover = false;
            isSpade = true;
            pokerState = PokerStates.SPADE;
            ator.SetTrigger("ToSpade");
        }
    }

    public void isIdle()
    {
        moveDirection = Vector2.zero;
        changedDirection = false;
        state = EnemyStates.MOVE;
    }

    public void isMoving()
    {
        if (!stunned)
        {
            if (!changedDirection)
            {
                moveDirection = Random.insideUnitCircle.normalized;
                changedDirection = true;
                rb.velocity = moveDirection * speed;
                StartCoroutine(WalkTime());
            }
        }
    }

    public IEnumerator WalkTime()
    {
        yield return new WaitForSeconds(Random.Range(1, 5));
        state = EnemyStates.IDLE;
    }

    public void isStunned()
    {
        bool allEqual = true;

        if (pokerManControllers[0].stunned && pokerManControllers[1].stunned && pokerManControllers[2].stunned)
        {
            for (int i = 1; i < pokerManControllers.Length; i++)
            {
                if (pokerManControllers[i].currentPokerState != pokerManControllers[i - 1].currentPokerState)
                {
                    allEqual = false;
                }
            }

            if (allEqual)
            {
                StartCoroutine(KillAll());
            }
            else
            {
                StartCoroutine(Reactivate());
            }
        }
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Attack")
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.enemyHit);
            moveDirection = Vector2.zero;
            rb.velocity = Vector2.zero;
            stunned = true;
            currentPokerState = pokerState;
            state = EnemyStates.STUNNED;
        }
    }

    public IEnumerator Reactivate()
    {
        yield return new WaitForSeconds(waitTime);
            
        AudioManager.instance.PlaySFX(AudioManager.instance.error);
        for (int i = 0; i < pokerManControllers.Length; i++)
        {
            pokerManControllers[i].stunned = false;
        }
    }

    public IEnumerator KillAll()
    {
        yield return new WaitForSeconds(waitTime);
        for (int i = 0; i < pokerManControllers.Length; i++)
        {
            pokerManControllers[i].Die();
        }
    }

    /*private void OnDestroy()
    {
        GameManager.instance.IncrementKills();
        GameManager.instance.IncrementScore(200);
    }*/
}
