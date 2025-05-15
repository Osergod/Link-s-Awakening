using System.Collections;
using UnityEngine;

public class DamageOnHitbox : MonoBehaviour
{
    private bool isDamaging = false;  // Controla si ya se está aplicando daño

    // Detecta cuando un enemigo permanece en el área de daño
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isDamaging)
        {
            StartCoroutine(ApplyDamageOverTime(collision.gameObject));
        }
    }

    // Resetea el estado al salir el enemigo del área
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            isDamaging = false;
        }
    }

    // Aplica daño continuo con intervalo de tiempo
    private IEnumerator ApplyDamageOverTime(GameObject enemy)
    {
        isDamaging = true;

        PlayerHealth playerHealth = GetComponentInParent<PlayerHealth>();
        ShieldCollider shield = GetComponentInParent<LinkController>().shieldCollider;

        // Aplica daño si no está protegido por escudo
        if (playerHealth != null)
        {
            if (shield == null || !shield.IsEnemyInside(enemy))
            {
                playerHealth.TakeDamage(1);
            }
        }

        yield return new WaitForSeconds(1.5f);  // Intervalo entre daños
        isDamaging = false;
    }
}