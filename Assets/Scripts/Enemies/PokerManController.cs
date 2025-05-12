using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerManController : Enemy
{
    Rigidbody2D rb;
    Animator ator;
    [SerializeField] GameObject effect;
    [SerializeField] float changeDelay;
    //List<PokerManController> otherPokerMans = new List<PokerManController>();
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
        //Debug.Log("Hay " + pokerManControllers.Length + " PokerMans");
        Debug.Log(pokerState);

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
    }

    public void isMoving()
    {
        //rb.velocity = new Vector3(1, 0, 0);

        //StartCoroutine(WalkTime());
    }

    public IEnumerator WalkTime()
    {
        int seconds = Random.Range(1, 5);

        yield return new WaitForSeconds(seconds);
        state = EnemyStates.IDLE;
    }

    public void isStunned()
    {
        
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

            CheckIfSameState();
        }
    }

    private void CheckIfSameState()
    {
        bool allEqual = true;
        bool allStunned = true;

        for (int i = 1; i < pokerManControllers.Length; i++)
        {
            if (!pokerManControllers[i].stunned)
            {
                allStunned = false;
            }
            else
            {
                allStunned = true;
            }
        }

        if(allStunned)
        {
            for (int i = 1; i < pokerManControllers.Length; i++)
            {
                if (pokerManControllers[i].currentPokerState != pokerManControllers[i-1].currentPokerState)
                {
                    allEqual = false;
                }
            }

            if (allEqual)
            {
                for (int i = 0; i < pokerManControllers.Length; i++)
                {
                    pokerManControllers[i].Die();
                }
            }
        }
            
    }

    private void Die()
    {
        Destroy(gameObject);
        Instantiate(effect, transform.position, Quaternion.identity);
    }
}
