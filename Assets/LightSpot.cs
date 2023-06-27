using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpot : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        LightEvent.onLightTurnOn += TurnOnLight;
        LightEvent.onLightTurnOff += TurnOffLight;
    }

    public void TurnOffLight()
    {
        animator.Play("LightCut");
    }

    public void TurnOnLight()
    {
        animator.SetTrigger("TurnBackLights");
    }

    private void OnDestroy()
    {
        LightEvent.onLightTurnOn -= TurnOnLight;
        LightEvent.onLightTurnOff -= TurnOffLight;
    }
}
