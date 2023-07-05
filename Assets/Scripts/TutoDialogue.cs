using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class TutoDialogue : MonoBehaviour
{
    public TimeManager timeManager;

    public float startDelay = 2f;

    [Header("Tuto")]
    public Projector tutoProjector;
    public Computer tutoComputerToHack;
    public Outline tutoComputerOutline;
    public Animator elevatorTutoAnimator;
    public float showBugAfterDelay = 3f;
    public float showProjectorAfterDelay = 3f;
    public GameObject clickBookUI;

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
        Invoke("ShowTutoDialogue", startDelay);
    }

    public void ShowTutoDialogue()
    {
        DialogueManager.Instance.ShowDialogue(tutoDialogue, tutoAudioClips, OnDialogueDone);
    }

    private void OnDialogueDone()
    {
        elevatorTutoAnimator.Play("OpenDoors");
        Invoke("ShowClickBookUI", 0.8f);
        Invoke("ShowTutoBug", showBugAfterDelay);
        Invoke("ShowProjector", showProjectorAfterDelay);
    }

    private void ShowClickBookUI()
    {
        clickBookUI.SetActive(true);
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
        DialogueManager.Instance.ShowDialogue(tutoAfterWarningDialogue, tutoAfterWarningAudioClips, () => Destroy(this));
        timeManager.StartTime();

        clickBookUI.SetActive(false);
    }

    private void OnDestroy()
    {
        Computer.onComputerFix -= OnComputerFix;
    }
}
