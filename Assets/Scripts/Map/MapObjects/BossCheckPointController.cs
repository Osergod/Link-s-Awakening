using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheckPointController : MonoBehaviour
{
    [SerializeField] GameObject targetCheckPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LinkController.instance.SetCurrentCheckPoint(targetCheckPoint.transform.position);
        }
    }
}
