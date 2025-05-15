using System.Collections;
using UnityEngine;

public class DamageOnHitbox : MonoBehaviour
{
    private bool isDamaging = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isDamaging)
        {
            StartCoroutine(ApplyDamageOverTime(collision.gameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            isDamaging = false;
        }
    }

    private IEnumerator ApplyDamageOverTime(GameObject enemy)
    {
        isDamaging = true;

        PlayerHealth playerHealth = GetComponentInParent<PlayerHealth>();
        ShieldCollider shield = GetComponentInParent<LinkController>().shieldCollider; // assigna'l al LinkController

        if (playerHealth != null)
        {
            if (shield == null || !shield.IsEnemyInside(enemy))
            {
                playerHealth.TakeDamage(1);
            }
        }

        yield return new WaitForSeconds(1.5f);
        isDamaging = false;
    }
}
