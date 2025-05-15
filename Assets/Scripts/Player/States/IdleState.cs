using Unity.VisualScripting;
using UnityEngine;

public class IdleState : IPlayerState
{
    private LinkController link;

    public void Enter(LinkController link)
    {
        // Configura el estado inicial de idle (animación y física).
        this.link = link;
        link.rig.velocity = Vector2.zero;

        link.anim.SetFloat("walk_up", 0);
        link.anim.SetFloat("walk_down", 0);
        link.anim.SetFloat("walk_left", 0);
        link.anim.SetFloat("walk_right", 0);

        float lastX = link.GetLastHorizontalMovementValue();
        float lastY = link.GetLastVerticalMovementValue();

        link.anim.SetFloat("LastMoveX", lastX);
        link.anim.SetFloat("LastMoveY", lastY);
    }

    public void Exit() { }

    public void Update()
    {
        // Detecta inputs para transicionar a otros estados (movimiento, ataque, salto o escudo).
        float mx = link.horizontal_ia.ReadValue<float>();
        float my = link.vertical_ia.ReadValue<float>();
        float atk = link.atack_ia.ReadValue<float>();
        float mj = link.jump_ia.ReadValue<float>();
        float dfs = link.shield_ia.ReadValue<float>();

        if (mx != 0 || my != 0)
            link.ChangeState(new WalkState());
        if (atk != 0)
            link.ChangeState(new AtackState());
        if (mj != 0 && link.HasFeather == true)
            link.ChangeState(new JumpState());
        if (dfs != 0)
            link.ChangeState(new ShieldState());
    }

    public void HandleInput() { }
}