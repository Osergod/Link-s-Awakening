using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    [Range(0, 1)][SerializeField] float atractionScale;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Vector3 towardsCenter = transform.position - collision.transform.position;
        towardsCenter.Normalize();

        if (collision.tag == "Player")
        {
            collision.transform.position += towardsCenter * atractionScale;
        }

        if (collision.tag == "Enemy")
        {
            collision.transform.position += towardsCenter * atractionScale;
        }
    }
    
}
