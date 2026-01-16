using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathExplosionEffectController : MiscEffect
{
    public override void Start()
    {
        Destroy(gameObject, destructionDelay);
    }
}
