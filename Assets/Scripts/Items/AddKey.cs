using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddKey : MonoBehaviour
{
    private void OnDestroy()
    {
        GameManager.instance.IncrementKeys();
    }
}
