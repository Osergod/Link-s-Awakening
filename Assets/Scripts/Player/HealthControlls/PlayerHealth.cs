using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnPlayedDamaged;
    public static event Action OnPlayerDeath;

    public float health, maxHealth;
    private bool isInvulnerable = false;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (isInvulnerable) return;

        health -= amount;
        OnPlayedDamaged?.Invoke();

        LinkController link = GetComponent<LinkController>();
        if (link != null)
        {
            link.ChangeState(new OnDamagedState());
        }

        StartCoroutine(TemporaryInvulnerability(1.5f));

        if (health <= 0)
        {
            health = 0;
            Debug.Log("You're dead");
            OnPlayerDeath?.Invoke();

            link?.Death();
        }
    }

    private IEnumerator TemporaryInvulnerability(float duration)
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(duration);
        isInvulnerable = false;
    }
}
