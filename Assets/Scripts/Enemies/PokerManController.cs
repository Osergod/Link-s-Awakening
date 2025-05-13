using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerManController : Enemy
{
    Rigidbody2D rb;
    Animator ator;
    [SerializeField] GameObject effect;
    [SerializeField] float changeDelay;
    [SerializeField] float waitTime;
    PokerManController[] pokerManControllers;
    private bool stunned;

    enum EnemyStates { IDLE, MOVE, STUNNED }
    EnemyStates state = EnemyStates.IDLE;

    enum PokerStates { SPADE, DIAMOND, CLOVER, HEART};
    PokerStates pokerState = PokerStates.SPADE;
    PokerStates currentPokerState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ator = GetComponent<Animator>();
        pokerManControllers = FindObjectsOfType<PokerManController>();
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
    }

    public IEnumerator ChangeToDiamond()
    {
        yield return new WaitForSeconds(changeDelay);
        if (!stunned)
        {
            pokerState = PokerStates.DIAMOND;
            ator.SetTrigger("ToDiamond");
        }
    }

    public IEnumerator ChangeToClover()
    {
        yield return new WaitForSeconds(changeDelay);
        if (!stunned)
        {
            pokerState = PokerStates.CLOVER;
            ator.SetTrigger("ToClover");
        }
    }

    public IEnumerator ChangeToHeart()
    {
        yield return new WaitForSeconds(changeDelay);
        if (!stunned)
        {
            pokerState = PokerStates.HEART;
            ator.SetTrigger("ToHeart");
        }
    }

    public IEnumerator ChangeToSpade()
    {
        yield return new WaitForSeconds(changeDelay);
        if (!stunned)
        {
            pokerState = PokerStates.SPADE;
            ator.SetTrigger("ToSpade");
        }
    }

    public void isIdle()
    {
        rb.velocity = Vector3.zero;
        //state = EnemyStates.MOVE;
    }

    public void isMoving()
    {
        rb.velocity = new Vector2(1, 0);
        StartCoroutine(WalkTime());
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
        //CheckIfSameState();
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            stunned = true;
            currentPokerState = pokerState;
            state = EnemyStates.STUNNED;
        }
    }

    public IEnumerator Reactivate()
    {
        yield return new WaitForSeconds(waitTime);
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

    private void Die()
    {
        Destroy(gameObject);
        Instantiate(effect, transform.position, Quaternion.identity);
    }
}
