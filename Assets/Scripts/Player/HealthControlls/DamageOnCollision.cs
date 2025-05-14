using System.Collections;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    private bool isDamaging = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDamaging)
        {
            StartCoroutine(ApplyDamageOverTime(collision.gameObject));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDamaging = false;
        }
    }

    private IEnumerator ApplyDamageOverTime(GameObject player)
    {
        isDamaging = true;

        while (isDamaging)
        {
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(1);

                LinkController link = player.GetComponent<LinkController>();
                if (link != null)
                {
                    link.ChangeState(new OnDamagedState());
                }
            }

            yield return new WaitForSeconds(1.5f);
        }
    }
}
