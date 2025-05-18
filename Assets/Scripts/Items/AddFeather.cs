using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFeather : MonoBehaviour
{
    private void OnDestroy()
    {
        LinkController.instance.SetHasFeather(true);
    }
}
