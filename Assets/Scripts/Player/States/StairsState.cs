using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsState : IPlayerState
{
    private LinkController link;

    public void Enter(LinkController link)
    {
        this.link = link;
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
        float my = link.vertical_ia.ReadValue<float>();

        if (!link.IsOnStairs)
        {
            link.ChangeState(new WalkState());
            return;
        }

        Vector2 move = new Vector2(mx, my / 2).normalized;
        link.rig.velocity = move * link.velocidad;

        ResetAnimation();

        if (Mathf.Abs(mx) >= Mathf.Abs(my))
        {
            link.velocidad = 4;

            if (mx > 0)
                link.anim.SetFloat("walk_right", 1);
            if (mx < 0)
                link.anim.SetFloat("walk_left", 1);
        }
        else
        {
            link.velocidad = 2;

            if (my > 0)
                link.anim.SetFloat("walk_up", 1);
            if (my < 0)
                link.anim.SetFloat("walk_down", 1);
        }

        link.SetLastHorizontalInputValue(mx);
    }

    

    public void HandleInput() { }
}

