using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBombController : MonoBehaviour
{
    [SerializeField] GameObject effect;
    [SerializeField] float explosionDelay;

    void Start()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.useShield);
        Destroy(gameObject, explosionDelay);
        
    }

    private void OnDestroy()
    {
        Instantiate(effect, transform.position, Quaternion.identity);
    }
}
