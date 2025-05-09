using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableFloor : MonoBehaviour
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
        StartCoroutine(BreakFloor());
    }

    private IEnumerator BreakFloor()
    {
        yield return new WaitForSeconds(destructionDelay);
        if (isAbove)
        {
            Destroy(gameObject);
            Instantiate(hole, transform.position, Quaternion.identity);
        }
    }
}
