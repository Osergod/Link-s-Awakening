using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class MiscEffect : MonoBehaviour
{
    [SerializeField] protected float destructionDelay;

    public abstract void Start();
}
