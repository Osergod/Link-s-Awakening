using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        if (GameManager.instance.GetPlayerName().Length > 0)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
