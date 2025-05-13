using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMoldormBody : MonoBehaviour
{
    [SerializeField] float followSpeed;
    MiniMoldormController head;
    Rigidbody2D rb;

    void Start()
    {
        head = GetComponentInParent<MiniMoldormController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 toHead = head.transform.position - transform.position;

        if (transform.position != head.transform.position)
        {
            rb.velocity = toHead * head.GetSpeed() * followSpeed;
        }
    }
}
