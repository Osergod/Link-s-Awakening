using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWallController : ActionableMapObject
{
    [SerializeField] GameObject effect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Explosion")
        {
            Activate();
        }
    }

    public override void Activate()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.rockShatter);
        Instantiate(effect, transform.position, Quaternion.identity);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
    }
}
