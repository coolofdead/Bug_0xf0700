using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RansomWare : MonoBehaviour
{
    [SerializeField] public TMP_InputField inputField;
    [SerializeField] public int code = 000;
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

    //public void DisableInputField()
    //{
    //    inputField.DeactivateInputField();
    //}
}
