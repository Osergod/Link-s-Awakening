using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] public int health = 1;
    [SerializeField] protected int damage;
    [SerializeField] protected GameObject effect;
    protected LayerMask originalLayer;
    [Range(1f, 5f)][SerializeField] protected float knockBackPower;
    protected LinkController player;

    private void Awake()
    {
        originalLayer = gameObject.layer;
        knockBackPower *= 10;
        player = FindAnyObjectByType<LinkController>();
    }

    public abstract void Attack();

    public int GetHealth()
    {
        return health;
    }

    public int GetDamage()
    {
        return damage;
    }

    public void GetHurt()
    {
        health --;

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
        Instantiate(effect, transform.position, Quaternion.identity);
    }

    public void DisableLayer()
    {
        gameObject.layer = 0;
    }

    public void EnableLayer()
    {
        gameObject.layer = originalLayer;
    }
}
