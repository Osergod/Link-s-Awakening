using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : Item
{
    public override void Use()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.getItem);
        GameManager.instance.IncrementKeys();
    }
}
