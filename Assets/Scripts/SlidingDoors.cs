using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoors : MonoBehaviour
{
    public float openDoorOffset;
    public float openDoorTime;
    public GameObject rightDoor;
    public GameObject leftDoor;
    public Material enableLightMaterial;
    public Material disableLightMaterial;

    public MeshRenderer[] statusIndicatorMaterials;
    public Light[] statusIndicatorLights;
    public Color enableStatusLightColor;
    public Color disableStatusLightColor;

    private Vector3 defaultRightDoorlocalPos;
    private Vector3 defaultLeftDoorlocalPos;

    private bool isEnable = true;

    private void Awake()
    {
        defaultRightDoorlocalPos = rightDoor.transform.localPosition;
        defaultLeftDoorlocalPos = leftDoor.transform.localPosition;

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
        LeanTween.cancel(rightDoor);
        LeanTween.cancel(leftDoor);

        LeanTween.moveLocal(rightDoor, defaultRightDoorlocalPos + Vector3.forward * -openDoorOffset, openDoorTime);
        LeanTween.moveLocal(leftDoor, defaultLeftDoorlocalPos + Vector3.forward * openDoorOffset, openDoorTime);
    }

    public void CloseDoors()
    {
        LeanTween.cancel(rightDoor);
        LeanTween.cancel(leftDoor);

        LeanTween.moveLocal(rightDoor, defaultRightDoorlocalPos, openDoorTime);
        LeanTween.moveLocal(leftDoor, defaultLeftDoorlocalPos, openDoorTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" || !isEnable)
        {
            return;
        }

        OpenDoors();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player" || !isEnable)
        {
            return;
        }

        CloseDoors();
    }

    private void OnDestroy()
    {
        BugsManager.onHack -= OnHackCloseDoors;
    }
}
