using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

public class RansomWare : MonoBehaviour
{
    public static Action onComputerBugResolve;

    [SerializeField] private Computer computer;
    [SerializeField] private TMP_InputField computerNumber;
    [SerializeField] public Sprite windowsXPScreen;
    [SerializeField] public Image screen;
    [SerializeField] public TMP_InputField inputField;
    private bool isCaretDestroy = false;

    private static Dictionary<string, string> computerCodeByCode = new Dictionary<string, string> {
        { "#011", "114" },
        { "#007", "914" },
        { "#002", "781" },
    };

    private int codeId;

    private void Awake()
    {
        codeId = UnityEngine.Random.Range(0, 3);
        computerNumber.text = computerCodeByCode.Keys.ToArray()[codeId];
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
        if (inputField.text.Trim() == computerCodeByCode.Values.ToArray()[codeId])
        {
            
            screen.sprite = windowsXPScreen;
            inputField.gameObject.SetActive(false);
            computerNumber.gameObject.SetActive(false);

            computer.FixBug();

            onComputerBugResolve?.Invoke();
        }
    }
}
