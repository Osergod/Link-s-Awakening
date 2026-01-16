using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFallEffect : MiscEffect
{
    public override void Start()
    {
        Destroy(gameObject, destructionDelay);
    }
}
