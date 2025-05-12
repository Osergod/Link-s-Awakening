using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallState : IPlayerState
{
    private LinkController link;

    public void Enter(LinkController link)
    {
        this.link = link;

        link.rig.velocity = Vector2.zero;
        link.anim.SetTrigger("fall");

        link.StartCoroutine(link.Fall_ReloadSceneAfterFall());
    }
    public void Exit() { }
    public void Update() { }
    public void HandleInput() { }
}
