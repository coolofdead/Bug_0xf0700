using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoors : MonoBehaviour
{
    public Animator doorsAnimator;
    public Material enableLightMaterial;
    public Material disableLightMaterial;

    public MeshRenderer[] statusIndicatorMaterials;
    public Light[] statusIndicatorLights;
    public Color enableStatusLightColor;
    public Color disableStatusLightColor;

    private bool isOpening;
    private bool isClosing;

    private bool isEnable = true;

    private void Awake()
    {
        BugsManager.onHack += OnHackCloseDoors;
    }

    private void OnHackCloseDoors()
    {
        isEnable = false;
        foreach (MeshRenderer mr in statusIndicatorMaterials) mr.material = disableLightMaterial;
        foreach (Light light in statusIndicatorLights) light.color = disableStatusLightColor;
        CloseDoors();

        Invoke("FixDoors", 10f);
    }

    public void FixDoors()
    {
        isEnable = true;
        foreach (MeshRenderer mr in statusIndicatorMaterials) mr.material = enableLightMaterial;
        foreach (Light light in statusIndicatorLights) light.color = enableStatusLightColor;
        OpenDoors();
    }

    public void OpenDoors()
    {
        isOpening = true;
        doorsAnimator.Play("OpenDoors");
    }

    public void CloseDoors()
    {
        isClosing = true;
        doorsAnimator.Play("CloseDoors");
    }

    private void DoorsOpened()
    {
        isOpening = false;
    }

    private void DoorsClosed()
    {
        isClosing = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" || isOpening || isClosing || !isEnable)
        {
            if (isClosing) doorsAnimator.SetTrigger("OpenDoorsAfterClosing");
            return;
        }

        OpenDoors();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player" || isOpening || isClosing || !isEnable)
        {
            if (isOpening) doorsAnimator.SetTrigger("CloseDoorsAfterOpening");
            return;
        }

        CloseDoors();
    }

    private void OnDestroy()
    {
        BugsManager.onHack -= OnHackCloseDoors;
    }
}
