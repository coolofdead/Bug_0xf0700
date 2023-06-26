using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class TutoDialogue : MonoBehaviour
{
    [Header("Tuto")]
    public Projector tutoProjector;
    public Computer tutoComputerToHack;
    public Outline tutoComputerOutline;
    public Animator elevatorTutoAnimator;
    public float showBugAfterDelay = 3f;
    public float showProjectorAfterDelay = 3f;

    [Header("Tuto Step 1")]
    public string[] tutoDialogue;
    public AudioClip[] tutoAudioClips;

    [Header("Tuto Step 2")]
    public string[] tutoAfterWarningDialogue;
    public AudioClip[] tutoAfterWarningAudioClips;

    private void Awake()
    {
        Computer.onComputerFix += OnComputerFix;
    }

    void Start()
    {
        ShowTutoDialogue();
    }

    public void ShowTutoDialogue()
    {
        DialogueManager.Instance.ShowDialogue(tutoDialogue, tutoAudioClips, OnDialogueDone);
    }

    private void OnDialogueDone()
    {
        elevatorTutoAnimator.Play("OpenDoors");
        Invoke("ShowTutoBug", showBugAfterDelay);
        Invoke("ShowProjector", showProjectorAfterDelay);
    }

    private void ShowTutoBug()
    {
        BugsManager.Instance.BugAppearOnComputer(tutoComputerToHack);
        tutoComputerOutline.enabled = true;
    }

    private void ShowProjector()
    {
        tutoProjector.StartProjecting();
    }

    private void OnComputerFix(Computer computer)
    {
        DialogueManager.Instance.ShowDialogue(tutoAfterWarningDialogue, tutoAfterWarningAudioClips, () => Destroy(gameObject));
    }

    private void OnDestroy()
    {
        Computer.onComputerFix -= OnComputerFix;
    }
}
