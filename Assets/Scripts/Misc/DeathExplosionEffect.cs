using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathExplosionEffectController : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.25f);
    }
}
