using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Eventos para notificar daño y muerte del jugador
    public static event Action OnPlayedDamaged;
    public static event Action OnPlayerDeath;

    public float health, maxHealth;
    private bool isInvulnerable = false;  // Controla la invulnerabilidad temporal

    // Inicializa la vida al máximo al comenzar
    private void Start()
    {
        health = maxHealth;
    }

    // Maneja el estado de invulnerabilidad
    public bool IsInvulnerable() => isInvulnerable;
    public void SetInvulnerable(bool value) => isInvulnerable = value;

    // Aplica daño al jugador y gestiona estados relacionados
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

        if (health <= 0)
        {
            health = 0;
            Debug.Log("You're dead");
            OnPlayerDeath?.Invoke();
            link?.Death();
        }
    }

    // Activa invulnerabilidad temporal por un tiempo determinado
    public void BecomeTemporarilyInvulnerable(float duration)
    {
        StartCoroutine(TemporaryInvulnerability(duration));
    }

    // Corrutina para gestionar el tiempo de invulnerabilidad
    private IEnumerator TemporaryInvulnerability(float duration)
    {
        SetInvulnerable(true);
        yield return new WaitForSeconds(duration);
        SetInvulnerable(false);
    }

    // Verifica si el escudo está bloqueando un ataque desde cierta posición
    public bool IsShieldBlocking(Vector2 attackerPosition)
    {
        LinkController link = GetComponent<LinkController>();
        if (link == null || !(link.currentState is ShieldState)) return false;

        Vector2 toAttacker = (attackerPosition - (Vector2)transform.position).normalized;
        float dot = Vector2.Dot(link.shieldDirection, toAttacker);

        return dot > 0.5f;  // Distancia para considerar bloqueo efectivo
    }
}