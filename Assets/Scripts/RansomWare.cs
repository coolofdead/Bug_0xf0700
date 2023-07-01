using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

public class RansomWare : MonoBehaviour
{
    [SerializeField] private Computer computer;
    [SerializeField] private TMP_InputField computerNumber;
    [SerializeField] public Sprite windowsXPScreen;
    [SerializeField] public Image screen;
    [SerializeField] public TMP_InputField inputField;
    private bool isCaretDestroy = false;

    public bool assignFirstFixCode = false;

    private FixCode fixCode;

    private void Start()
    {
        fixCode = FixCodeManager.Instance.GetRandomFixCode(assignFirstFixCode);
        computerNumber.text = fixCode.pcId;
    }

    public void EnableInputField(bool activate)
    {
        if (!isCaretDestroy)
        {
            var caret = inputField.GetComponentInChildren<TMP_SelectionCaret>();
            Destroy(caret?.gameObject);
            isCaretDestroy = true;
        }

        if (activate)
        {
            inputField.ActivateInputField();
        }
        else
        {
            inputField.DeactivateInputField();
        }
    }

    public void CompareCode()
    {
        if (inputField.text.Trim() == fixCode.fixCode)
        {
            screen.sprite = windowsXPScreen;
            inputField.gameObject.SetActive(false);
            computerNumber.gameObject.SetActive(false);

            computer.FixBug();
        }
    }
}
