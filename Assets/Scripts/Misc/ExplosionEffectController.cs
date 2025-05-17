using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffectController : MiscEffect
{
    public override void Start()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.burst);
        Destroy(gameObject, destructionDelay);
    }
}
