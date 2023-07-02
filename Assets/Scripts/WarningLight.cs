using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLight : MonoBehaviour
{
    public static float warningLightDuration = 8f;

    public Material warningLightMaterial;
    public Color warningColor;
    public Color disabledColor;
    public GameObject lights;

    private void Awake()
    {
        BugsManager.onHack += SoundAlarm;
        StopSoundingAlarm();
    }

    public void SoundAlarm()
    {
        lights.SetActive(true);
        warningLightMaterial.color = warningColor;
        warningLightMaterial.SetColor("_EmissionColor", warningColor * 10f);
        warningLightMaterial.EnableKeyword("_EMISSION");

        CancelInvoke();
        Invoke("StopSoundingAlarm", warningLightDuration);
    }

    public void StopSoundingAlarm()
    {
        lights.SetActive(false);
        warningLightMaterial.SetColor("_EmissionColor", disabledColor);
        warningLightMaterial.DisableKeyword("_EMISSION");
        warningLightMaterial.color = disabledColor;
    }

    private void OnDestroy()
    {
        BugsManager.onHack -= SoundAlarm;
    }
}
