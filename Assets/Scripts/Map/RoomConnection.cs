using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomConnectionController : MonoBehaviour
{
    [SerializeField]
    GameObject destination;
    //[SerializeField]
    //Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        collision.transform.position = destination.transform.position;
        //StartCoroutine(MoveToRoom(collision));

    }

    /*private IEnumerator MoveToRoom(Collider2D collision)
    {
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(0.25f);
        collision.transform.position = destination.transform.position;
        anim.SetTrigger("End");
    }*/
}
