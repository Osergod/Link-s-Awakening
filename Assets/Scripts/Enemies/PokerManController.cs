using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerManController : Enemy
{
    Rigidbody2D rb;
    Animator ator;
    [SerializeField] GameObject effect;
    //List<PokerManController> otherPokerMans = new List<PokerManController>();
    PokerManController[] pokerManControllers;

    enum EnemyStates { IDLE, MOVE, STUNNED }
    EnemyStates state = EnemyStates.IDLE;

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
        Debug.Log("Hay " + pokerManControllers.Length + " PokerMans");
    }

    public void isIdle()
    {
        rb.velocity = Vector3.zero;
    }

    public void isMoving()
    {
        //rb.velocity = new Vector3(1, 0, 0);

        StartCoroutine(WalkTime());
    }

    public IEnumerator WalkTime()
    {
        int seconds = Random.Range(1, 5);

        yield return new WaitForSeconds(seconds);
        state = EnemyStates.IDLE;
    }

    public void isStunned()
    {
        ator.speed = 0;
        CheckIfSameState();
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            state = EnemyStates.STUNNED;
        }
    }

    private void CheckIfSameState()
    {
        bool result = true;

        for (int i = 0; i < pokerManControllers.Length; i++)
        {
            /*if (pokerManControllers[i])
            {

            }*/

            pokerManControllers[i].Die();
        }

        //return result;
    }

    private void Die()
    {
        Destroy(gameObject);
        Instantiate(effect, transform.position, Quaternion.identity);
    }
}
