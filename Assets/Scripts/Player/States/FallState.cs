using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallState : IPlayerState
{
    private LinkController link;

    public void Enter(LinkController link)
    {
        // Inicia la caída: congela el movimiento y activa la animación de caída.
        this.link = link;
        link.rig.velocity = Vector2.zero;
        link.anim.SetTrigger("fall");

        link.StartCoroutine(link.Fall_GoToCheckPointAfterFall());
    }

    public void Exit() { }
    public void Update() { }
    public void HandleInput() { }
}