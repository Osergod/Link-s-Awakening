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
            AudioManager.instance.PlaySFX(AudioManager.instance.stairs);
            LinkController.instance.transform.position = targetStairs.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !canPass)
        {
            canPass = true;
        }
        else if (collision.tag == "Player" && canPass)
        {
            canPass = false;
        }
    }

    public void Activate()
    {
        canPass = true;
    }

    public void Deactivate()
    {
        canPass = false;
    }
}
