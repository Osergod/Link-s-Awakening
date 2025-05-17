using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleFallingController : MonoBehaviour
{
    [SerializeField] GameObject effect;
    [SerializeField] float fallDelay;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" && !LinkController.instance.GetIsBeingPulled())
        {
            LinkController.instance.SetIsBeingPulled(true);
            LinkController.instance.transform.position = Vector2.MoveTowards(LinkController.instance.transform.position, transform.position, 5);
            LinkController.instance.ChangeState(new FallState());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.transform.position = transform.position;
            collision.GetComponent<Animator>().speed = 0;
            collision.GetComponent<Enemy>().enabled = false;
            collision.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(DestroyEnemy(collision));
        }
    }

    private IEnumerator DestroyEnemy(Collider2D collision)
    {
        Vector3 collisionPosition = collision.transform.position;
        yield return new WaitForSeconds(fallDelay);

        if (collision != null)
        {
            Instantiate(effect, collisionPosition, Quaternion.identity);
            collision.GetComponent<Enemy>().DisableEffect();
            collision.GetComponent<Enemy>().Die();
        }
        
    }
}
