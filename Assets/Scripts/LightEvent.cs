using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LightEvent : MonoBehaviour
{
    public static Action onLightTurnOff;
    public static Action onLightTurnOn;

    [Header("Event")]
    [Range(0, 100)] public float lightEventRate = 40;
    public float lightEventEverySec = 40;

    [Header("Lights")]
    public Image blackScreen;
    public Image blackScreenTransition;
    public Image powerImage;
    public float timeToRestoreSomeOpacity;
    public float timeToHideBlackScreen = 0.7f;
    public float timeBeforeHavingBug = 60;
    [Range(0, 1)] public float blackScreenOpacity;
    public AudioSource audioSource;
    private float lastLightOffTime;

    [Header("Dialogue")]
    public bool showDialogueOnce = true;
    public string[] dialoguesOnLightsOff;
    public AudioClip[] dialoguesAudioClipsOnLightsOff;

    public bool AreLightsDown => blackScreen.color.a != 0;
    public static int NbLightEvents { get; private set; }

    private bool hasShownDialogue;

    private void Awake()
    {
        PowerGenerator.onGeneratorFixed += TurnOnAllLights;
        InvokeRepeating("TryToTurnOffLights", 0, lightEventEverySec);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L)) TurnOffAllLights();
    }

    private void TryToTurnOffLights()
    {
        if (AreLightsDown || BugsManager.Instance.TotalOfComputersFixed <= 2 || Time.time < lastLightOffTime + timeBeforeHavingBug) return;

        if (UnityEngine.Random.Range(0, 100) > lightEventRate) return;

        TurnOffAllLights();
    }

    public void TurnOffAllLights()
    {
        NbLightEvents++;

        blackScreen.color = Color.black;
        powerImage.gameObject.SetActive(true);
        LeanTween.value(
            powerImage.gameObject,
            (Color c) => { powerImage.color = c; },
            Color.black, Color.white, 0.7f
        ).setLoopPingPong();
        audioSource.Play();

        Invoke("RestoreSomeOpacity", timeToRestoreSomeOpacity);
    }

    public void TurnOnAllLights()
    {
        lastLightOffTime = Time.time;
        LeanTween.value(
            blackScreen.gameObject,
            (Color c) => { blackScreen.color = c; },
            blackScreen.color, new Color(0, 0, 0, 0), timeToHideBlackScreen
        );

        onLightTurnOn?.Invoke();
    }

    private void RestoreSomeOpacity()
    {
        Color color = Color.black;
        color.a = blackScreenOpacity;
        blackScreen.color = color;
        LeanTween.cancel(powerImage.gameObject);
        powerImage.gameObject.SetActive(false);
        LeanTween.value(
            blackScreenTransition.gameObject,
            (Color c) => { blackScreenTransition.color = c; },
            Color.black, new Color(0, 0, 0, 0), timeToRestoreSomeOpacity
        );

        onLightTurnOff?.Invoke();

        if (!showDialogueOnce || !hasShownDialogue)
        {
            hasShownDialogue = true;
            DialogueManager.Instance.ShowDialogue(dialoguesOnLightsOff, dialoguesAudioClipsOnLightsOff);
        }
    }

    private void OnDestroy()
    {
        PowerGenerator.onGeneratorFixed -= TurnOnAllLights;
    }
}
