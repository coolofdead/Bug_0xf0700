using Febucci.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public float delayBeforeShowingNextLongText = 2f;
    public TextMeshProUGUI dialogueTMP;
    public TextAnimatorPlayer textAnimatorPlayer;

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

    public void ShowLongDialogues(string[] textsToShow)
    {
        StartCoroutine(ShowLongDialogue(textsToShow));
    }

    private IEnumerator ShowLongDialogue(string[] textsToShow)
    {
        showLongDialogue = true;

        int currentTextIdToShow = 0;
        while (currentTextIdToShow < textsToShow.Length)
        {
            ShowDialogue(textsToShow[currentTextIdToShow]);

            yield return new WaitWhile(() => !showNextLongDialogue);

            showNextLongDialogue = false;
            yield return new WaitForSeconds(delayBeforeShowingNextLongText);

            currentTextIdToShow++;
        }

        showLongDialogue = false;
    }

    public void ShowDialogue(string textToShow)
    {
        textAnimatorPlayer.ShowText(textToShow);
    }
}
