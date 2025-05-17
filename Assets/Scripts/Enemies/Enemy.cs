using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] public int maxHealth = 1;
    [SerializeField] protected int damage;
    [SerializeField] protected GameObject effect;
    [Range(1f, 5f)][SerializeField] protected float knockBackPower;
    int health;
    protected LayerMask originalLayer;
    protected LinkController player;
    Vector2 initialPos;
    private bool isDying;

    private void Awake()
    {
        initialPos = gameObject.transform.position;
        originalLayer = gameObject.layer;
        knockBackPower *= 10;
        player = FindAnyObjectByType<LinkController>();
        health = maxHealth;
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
        if (!isDying)
        {
            isDying = true;
            Destroy(gameObject);
            if (effect != null)
            {
                Instantiate(effect, transform.position, Quaternion.identity);
            }
            GameManager.instance.IncrementKills();
            //GameManager.instance.IncrementScore();
        }
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
        health = maxHealth;
        gameObject.transform.position = initialPos;
        gameObject.SetActive(false);
    }
}
