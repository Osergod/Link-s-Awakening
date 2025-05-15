using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class JumpState : IPlayerState
{
    // Control del estado de salto
    public bool OnJumpState;
    float jumpTimer;
    float jumpDuration = 1f;
    private LinkController link;

    public void Enter(LinkController link)
    {
        // Inicialización del salto
        this.link = link;
        jumpTimer = jumpDuration;
        float mx = link.horizontal_ia.ReadValue<float>();
        float my = link.vertical_ia.ReadValue<float>();

        Vector2 move = new Vector2(mx, my).normalized;

        // Aplicar transformaciones visuales del salto
        link.transform.position += Vector3.up * 1;
        link.shadow.transform.localPosition = Vector3.down * 1;
        link.transform.localScale += Vector3.up * 1 / 4;

        link.OnJumping = true;
        SetStairsTrigger(false);
    }

    public void Exit()
    {
        // Revertir transformaciones del salto
        ResetAnimation();

        link.transform.position += Vector3.down * 1;
        link.shadow.transform.localPosition = Vector3.up * -0.5f;
        link.transform.localScale += Vector3.down * 1 / 4;

        link.OnJumping = false;
        SetStairsTrigger(true);
    }

    void ResetAnimation()
    {
        // Reiniciar parámetros de animación
        link.anim.SetFloat("jump_up", 0);
        link.anim.SetFloat("jump_down", 0);
        link.anim.SetFloat("jump_left", 0);
        link.anim.SetFloat("jump_right", 0);
    }

    public void Update()
    {
        // Lógica de actualización durante el salto
        float mx = link.horizontal_ia.ReadValue<float>();
        float my = link.vertical_ia.ReadValue<float>();
        float mj = link.jump_ia.ReadValue<float>();

        if (jumpTimer <= 0)
        {
            link.ChangeState(new IdleState());
            return;
        }
        else
            jumpTimer -= Time.deltaTime;

        // Movimiento durante el salto
        Vector2 move = new Vector2(mx, my).normalized;
        link.rig.velocity = move * link.velocidad;

        // Animaciones según dirección
        if (Mathf.Abs(mx) >= Mathf.Abs(my))
        {
            if (mx >= 0)
                link.anim.SetFloat("jump_right", 1);
            if (mx <= 0)
                link.anim.SetFloat("jump_left", 1);
        }
        if (Mathf.Abs(mx) <= Mathf.Abs(my))
        {
            if (my >= 0)
                link.anim.SetFloat("jump_up", 1);
            if (my <= 0)
                link.anim.SetFloat("jump_down", 1);
        }

        link.SetLastHorizontalInputValue(mx);
    }

    void SetStairsTrigger(bool isTrigger)
    {
        // Activar/desactivar colisiones con escaleras
        GameObject[] stairs = GameObject.FindGameObjectsWithTag("Stairs");
        foreach (GameObject stair in stairs)
        {
            Collider2D col = stair.GetComponent<Collider2D>();
            if (col != null)
            {
                col.isTrigger = isTrigger;
            }
        }
    }

    public void HandleInput() { }
}