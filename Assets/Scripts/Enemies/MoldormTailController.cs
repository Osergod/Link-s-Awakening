using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldormTailController : MonoBehaviour
{
    [SerializeField] float followSpeed;
    [SerializeField] GameObject effect;
    MoldormController head;
    MoldormBodyController body;
    Rigidbody2D rb;
    Animator ator;
    Vector2 toBody;
    bool movementStopped = false;
    enum TailStates { STILL, MOVING};
    TailStates state;
    void Start()
    {
        ator = GetComponent<Animator>();
        head = GetComponentInParent<MoldormController>();
        body = GetComponentInParent<MoldormBodyController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        switch (state)
        {
            case TailStates.STILL:
                isStill();
                break;
            case TailStates.MOVING:
                isMoving();
                break;
        }

        ator.SetFloat("X", toBody.x);
        ator.SetFloat("Y", toBody.y);
    }

    public void isStill()
    {
        if (!movementStopped)
        {
            state = TailStates.MOVING;
        }

        rb.velocity = Vector2.zero;
    }
    public void isMoving()
    {
        if (movementStopped)
        {
            state = TailStates.STILL;
        }

        toBody = body.transform.position - transform.position;
        if (transform.position != body.transform.position)
        {
            rb.velocity = toBody * head.GetSpeed() * followSpeed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !head.GetGotHurt())
        {
            head.SetGotHurt(true);
        }
    }

    public void StopMovement()
    {
        movementStopped = true;
    }

    public void ResumeMovement()
    {
        movementStopped = false;
    }

    public void Eliminate()
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
