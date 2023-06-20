using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

public class RansomWare : MonoBehaviour
{
    [SerializeField] private TMP_InputField computerNumber;
    [SerializeField] public Sprite windowsXPScreen;
    [SerializeField] public Image screen;
    [SerializeField] public TMP_InputField inputField;
    [SerializeField] public string code = "000";
    private bool isCaretDestroy = false;

    public void Init()
    {
       
    }

    public void EnableInputField(bool activate)
    {
        if (!isCaretDestroy)
        {
            var caret = inputField.GetComponentInChildren<TMP_SelectionCaret>();
            Destroy(caret.gameObject);
            isCaretDestroy = true;
        }

        if (activate)
        {
            inputField.ActivateInputField();
            Debug.Log("ACTIVATED");
        }
        else
        {
            inputField.DeactivateInputField();
            Debug.Log("DEACTIVATED");
        }
    }

    public void CompareCode()
    {
        Debug.Log("input : " + inputField.text);
        if (inputField.text.Trim() == code.Trim())
        {
            
            Debug.Log("Good Code Friend !");
            screen.sprite = windowsXPScreen;
            inputField.gameObject.SetActive(false);
            computerNumber.gameObject.SetActive(false);
        }
    }
}
