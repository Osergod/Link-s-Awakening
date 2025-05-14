using System.Collections;
using UnityEngine;

public class OnDamagedState : IPlayerState
{
    private LinkController link;
    private float timer = 0f;
    private float duration = 1f;

    public void Enter(LinkController link)
    {
        this.link = link;

        float mx = link.horizontal_ia.ReadValue<float>();
        float my = link.vertical_ia.ReadValue<float>() * link.speedYModifier;

        Vector2 move = new Vector2(mx, my).normalized;
        link.rig.velocity = move * link.velocidad;

        link.anim.SetBool("hurt", true);
    }

    public void Exit()
    {
        ResetAnimation();
    }

    void ResetAnimation()
    {
        link.anim.SetBool("hurt", false);
    }

    public void Update()
    {
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

    public void HandleInput() { }
}
