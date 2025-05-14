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
        link.anim.SetBool("hurt", true);

        Vector2 pushDir = new Vector2(link.GetLastHorizontalMovementValue(), link.GetLastVerticalMovementValue()).normalized;
        link.rig.velocity = pushDir * link.velocidad;
    }

    public void Exit()
    {
        link.anim.SetBool("hurt", false);
        link.rig.velocity = Vector2.zero;
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            link.ChangeState(new WalkState());
        }
    }

    public void HandleInput() { }
}
