using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosionController : MiscEffect
{
    public override void Start()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.bombExplosion);
        Destroy(gameObject, destructionDelay);
    }
}
