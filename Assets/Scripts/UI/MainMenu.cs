using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void StartGame()
    {
        if (GameManager.instance.GetPlayerName().Length > 0 || GameManager.instance.GetPlayerName() == null)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
