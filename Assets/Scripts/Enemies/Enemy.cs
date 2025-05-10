using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int life;
    [SerializeField] protected int damage;
    public abstract void Attack();

    public int GetLife()
    {
        return life;
    }

    public int GetDamage()
    {
        return damage;
    }

    public void GetHurt(int incomingDamage)
    {
        life -= incomingDamage;
    }
}
