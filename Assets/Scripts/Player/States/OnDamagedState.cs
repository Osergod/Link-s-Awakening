using System.Collections;
using UnityEngine;

public class OnDamagedState : IPlayerState
{
    // Variables de estado
    private LinkController link;
    private float timer = 0f;
    private float duration = 1f;

    // Método de entrada al estado
    public void Enter(LinkController link)
    {
        // Inicia el estado de daño: aplica retroceso físico y activa la animación de herido
        AudioManager.instance.PlaySFX(AudioManager.instance.linkHurt);
        this.link = link;

        float mx = link.horizontal_ia.ReadValue<float>();
        float my = link.vertical_ia.ReadValue<float>() * link.speedYModifier;

        Vector2 move = new Vector2(mx, my).normalized;
        link.rig.velocity = move * link.velocidad;

        link.anim.SetBool("hurt", true);
    }

    // Método de actualización
    public void Update()
    {
        // Controla la duración del estado y permite movimiento limitado durante el daño
        float mx = link.horizontal_ia.ReadValue<float>();
        float my = link.vertical_ia.ReadValue<float>() * link.speedYModifier;

        timer += Time.deltaTime;
        if (timer >= duration)
        {
            link.ChangeState(new WalkState());
            return;
        }

        Vector2 move = new Vector2(mx, my).normalized;
        link.rig.velocity = move * link.velocidad;

        ResetAnimation();
        link.anim.SetBool("hurt", true);

        if (move != Vector2.zero)
        {
            link.SetLastHorizontalInputValue(move.x);
            link.SetLastVerticalInputValue(move.y);
        }
    }

    // Método de salida del estado
    public void Exit()
    {
        // Restaura las animaciones al salir del estado de daño
        ResetAnimation();
    }

    // Método auxiliar
    void ResetAnimation()
    {
        // Asegura que la animación de "herido" se desactive para evitar bugs visuales
        link.anim.SetBool("hurt", false);
    }

    // Método obligatorio (sin uso en este estado)
    public void HandleInput() { }
}