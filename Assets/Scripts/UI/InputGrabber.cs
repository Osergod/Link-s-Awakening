using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class InputGrabber : MonoBehaviour
{
    [Header("Value we got from input field")]
    [SerializeField] private string inputText;
    [SerializeField] TMP_InputField inputField;


    private void OnEnable()
    {
        inputField.ActivateInputField();
        Debug.Log("Activated");
    }

    private void OnDisable()
    {
        inputField.DeactivateInputField();
        inputText = "";
        Debug.Log("Deactivated");
    }

    public void GrabInputFromField (string input)
    {
        inputText = input;
    }

    public void RegisterInputName()
    {
        if (inputText.Length > 0)
        {
            GameManager.instance.SetPlayerName(inputText);
        }
    }
}
