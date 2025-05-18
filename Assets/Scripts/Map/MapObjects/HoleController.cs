using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    [Range(0, 1)][SerializeField] float atractionScale;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Vector3 towardsCenter;

        if (collision.tag == "Player" && !GameManager.instance.GetPlayerJumping())
        {
            towardsCenter = transform.position - LinkController.instance.transform.position;
            towardsCenter.Normalize();
            LinkController.instance.transform.position += towardsCenter * atractionScale;
        }

        if (collision.tag == "Enemy")
        {
            towardsCenter = transform.position - collision.transform.position;
            towardsCenter.Normalize();
            collision.transform.position += towardsCenter * atractionScale;
        }
    }
}
