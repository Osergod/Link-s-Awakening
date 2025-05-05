using UnityEngine;

public class WalkState : IPlayerState
{
    private LinkController link;

    public void Enter(LinkController link)
    {
        this.link = link;
    }

    public void Exit() { }

    public void Update()
    {
        float mx = link.horizontal_ia.ReadValue<float>();
        float my = link.vertical_ia.ReadValue<float>();

        if (mx == 0 && my == 0)
        {
            link.ChangeState(new IdleState());
            return;
        }

        Vector2 move = new Vector2(mx, my).normalized;
        link.rig.velocity = move * link.velocidad;

        
        link.anim.SetFloat("walk_up", 0);
        link.anim.SetFloat("walk_down", 0);
        link.anim.SetFloat("walk_left", 0);
        link.anim.SetFloat("walk_right", 0);

        
        if (Mathf.Abs(my) > Mathf.Abs(mx))
        {
            if (my > 0)
                link.anim.SetFloat("walk_up", 1);
            else
                link.anim.SetFloat("walk_down", 1);
        }
        else
        {
            if (mx > 0)
                link.anim.SetFloat("walk_right", 1);
            else
                link.anim.SetFloat("walk_left", 1);
        }

        link.TransformP(mx);
    }

    public void HandleInput() { }
}
