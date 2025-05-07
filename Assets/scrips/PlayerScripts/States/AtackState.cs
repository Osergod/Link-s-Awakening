using UnityEditor.Timeline.Actions;
using UnityEngine;

public class AtackState : IPlayerState
{
    float attackTimer;
    float attackDuration = 0.5f;
    private LinkController link;

    public void Enter(LinkController link)
    {
        this.link = link;
        float mx = link.horizontal_ia.ReadValue<float>();
        attackTimer = attackDuration;

        link.anim.SetTrigger("atack");

        link.rig.velocity = Vector2.zero;

    }

    public void Exit() { }

    public void Update()
    {

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
