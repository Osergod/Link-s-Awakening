using Unity.VisualScripting;
using UnityEngine;

public class WalkState : IPlayerState
{
    private LinkController link;

    public void Enter(LinkController link)
    {
        this.link = link;
        link.velocidad = 4;
    }

    public void Exit()
    {
        ResetAnimation();
    }

    void ResetAnimation()
    {
        link.anim.SetFloat("walk_up", 0);
        link.anim.SetFloat("walk_down", 0);
        link.anim.SetFloat("walk_left", 0);
        link.anim.SetFloat("walk_right", 0);
    }

    public void Update()
    {
        float mx = link.horizontal_ia.ReadValue<float>();
        float my = link.vertical_ia.ReadValue<float>() * link.speedYModifier;
        float atk = link.atack_ia.ReadValue<float>();
        float mj = link.jump_ia.ReadValue<float>();

        if (mx == 0 && my == 0)
        {
            link.ChangeState(new IdleState());
            return;
        }
        if (atk != 0)
        {
            link.ChangeState(new AtackState());
            return;
        }
        if (mj != 0 && link.HasFeather == true)
        {
            link.ChangeState(new JumpState());
            return;
        }

        Vector2 move = new Vector2(mx, my).normalized;
        link.rig.velocity = move * link.velocidad;

        ResetAnimation(); 

        if (Mathf.Abs(mx) > Mathf.Abs(my))
        {
            if (mx > 0)
                link.anim.SetFloat("walk_right", 1);
            else
                link.anim.SetFloat("walk_left", 1);
        }
        else
        {
            if (my > 0)
                link.anim.SetFloat("walk_up", 1);
            else
                link.anim.SetFloat("walk_down", 1);
        }

        if (move != Vector2.zero)
        {
            link.SetLastHorizontalInputValue(move.x);
            link.SetLastVerticalInputValue(move.y);
        }
    }


    public void HandleInput() { }
}
