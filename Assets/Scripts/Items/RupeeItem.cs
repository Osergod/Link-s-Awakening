using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RupeeItem : Item
{
    [SerializeField] int quantity;

    public override void Use()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.getRupee);
        GameManager.instance.IncrementScore(quantity * 2);
        GameManager.instance.IncrementRupees(quantity);
    }
}
