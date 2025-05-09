using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableRockController : ActionableMapObject
{
    [SerializeField] GameObject targetSpot;
    private bool isOnTarget = false;

    private void Update()
    {
        if (transform.position == targetSpot.transform.position)
        {
            isOnTarget = true;
        }
        else if (transform.position != targetSpot.transform.position)
        {
            isOnTarget = false;
        }
    }

    public override void Activate()
    {
        


        /*if (!isOnTarget)
        {
            transform.position += towardsTarget * 0.01f;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isOnTarget)
        {
            Activate();
        }
    }
}
