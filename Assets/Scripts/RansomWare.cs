using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RansomWare : MonoBehaviour
{
    [SerializeField] public TMP_InputField inputField;
    [SerializeField] public int code = 000;

    public void Init()
    {
        inputField.caretPosition = 0;
        
        //if (inputField.Tr<TMP_SelectionCaret>(out var caret))
        //{
        //    Debug.Log("Destroy caret !");
        //    Destroy(caret);
        //}

        inputField.ActivateInputField();
    }
}
