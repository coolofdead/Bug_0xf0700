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

    public Outline outline;

    private bool status;

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
    }

    public void TurnOnLight()
    {
        status = true;
        statusMR.material = enableMaterial;
        foreach (GameObject objetcToDisableWhenPowerOn in objetcsToDisableWhenPowerOn) objetcToDisableWhenPowerOn.SetActive(false);
    }

    private void OnDestroy()
    {
        LightEvent.onLightTurnOn -= TurnOnLight;
        LightEvent.onLightTurnOff -= TurnOffLight;
    }

    public void Interact()
    {
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
