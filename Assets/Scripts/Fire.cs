using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float currentIntensity = 1.0f;

    private float[] startIntensity;

    ParticleSystem[] particle;

    private void Start()
    {
        particle = GetComponentsInChildren<ParticleSystem>();
        startIntensity = new float[particle.Length];
        for (int i = 0; i < particle.Length; i++)
        {
            startIntensity[i] = particle[i].emission.rateOverTimeMultiplier;
        }
    }

    private void Update()
    {
        for (int i = 0; i < particle.Length; i++)
        {
            ChangeIntensity(particle[i], startIntensity[i]);
        }
    }

    private void ChangeIntensity(ParticleSystem particle, float startIntensity)
    {
        var emission = particle.emission;
        emission.rateOverTime = currentIntensity * startIntensity;
    }
}
    

