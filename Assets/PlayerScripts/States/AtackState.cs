using UnityEngine;

public class AtackState : IPlayerState
{
    private LinkController link;

    public void Enter(LinkController link)
    {
        this.link = link;

        float mx = link.horizontal_ia.ReadValue<float>();

        if (mx > 0)
            ;

        link.anim.SetTrigger("atack");
    }

    public void Exit() { }

    public void Update()
    {
        float atk = link.atack_ia.ReadValue<float>();

        if (atk == 0)
            link.ChangeState(new IdleState());
    }

    public void HandleInput() { }
}
