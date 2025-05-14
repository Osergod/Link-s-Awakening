using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MoldormBodyController : MonoBehaviour
{
    [SerializeField] float followSpeed;
    MoldormController head;
    Rigidbody2D rb;
    private bool movementStopped;
    Animator ator;
    Vector2 toHead;
    enum BodyStates { STILL, MOVING };
    BodyStates state;
    void Start()
    {
        head = GetComponentInParent<MoldormController>();
        rb = GetComponent<Rigidbody2D>();
        ator = GetComponent<Animator>();
        toHead = head.transform.position - transform.position;
    }

    private void Update()
    {

        switch (state)
        {
            case BodyStates.STILL:
                isStill();
                break;
            case BodyStates.MOVING:
                isMoving();
                break;
        }
    }

    public void isStill()
    {
        if (!movementStopped)
        {
            state = BodyStates.MOVING;
        }

        rb.velocity = Vector2.zero;
    }
    public void isMoving()
    {
        if (movementStopped)
        {
            state = BodyStates.STILL;
        }

        toHead = head.transform.position - transform.position;
        if (transform.position != head.transform.position)
        {
            rb.velocity = toHead * head.GetSpeed() * followSpeed;
        }
    }

    public void StopMovement()
    {
        ator.SetBool("isHurt", true);
        movementStopped = true;
    }

    public void ResumeMovement()
    {
        ator.SetBool("isHurt", false);
        movementStopped = false;
    }
}
