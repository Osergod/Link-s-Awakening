using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffectController : MiscEffect
{
    public override void Start()
    {
        Destroy(gameObject, destructionDelay);
    }
}
