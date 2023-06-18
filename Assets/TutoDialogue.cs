using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoDialogue : MonoBehaviour
{
    public string[] tutoDialogue;

    void Start()
    {
        ShowTutoDialogue();
    }

    public void ShowTutoDialogue()
    {
        DialogueManager.Instance.ShowLongDialogues(tutoDialogue);
    }
}
