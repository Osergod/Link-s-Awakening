using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStairsController : MonoBehaviour
{
    [SerializeField] bool canPass;
    [SerializeField] GameObject targetStairs;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && canPass)
        {
            LinkController.instance.transform.position = targetStairs.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !canPass)
        {
            canPass = true;
        }
        else
        {
            canPass = false;
        }
    }
}
