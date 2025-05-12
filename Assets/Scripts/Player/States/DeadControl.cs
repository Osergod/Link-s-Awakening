using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadControl: IPlayerState
{
    private LinkController link;

    public void Enter(LinkController link)
    {
        this.link = link;

        link.rig.velocity = Vector2.zero;
        link.anim.SetTrigger("dead");

        link.StartCoroutine(link.ReloadSceneAfterFall());
    }
    public void Exit() { }
    public void Update() { }
    public void HandleInput() { }
}
