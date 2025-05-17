using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayDoorController : MonoBehaviour
{

    private Animator ator;

    private void Start()
    {
        ator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ator.SetTrigger("PlayerCrossed");
            StartCoroutine(MovePlayer(collision));
        }   
    }

    public IEnumerator MovePlayer(Collider2D collision)
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.oneWayDoor);
        LinkController.instance.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        LinkController.instance.GetComponent<Rigidbody2D>().transform.Translate(new Vector3(0, 2, 0));
        LinkController.instance.GetComponent<SpriteRenderer>().enabled = true;

    }
}
