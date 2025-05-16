using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputGrabber : MonoBehaviour
{
    [Header("Value we got from input field")]
    [SerializeField] private string inputText;

    public void GrabInputFromField (string input)
    {
        inputText = input;
        DisplayReactionToInput();
    }

    private void DisplayReactionToInput()
    {
        Debug.Log(inputText);
    }
}
