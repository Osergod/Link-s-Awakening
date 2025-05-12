using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldState : IPlayerState
{
    private LinkController link;
    float defenseTimer;
    float defenseMinDuration = 0.1f;

    public void Enter(LinkController link)
    {
        this.link = link;
        defenseTimer = defenseMinDuration;

        float mx = link.horizontal_ia.ReadValue<float>();

        link.rig.velocity = Vector2.zero;
        link.anim.SetFloat("shield", 1);
    }

    public void Exit()
    {
        link.anim.SetFloat("shield", 0);
    }

    public void Update()
    {
        float mx = link.horizontal_ia.ReadValue<float>();
        float my = link.vertical_ia.ReadValue<float>();
        float dfs = link.shield_ia.ReadValue<float>();

        Vector2 move = new Vector2(mx, my).normalized;
        link.rig.velocity = move * link.velocidad;

        if (dfs == 0)
        {
            if (defenseTimer <= 0)
            {
                if (dfs == 0)
                {
                    link.ChangeState(new WalkState());
                    return;
                }
            }
            else
            {
                defenseTimer -= Time.deltaTime;
            }
        }

        Debug.Log(defenseTimer);
    }

    public void HandleInput() { }
}
