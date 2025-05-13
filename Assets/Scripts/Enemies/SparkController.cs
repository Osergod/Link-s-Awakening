using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SparkController : Enemy
{
    [SerializeField] float speed;
    [SerializeField] List<GameObject> targetPoints = new List<GameObject>();

    int nextTagetPoint = 0;

    void Start()
    {

    }

    void Update()
    {
        Move();
        //Debug.Log(nextTagetPoint);
    }

    public void Move()
    {
        if (nextTagetPoint == targetPoints.Count)
        {
            nextTagetPoint = 0;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPoints[nextTagetPoint].transform.position, 2f * Time.deltaTime);

        if (transform.position == targetPoints[nextTagetPoint].transform.position)
        {
            nextTagetPoint++;
        }
    }

    public override void Attack()
    {
        
    }
}
