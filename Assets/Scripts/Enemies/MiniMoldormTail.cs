using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMoldormTail : MonoBehaviour
{
    [SerializeField] float followSpeed;
    MiniMoldormController head;
    MiniMoldormBody body;
    Rigidbody2D rb;

    void Start()
    {
        head = GetComponentInParent<MiniMoldormController>();
        body = GetComponentInParent<MiniMoldormBody>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 toBody = body.transform.position - transform.position;

        if (transform.position != body.transform.position)
        {
            rb.velocity = toBody * head.GetSpeed() * followSpeed;
        }
    }
}
