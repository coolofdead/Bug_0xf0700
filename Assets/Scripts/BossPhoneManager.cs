using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhoneManager : MonoBehaviour
{
    [Header("Fire Dialogue")]
    public float fireDelayToShowDialogue = 5f;
    public string fireDialogue = "C'est normal que les alarmes incendies soient activées ?";
    public AudioClip fireDialogueAudio;

    [Header("A lot of bugs Dialogue")]
    public int minBugToSay = 5;
    public string tooMuchBugsDialogue = "Tu n'oublies pas de m'envoyer une photo quand tu as fini";
    public AudioClip tooMuchBugsDialogueAudio;

    private void Start()
    {
        Computer.onComputerHack += OnComputerHack;
        Computer.onComputerFire += OnComputerFire;
    }

    private void OnComputerHack(Computer computer)
    {
        if (BugsManager.Instance.TotalOfComputersHacked >= minBugToSay)
        {
            DialogueManager.Instance.ShowDialogue(tooMuchBugsDialogue, tooMuchBugsDialogueAudio);
        }
    }

    private void OnComputerFire(Computer computer)
    {
        DialogueManager.Instance.ShowDialogue(fireDialogue, fireDialogueAudio);
    }

    private void OnDestroy()
    {
        Computer.onComputerHack -= OnComputerHack;
        Computer.onComputerFire -= OnComputerFire;
    }
}
