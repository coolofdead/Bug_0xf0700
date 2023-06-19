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

    private bool showLongDialogue;
    private bool showNextLongDialogue;

    private void Awake()
    {
        Instance = this;
    }

    public void OnDialogueShown()
    {
        if (!showLongDialogue) return;

        showNextLongDialogue = true;
    }

    private void ShowText(string textToShow)
    {
        textAnimatorPlayer.ShowText(textToShow);
    }

    public void ShowDialogue(string textToShow, Action onLongDialogueDone = null)
    {
        ShowDialogue(new[] { textToShow}, onLongDialogueDone);
    }

    public void ShowDialogue(string[] textsToShow, Action onLongDialogueDone = null)
    {
        StartCoroutine(ShowLongDialogue(textsToShow, onLongDialogueDone));
    }

    private IEnumerator ShowLongDialogue(string[] textsToShow, Action onLongDialogueDone = null)
    {
        showLongDialogue = true;

        phoneAnimator.Play("PickUp");

        yield return new WaitForSeconds(pickupPhoneAnimationClip.length * 0.8f);

        int currentTextIdToShow = 0;
        while (currentTextIdToShow < textsToShow.Length)
        {
            ShowText(textsToShow[currentTextIdToShow]);

            yield return new WaitWhile(() => !showNextLongDialogue);

            showNextLongDialogue = false;
            yield return new WaitForSeconds(delayBeforeShowingNextLongText);

            currentTextIdToShow++;
        }

        phoneAnimator.Play("HangUp");

        onLongDialogueDone?.Invoke();
        showLongDialogue = false;
    }
}
