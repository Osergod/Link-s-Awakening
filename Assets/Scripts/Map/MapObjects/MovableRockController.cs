using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableRockController : ActionableMapObject
{
    [SerializeField] GameObject targetSpot;
    [SerializeField] ActionableMapObject targetActivationObject;
    [SerializeField] float activationDelay;
    [SerializeField] float movingSpeed;

    private bool playerPushing;
    private bool playerPushed;
    private bool isActive = false;

    enum RockStates { STILL, MOVABLE, MOVING };
    RockStates states = RockStates.MOVABLE;
    private void Update()
    {
        switch (states)
        {
            case RockStates.STILL:
                IsStill();
                break;
            case RockStates.MOVABLE:
                IsMovable();
                break;
            case RockStates.MOVING:
                IsMoving();
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerPushing = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && LinkController.instance.GetHorizontalMovement() != 0)
        {
            playerPushing = true;
            StartCoroutine(MoveRock(collision));
        }
    }
    public override void Activate()
    {
        if (!isActive)
        {
            isActive = true;
            AudioManager.instance.PlaySFX(AudioManager.instance.rockPush);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetSpot.transform.position, movingSpeed * Time.deltaTime);

        if (transform.position == targetSpot.transform.position)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.roomSolved);
        }
    }

    public IEnumerator MoveRock(Collider2D collision)
    {
        yield return new WaitForSeconds(activationDelay);
        if (playerPushing && LinkController.instance.GetHorizontalMovement() != 0)
        {
            playerPushed = true;
        }
    }

    public void IsStill()
    {
        if (transform.position != targetSpot.transform.position)
        {
            states = RockStates.MOVABLE;
        }
        targetActivationObject.Activate();
    }

    public void IsMovable()
    {
        if (playerPushed)
        {
            states = RockStates.MOVING;
        }
    }
    public void IsMoving()
    {
        if (transform.position == targetSpot.transform.position)
        {
            states = RockStates.STILL;
        }

        Activate();
    }
}
