using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSign : MonoBehaviour
{
    public Material exitSignMaterial;
    public Color emissionColor;
    public float minEmission = 0.8f;
    public float maxEmission = 1.4f;
    public float updateTime = 0.7f;

    private void Awake()
    {
        StartCoroutine(UpdateEmissionValue());
    }

    private IEnumerator UpdateEmissionValue()
    {
        float currentTime = 0;
        bool reverse = false;

        while (true)
        {
            currentTime += Time.deltaTime;
            float currentEmission = Mathf.Lerp(reverse ? minEmission : maxEmission, reverse ? maxEmission : minEmission, currentTime / updateTime);
            exitSignMaterial.SetColor("_EmissionColor", emissionColor * currentEmission);
            yield return null;
            if (currentTime >= updateTime)
            {
                currentTime = 0;
                reverse = !reverse;
            }
        }
    }
}
