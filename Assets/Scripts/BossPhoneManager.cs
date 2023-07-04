using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhoneManager : MonoBehaviour
{
    [Header("Fire Dialogue")]
    public float fireDelayToShowDialogue = 5f;
    public string fireDialogue = "C'est normal que les alarmes incendies soient activées ?";
    public AudioClip fireDialogueAudio;
    private bool hasShownFireDialogue = false;

    [Header("A lot of bugs Dialogue")]
    public int minBugToSay = 5;
    public string tooMuchBugsDialogue = "Tu n'oublies pas de m'envoyer une photo quand tu as fini";
    public AudioClip tooMuchBugsDialogueAudio;
    private bool hasShownLottaBugDialogue = false;

    [Header("Time left Dialogue")]
    public string[] halfTimeLeftDialogue;
    public AudioClip[] halfTimeLeftDialogueAudio;
    public string quarterTimeLeftDialogue = "Tu n'oublies pas de m'envoyer une photo quand tu as fini";
    public AudioClip quarterTimeLeftDialogueAudio;

    private void Start()
    {
        Computer.onComputerHack += OnComputerHack;
        Computer.onComputerFire += OnComputerFire;
        TimeManager.onTimeStart += OnTimeStart;
    }

    private void OnTimeStart(TimeManager timeManager)
    {
        Invoke("HalfTimeDialogue", timeManager.levelDurationInMinutes * 0.5f * 60f);
        Invoke("QuarterTimeDialogue", timeManager.levelDurationInMinutes * 0.75f * 60f);
    }

    private void HalfTimeDialogue()
    {
        DialogueManager.Instance.ShowDialogue(halfTimeLeftDialogue, halfTimeLeftDialogueAudio);
    }

    private void QuarterTimeDialogue()
    {
        DialogueManager.Instance.ShowDialogue(quarterTimeLeftDialogue, quarterTimeLeftDialogueAudio);
    }

    private void OnComputerHack(Computer computer)
    {
        if (BugsManager.Instance.TotalOfComputersHacked >= minBugToSay && !hasShownLottaBugDialogue)
        {
            hasShownLottaBugDialogue = true;
            DialogueManager.Instance.ShowDialogue(tooMuchBugsDialogue, tooMuchBugsDialogueAudio);
        }
    }

    private void OnComputerFire(Computer computer)
    {
        if (!hasShownFireDialogue)
        {
            hasShownFireDialogue = true;
            DialogueManager.Instance.ShowDialogue(fireDialogue, fireDialogueAudio);
        }
    }

    private void OnDestroy()
    {
        Computer.onComputerHack -= OnComputerHack;
        Computer.onComputerFire -= OnComputerFire;
        TimeManager.onTimeStart -= OnTimeStart;
    }
}
