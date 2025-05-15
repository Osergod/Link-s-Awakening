using System.Collections.Generic;
using UnityEngine;

public class ShieldState : IPlayerState
{
    private LinkController link;
    private float defenseTimer;
    private float defenseMinDuration = 0.1f;
    private List<Collider2D> enemyHitboxes = new List<Collider2D>();

    public void Enter(LinkController link)
    {
        this.link = link;
        defenseTimer = defenseMinDuration;

        link.rig.velocity = Vector2.zero;
        link.anim.SetFloat("shield", 1);

        float mx = link.horizontal_ia.ReadValue<float>();
        float my = link.vertical_ia.ReadValue<float>();

        Vector2 dir = new Vector2(mx, my);
        if (dir == Vector2.zero) dir = Vector2.down;

        link.SetShieldDirection(dir);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Collider2D hitbox = enemy.GetComponent<Collider2D>();
            if (hitbox != null && hitbox.isTrigger)
            {
                hitbox.isTrigger = false;
                enemyHitboxes.Add(hitbox);
            }
        }
    }


    public void Exit()
    {
        link.anim.SetFloat("shield", 0);

        foreach (Collider2D hitbox in enemyHitboxes)
        {
            if (hitbox != null)
            {
                hitbox.isTrigger = true;
            }
        }

        enemyHitboxes.Clear();
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
                link.ChangeState(new WalkState());
                return;
            }
            else
            {
                defenseTimer -= Time.deltaTime;
            }
        }
    }

    public void HandleInput() { }
}
