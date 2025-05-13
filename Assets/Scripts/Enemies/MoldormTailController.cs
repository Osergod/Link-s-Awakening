using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldormTailController : MonoBehaviour
{
    [SerializeField] float followSpeed;
    MiniMoldormController head;
    MiniMoldormBody body;
    Rigidbody2D rb;
    Animator ator;

    void Start()
    {
        ator = GetComponent<Animator>();
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

        ator.SetFloat("X", toBody.x);
        ator.SetFloat("Y", toBody.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            head.GetHurt();
        }
    }
}
