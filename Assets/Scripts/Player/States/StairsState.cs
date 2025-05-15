using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsState : IPlayerState
{
    private LinkController link;

    public void Enter(LinkController link)
    {
        // Prepara al personaje para moverse en escaleras: inicializa referencias pero mantiene la configuración previa
        this.link = link;
    }

    public void Exit()
    {
        // Limpia las animaciones de movimiento al salir de las escaleras para evitar transiciones bruscas
        ResetAnimation();
    }

    void ResetAnimation()
    {
        // Restablece todos los parámetros de animación de caminata para asegurar un estado neutro
        link.anim.SetFloat("walk_up", 0);
        link.anim.SetFloat("walk_down", 0);
        link.anim.SetFloat("walk_left", 0);
        link.anim.SetFloat("walk_right", 0);
    }

    public void Update()
    {
        // Controla el movimiento en escaleras: velocidad adaptativa, animaciones y detección de salida
        float mx = link.horizontal_ia.ReadValue<float>();
        float my = link.vertical_ia.ReadValue<float>();

        // Verifica si el personaje sigue en escaleras para evitar estados inválidos
        if (!link.IsOnStairs)
        {
            link.ChangeState(new WalkState());
            return;
        }

        // Aplica movimiento con velocidad vertical reducida para simular esfuerzo en escaleras
        Vector2 move = new Vector2(mx, my / 2).normalized;
        link.rig.velocity = move * link.velocidad;

        ResetAnimation();

        // Ajusta velocidad y animaciones según dirección predominante
        if (Mathf.Abs(mx) >= Mathf.Abs(my))
        {
            link.velocidad = 4; // Velocidad normal en dirección horizontal

            if (mx > 0)
                link.anim.SetFloat("walk_right", 1);
            if (mx < 0)
                link.anim.SetFloat("walk_left", 1);
        }
        else
        {
            link.velocidad = 2; // Velocidad reducida al subir/bajar escaleras

            if (my > 0)
                link.anim.SetFloat("walk_up", 1);
            if (my < 0)
                link.anim.SetFloat("walk_down", 1);
        }

        // Guarda la última dirección para transiciones posteriores
        link.SetLastHorizontalInputValue(mx);
    }

    public void HandleInput() { }
}