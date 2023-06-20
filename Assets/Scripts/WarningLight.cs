using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLight : MonoBehaviour
{
    public static float warningLightDuration = 16f;

    public Material warningLightMaterial;
    public Color warningColor;
    public Color disabledColor;
    public GameObject lights;

    private void Awake()
    {
        BugsManager.onHack += SoundAlarm;
    }

    public void SoundAlarm()
    {
        lights.SetActive(true);
        warningLightMaterial.color = warningColor;
        warningLightMaterial.SetColor("_EmissionColor", warningColor * 1);

        CancelInvoke();
        Invoke("StopSoundingAlarm", warningLightDuration);
    }

    public void StopSoundingAlarm()
    {
        lights.SetActive(false);
        warningLightMaterial.SetColor("_EmissionColor", disabledColor);
        warningLightMaterial.color = disabledColor;
    }

    private void OnDestroy()
    {
        BugsManager.onHack -= SoundAlarm;
    }
}
