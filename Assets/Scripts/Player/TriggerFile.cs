using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFile : MonoBehaviour
{
    [SerializeField]
    InfoReference info;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        info.saveData();
    }
}
