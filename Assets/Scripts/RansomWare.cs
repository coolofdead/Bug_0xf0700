using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RansomWare : MonoBehaviour
{
    [SerializeField] private Computer computer;
    [SerializeField] private TMP_InputField computerNumber;
    [SerializeField] public TMP_InputField inputField;

    public bool assignFirstFixCode = false;

    private FixCode fixCode;

    private void Start()
    {
        fixCode = FixCodeManager.Instance.GetRandomFixCode(assignFirstFixCode);
        computerNumber.text = fixCode.pcId;
    }

    public void EnableInputField(bool activate)
    {
        var caret = inputField.GetComponentInChildren<TMP_SelectionCaret>();
        Destroy(caret?.gameObject);

        if (activate)
        {
            inputField.Select();
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
            inputField.gameObject.SetActive(false);
            computerNumber.gameObject.SetActive(false);

            computer.FixBug();
        }
    }
}
