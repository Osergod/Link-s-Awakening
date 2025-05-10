using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardHatBeetleController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject player;
    void Start()
    {
        
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
