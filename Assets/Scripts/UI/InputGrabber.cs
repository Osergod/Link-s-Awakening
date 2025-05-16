using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        Debug.Log("Deactivated");
    }

    public void GrabInputFromField (string input)
    {
        if (input != null)
        {
            inputText = input;
            DisplayReactionToInput();
        }
    }

    private void DisplayReactionToInput()
    {
        Debug.Log(inputText);
    }
}
