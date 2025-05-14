using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] public int health = 1;
    [SerializeField] protected int damage;
    [SerializeField] protected GameObject effect;
    [Range(1f, 5f)][SerializeField] protected float knockBackPower;
    protected LayerMask originalLayer;
    protected LinkController player;
    Vector2 initialPos;

    private void Awake()
    {
        initialPos = gameObject.transform.position;
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

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.transform.position = initialPos;
        gameObject.SetActive(false);
    }
}
