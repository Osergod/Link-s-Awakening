using UnityEngine;

public class IdleState : IPlayerState
{
    private LinkController link;

    public void Enter(LinkController link)
    {
        this.link = link;

        
        link.rig.velocity = Vector2.zero;

        
        link.anim.SetFloat("walk_up", 0);
        link.anim.SetFloat("walk_down", 0);
        link.anim.SetFloat("walk_left", 0);
        link.anim.SetFloat("walk_right", 0);
    }

    public void Exit() { }

    public void Update()
    {
        float mx = link.horizontal_ia.ReadValue<float>();
        float my = link.vertical_ia.ReadValue<float>();

        if (mx != 0 || my != 0)
        {
            link.ChangeState(new WalkState());
        }
    }

    public void HandleInput() { }
}
