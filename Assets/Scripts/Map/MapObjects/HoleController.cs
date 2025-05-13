using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
<<<<<<< HEAD
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

=======
    [Range(0, 1)][SerializeField] float atractionScale;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Vector3 towardsCenter = transform.position - collision.transform.position;
            towardsCenter.Normalize();

            collision.transform.position += towardsCenter * atractionScale;
>>>>>>> test
        }
    }
}
