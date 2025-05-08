using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stairs : MonoBehaviour
{
    public bool OnStairs;

    private void Start() => OnStairs = false;

    private void OnTriggerEnter2D(Collider2D collision) => OnStairs = true;
    private void OnTriggerExit2D(Collider2D collision) => OnStairs = false;
}