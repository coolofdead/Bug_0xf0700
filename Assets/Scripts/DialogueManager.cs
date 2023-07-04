using Febucci.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("Dialogue UI")]
    public float delayBeforeShowingNextLongText = 2f;
    public TextMeshProUGUI dialogueTMP;
    public TextAnimatorPlayer textAnimatorPlayer;

    [Header("Phone")]
    public Animator phoneAnimator;
    public AnimationClip pickupPhoneAnimationClip;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip phoneRingingAudioClip;
    public int totalPhoneRingBeforeDialogue = 2;

    [Header("Random Bullshit Dialogues")]
    public float throwRandomDialogueEverySec = 150;
    public BullshitDialogues[] randomDialogues;

    public bool IsShowingDialogue { get; private set; }
    private bool showNextLongDialogue;

    private void Awake()
    {
        Instance = this;

        InvokeRepeating("ThrowRandomBullshitDialogue", 0, throwRandomDialogueEverySec);
    }

    private void ThrowRandomBullshitDialogue()
    {
        if (IsShowingDialogue) return;

        int randomDialogueId = UnityEngine.Random.Range(0, randomDialogues.Length);
        ShowDialogue(randomDialogues[randomDialogueId].dialogues, randomDialogues[randomDialogueId].dialoguesAudioClips);
    }

    public void OnDialogueShown()
    {
        showNextLongDialogue = true;
    }

    private void ShowText(string textToShow)
    {
        textAnimatorPlayer.ShowText(textToShow);
    }

    public void ShowDialogue(string textToShow, AudioClip audioClip = null, Action onLongDialogueDone = null)
    {
        ShowDialogue(new[] { textToShow }, new[] { audioClip }, onLongDialogueDone);
    }

    public void ShowDialogue(string[] textsToShow, AudioClip[] audioClips = null, Action onLongDialogueDone = null)
    {
        StartCoroutine(ShowLongDialogue(textsToShow, audioClips, onLongDialogueDone));
    }

    private IEnumerator ShowLongDialogue(string[] textsToShow, AudioClip[] audioClips = null, Action onLongDialogueDone = null)
    {
        IsShowingDialogue = true;

        audioSource.clip = phoneRingingAudioClip;
        for (int i = 0; i < totalPhoneRingBeforeDialogue; i++)
        {
            audioSource.Play();
            yield return new WaitWhile(() => audioSource.isPlaying);
        }

        phoneAnimator.Play("PickUp");

        yield return new WaitForSeconds(pickupPhoneAnimationClip.length * 0.8f);

        int currentTextIdToShow = 0;
        while (currentTextIdToShow < textsToShow.Length)
        {
            ShowText(textsToShow[currentTextIdToShow]);

            if (currentTextIdToShow < audioClips.Length && audioClips[currentTextIdToShow] != null)
            {
                audioSource.clip = audioClips[currentTextIdToShow];
                audioSource.Play();
            }

            yield return new WaitWhile(() => !showNextLongDialogue);

            showNextLongDialogue = false;
            yield return new WaitForSeconds(delayBeforeShowingNextLongText);

            currentTextIdToShow++;
        }

        phoneAnimator.Play("HangUp");

        onLongDialogueDone?.Invoke();
        IsShowingDialogue = false;
    }

    [Serializable]
    public struct BullshitDialogues
    {
        public string[] dialogues;
        public AudioClip[] dialoguesAudioClips;
    }
}
