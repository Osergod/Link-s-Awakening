using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class JumpState : IPlayerState
{
    float jumpTimer;
    float jumpDuration = 0.5f;
    private LinkController link;

    public void Enter(LinkController link)
    {
        Debug.Log(link.currentState);
        this.link = link;
        jumpTimer = jumpDuration;
        float mx = link.horizontal_ia.ReadValue<float>();
        float my = link.vertical_ia.ReadValue<float>();

        Vector2 move = new Vector2(mx, my).normalized;
        link.rig.velocity = move * 0;

        link.transform.position += Vector3.up * 1 / 4;
        link.transform.localScale += Vector3.up * 1 / 4;

        link.GetComponentInChildren<BoxCollider2D>().enabled = false;
    }

    public void Exit()
    {
        Debug.Log("no " + link.currentState);
        ResetAnimation();
        link.GetComponentInChildren<BoxCollider2D>().enabled = true;

        link.transform.position += Vector3.down * 1 / 4;
        link.transform.localScale += Vector3.down * 1 / 4;

    }

    void ResetAnimation()
    {
        link.anim.SetFloat("jump_up", 0);
        link.anim.SetFloat("jump_down", 0);
        link.anim.SetFloat("jump_left", 0);
        link.anim.SetFloat("jump_right", 0);
    }

    public void Update()
    {
        float mx = link.horizontal_ia.ReadValue<float>();
        float my = link.vertical_ia.ReadValue<float>();
        float mj = link.jump_ia.ReadValue<float>();

        if (mj == 0)
        {
            if (jumpTimer <= 0)
            {
                link.ChangeState(new IdleState());
                return;
            }
            else
                jumpTimer -= Time.deltaTime;
        }

        Vector2 move = new Vector2(mx, my).normalized;
        link.rig.velocity = move * link.velocidad;

        if (Mathf.Abs(mx) > Mathf.Abs(my))
        {
            if (mx >= 0)
                link.anim.SetFloat("jump_right", 1);
            else
                link.anim.SetFloat("jump_left", 1);
        }
        else
        {
            if (my >= 0)
                link.anim.SetFloat("jump_up", 1);
            else
                link.anim.SetFloat("jump_down", 1);
        }

        link.SetLastHorizontalInputValue(mx);
    }
    public void HandleInput() { }
}
