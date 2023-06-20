using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class TutoDialogue : MonoBehaviour
{
    public Animator elevatorTutoAnimator;
    public float showBugAfterDelay = 3f;
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
        elevatorTutoAnimator.Play("OpenDoors");
        Invoke("ShowTutoBug", showBugAfterDelay);
    }

    private void ShowTutoBug()
    {
        BugsManager.Instance.BugAppear();
    }
}
