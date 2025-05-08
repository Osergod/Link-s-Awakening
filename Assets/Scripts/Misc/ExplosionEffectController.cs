using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffectController : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.25f);
    }
}
