using UnityEditor.Timeline.Actions;
using UnityEngine;

public class AtackState : IPlayerState
{
    float attackTimer;
    float attackDuration = 0.1f;
    private LinkController link;

    public void Enter(LinkController link)
    {
        // Inicia el ataque: congela el movimiento y activa la animación.
        this.link = link;
        float mx = link.horizontal_ia.ReadValue<float>();
        attackTimer = attackDuration;

        link.anim.SetTrigger("atack");
        link.rig.velocity = Vector2.zero;
    }

    public void Exit() { }

    public void Update()
    {
        // Controla la duración del ataque y transición a idle si no hay input.
        float atk = link.atack_ia.ReadValue<float>();

        if (atk == 0)
        {
            if (attackTimer <= 0)
            {
                link.ChangeState(new IdleState());
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }
    }

    public void HandleInput() { }
}