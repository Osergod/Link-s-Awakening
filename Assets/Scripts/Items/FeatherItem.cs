using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherItem : Item
{
    public override void Use()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.getItem);
        LinkController.instance.SetHasFeather(true);
    }
}
