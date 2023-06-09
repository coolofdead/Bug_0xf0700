using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLight : MonoBehaviour
{
    public Material warningLightMaterial;
    public Color warningColor;
    public Color disabledColor;
    public GameObject lights;

    public void SoundAlarm()
    {
        lights.SetActive(true);
        warningLightMaterial.color = warningColor;
        warningLightMaterial.SetColor("_EmissionColor", warningColor * 1);
    }

    public void StopSoundingAlarm()
    {
        lights.SetActive(false);
        warningLightMaterial.SetColor("_EmissionColor", disabledColor);
        warningLightMaterial.color = disabledColor;
    }
}
