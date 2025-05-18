using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMap : MonoBehaviour
{
    private void OnDestroy()
    {
        GameManager.instance.SetHasMap(true);
    }
}
