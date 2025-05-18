using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorController : ActionableMapObject
{
    Animator ator;

    private void Start()
    {
        ator = GetComponent<Animator>();
    }

    public override void Activate()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.chestOpen);
        ator.SetTrigger("Activated");
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && GameManager.instance.GetHasBossKey())
        {
            StartCoroutine(OpenDoor());
            GameManager.instance.DecrementKeys();
        }
    }

    public IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(0.2f);
        Activate();
    }
}
