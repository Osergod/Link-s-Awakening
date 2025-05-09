using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRockController : ActionableMapObject
{
    [SerializeField] GameObject effect;

    public override void Activate()
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<LinkController>().GetKeys() > 0)
            {
                StartCoroutine(DestroyRock());
                collision.GetComponent<LinkController>().DecrementKeys();
            }
        }
    }

    public IEnumerator DestroyRock()
    {
        yield return new WaitForSeconds(0.2f);
        Activate();
    }
}
