using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class GameManager : MonoBehaviour
{
    private static GameManager singletonManger;
    
    public static GameManager instance { 
        get 
        { 
            return RequestGameManager(); 
        }
    }

    private static GameManager RequestGameManager()
    {
        if (!singletonManger) { 
            singletonManger = FindObjectOfType<GameManager>();
        }
        return singletonManger;
    }

    int heartsHP = 6;

    public void DecreaseLife(int damage)
    {
        if (Input.GetKeyDown(KeyCode.T)) { 
            heartsHP -= damage;
            Debug.Log(heartsHP);
        }
    }
}
