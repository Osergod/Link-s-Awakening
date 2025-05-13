using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleFallingEnemies : MonoBehaviour
{
    [SerializeField] GameObject effect;
    [SerializeField] float fallDelay;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.transform.position = transform.position;
            collision.GetComponent<Animator>().speed = 0;
            collision.GetComponent<Enemy>().enabled = false;
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
            Destroy(collision.gameObject);
        }
        
    }
}
