using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnPlayedDamaged;
    public static event Action OnPlayerDeath;

    public float health, maxHealth;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        OnPlayedDamaged?.Invoke();

        if (health <= 0)
        {
            health = 0;
            Debug.Log("You're dead");
            OnPlayedDamaged?.Invoke();

            GetComponent<LinkController>().Death();
        }
    }
}
