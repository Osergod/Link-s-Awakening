using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Health()
    {
        double health = 3;
        double maxHealth = 3;
    }

    public void Hurt(double health)
    {
        health -= 0.5;
    }
}
