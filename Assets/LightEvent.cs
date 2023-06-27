using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LightEvent : MonoBehaviour
{
    public static Action onLightTurnOff;
    public static Action onLightTurnOn;

    [Header("Lights")]
    public Image blackScreen;
    public Image blackScreenTransition;
    public float timeToRestoreSomeOpacity;
    [Range(0, 1)] public float blackScreenOpacity;
    public AudioSource audioSource;

    [Header("Dialogue")]
    public bool showDialogueOnce = true;
    public string[] dialoguesOnLightsOff;
    public AudioClip[] dialoguesAudioClipsOnLightsOff;

    private bool hasShownDialogue;

    private void Awake()
    {
        PowerGenerator.onGeneratorFixed += TurnOnAllLights;
    }

    public void TurnOffAllLights()
    {
        blackScreen.color = Color.black;
        audioSource.Play();

        Invoke("RestoreSomeOpacity", timeToRestoreSomeOpacity);
    }

    public void TurnOnAllLights()
    {
        blackScreen.color = new Color(0, 0, 0, 0);

        onLightTurnOn?.Invoke();
    }

    private void RestoreSomeOpacity()
    {
        Color color = Color.black;
        color.a = blackScreenOpacity;
        blackScreen.color = color;

        LeanTween.value(
            blackScreenTransition.gameObject,
            (Color c) => { blackScreenTransition.color = c; },
            Color.black, new Color(0, 0, 0, 0), timeToRestoreSomeOpacity
        );

        onLightTurnOff?.Invoke();

        if (!showDialogueOnce || !hasShownDialogue)
        {
            hasShownDialogue = false;
            DialogueManager.Instance.ShowDialogue(dialoguesOnLightsOff, dialoguesAudioClipsOnLightsOff);
        }
    }

    private void OnDestroy()
    {
        PowerGenerator.onGeneratorFixed -= TurnOnAllLights;
    }
}
