using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleFallingEnemies : MonoBehaviour
{
    [SerializeField] GameObject effect;

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.transform.position = transform.position;
        collision.GetComponent<Animator>().speed = 0;
        collision.GetComponent<Enemy>().enabled = false;
        StartCoroutine(DestroyEnemy(collision));
    }

    private IEnumerator DestroyEnemy(Collider2D collision)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(collision.gameObject);
        Instantiate(effect, collision.transform.position, Quaternion.identity);
    }
}
