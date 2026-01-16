using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : Item
{
    public override void Use()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.getItem);
        GameManager.instance.IncrementBombs();
    }
}
