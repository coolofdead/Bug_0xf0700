using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerGenerator : MonoBehaviour, IInteractable
{
    public static Action onGeneratorFixed;

    public Material enableMaterial;
    public Material disableMaterial;
    public MeshRenderer statusMR;

    public GameObject[] objetcsToDisableWhenPowerOn;

    [Header("Outline")]
    public Outline outline;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip powerDownClip;
    public AudioClip powerUpClip;

    private bool status = true;

    private void Awake()
    {
        LightEvent.onLightTurnOn += TurnOnLight;
        LightEvent.onLightTurnOff += TurnOffLight;
    }

    public void TurnOffLight()
    {
        status = false;
        statusMR.material = disableMaterial;
        foreach (GameObject objetcToDisableWhenPowerOn in objetcsToDisableWhenPowerOn) objetcToDisableWhenPowerOn.SetActive(true);
        audioSource.clip = powerDownClip;
        audioSource.Play();
    }

    public void TurnOnLight()
    {
        status = true;
        statusMR.material = enableMaterial;
        foreach (GameObject objetcToDisableWhenPowerOn in objetcsToDisableWhenPowerOn) objetcToDisableWhenPowerOn.SetActive(false);
        audioSource.clip = powerUpClip;
        audioSource.Play();
    }

    private void OnDestroy()
    {
        LightEvent.onLightTurnOn -= TurnOnLight;
        LightEvent.onLightTurnOff -= TurnOffLight;
    }

    public void Interact()
    {
        if (!status) return;

        outline.enabled = false;
        onGeneratorFixed?.Invoke();
    }

    public void Hover()
    {
        if (!status) outline.enabled = true;
    }

    public void ExitHover()
    {
        if (!status) outline.enabled = false;
    }

    public bool IsInteractable()
    {
        return true;
    }
}
