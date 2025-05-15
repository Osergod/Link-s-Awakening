using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using static Unity.Collections.AllocatorManager;

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
}
