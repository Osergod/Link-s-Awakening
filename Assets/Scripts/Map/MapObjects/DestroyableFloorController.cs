using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableFloorController : ActionableMapObject
{
    [SerializeField] float destructionDelay;
    [SerializeField] GameObject hole;

    private bool isAbove;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isAbove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isAbove = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Activate();
    }

    private IEnumerator BreakFloor()
    {
        yield return new WaitForSeconds(destructionDelay);
        if (isAbove)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.groundCrumble);
            Destroy(gameObject);
            Instantiate(hole, transform.position, Quaternion.identity);
        }
    }

    public override void Activate()
    {
        StartCoroutine(BreakFloor());
    }
}
