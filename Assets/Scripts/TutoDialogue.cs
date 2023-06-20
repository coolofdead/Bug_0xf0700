using System.Collections;
using System.Collections.Generic;
using StarterAssets;
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
        DialogueManager.Instance.ShowDialogue(tutoDialogue, OnDialogueDone);
    }

    private void OnDialogueDone()
    {
        Invoke("ShowTutoBug", 5f);
    }

    private void ShowTutoBug()
    {
        BugsManager.Instance.BugAppear();
    }
}
