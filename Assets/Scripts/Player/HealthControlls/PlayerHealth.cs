using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnPlayedDamaged;
    public static event Action OnPlayerDeath;

    public float health, maxHealth;

    private bool isInvulnerable = false;  // Variable para gestionar la invulnerabilidad

    private void Start()
    {
        health = maxHealth;
    }

    // Función para comprobar si el jugador es invulnerable
    public bool IsInvulnerable()
    {
        return isInvulnerable;  // Devuelve el estado de invulnerabilidad
    }

    // Función que activa la invulnerabilidad
    public void SetInvulnerable(bool value)
    {
        isInvulnerable = value;
    }

    public void TakeDamage(float amount)
    {
        if (isInvulnerable) return;  // Si el jugador es invulnerable, no recibirá daño

        health -= amount;
        OnPlayedDamaged?.Invoke();

        LinkController link = GetComponent<LinkController>();
        if (link != null)
        {
            link.ChangeState(new OnDamagedState());  // Cambiar al estado de "dañado"
        }

        if (health <= 0)
        {
            health = 0;
            Debug.Log("You're dead");
            OnPlayerDeath?.Invoke();

            link?.Death();
        }
    }

    // Puedes añadir un temporizador para manejar la invulnerabilidad por un tiempo determinado
    public void BecomeTemporarilyInvulnerable(float duration)
    {
        StartCoroutine(TemporaryInvulnerability(duration));
    }

    private IEnumerator TemporaryInvulnerability(float duration)
    {
        SetInvulnerable(true);
        yield return new WaitForSeconds(duration);
        SetInvulnerable(false);
    }

    public bool IsShieldBlocking(Vector2 attackerPosition)
    {
        LinkController link = GetComponent<LinkController>();
        if (link == null || !(link.currentState is ShieldState)) return false;

        Vector2 toAttacker = (attackerPosition - (Vector2)transform.position).normalized;
        float dot = Vector2.Dot(link.shieldDirection, toAttacker);

        return dot > 0.7f; // Pot ajustar-se el marge (0.7 és ~45º)
    }

}
