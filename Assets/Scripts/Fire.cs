using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float currentIntensity = 1.0f;
    [SerializeField] private float regenDelay = 2.5f;
    [SerializeField] private float regenRate = 0.1f;


    private float[] startIntensity;

    private ParticleSystem[] particle;

    private float timeLastWatered = 0;

    private bool isLit = true;

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
        if (isLit && currentIntensity < 1.0f && Time.time - timeLastWatered >= regenDelay)
        {
            currentIntensity += regenRate * Time.deltaTime;
            
            for (int i = 0; i < particle.Length; i++)
            {
                ChangeIntensity(particle[i], startIntensity[i]);
            }
        }
        //for (int i = 0; i < particle.Length; i++)
        //{
        //    ChangeIntensity(particle[i], startIntensity[i]);
        //}
    }

    private void ChangeIntensity(ParticleSystem particle, float startIntensity)
    {
        var emission = particle.emission;
        emission.rateOverTime = currentIntensity * startIntensity;
    }

    public bool TryExtinguish(float amount)
    {
        timeLastWatered = Time.time;
        
        currentIntensity -= amount;

        for (int i = 0; i < particle.Length; i++)
        {
            ChangeIntensity(particle[i], startIntensity[i]);
        }

        if (currentIntensity <= 0)
        {
            isLit = false;
            return true;
        }
        
        return false;
    }
}
    

