using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObject : MonoBehaviour
{
    public GameObject sourceLight;

    void Awake()
    {
        LightEvent.onLightTurnOff += TurnOnItem;
        LightEvent.onLightTurnOn += TurnOffItem;
    }

    private void TurnOnItem()
    {
        sourceLight.SetActive(true);
    }

    private void TurnOffItem()
    {
        sourceLight.SetActive(false);
    }

    private void OnDestroy()
    {
        LightEvent.onLightTurnOff -= TurnOnItem;
        LightEvent.onLightTurnOn -= TurnOffItem;
    }
}
