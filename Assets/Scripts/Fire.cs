using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [Header("Fire")]
    [SerializeField, Range(0f, 1f)] private float currentIntensity = 0.1f;
    [SerializeField] private float regenDelay = 2.5f;
    [SerializeField] private float regenRate = 0.1f;

    [Header("Fire Spread")]
    [SerializeField] private float timeBeforeSpreading = 10f;
    [SerializeField] private float spreadRange = 1f;

    private ParticleSystem[] particle;
    private AudioSource sfx;
    private float[] startIntensity;
    private float startVolume;
    private float timeLastWatered = 0;
    public bool isLit { get; private set; } = true;
    public bool start { get; private set; } = false;

    private void Awake()
    {
        start = false;

        if (TryGetComponent(out sfx))
        {
            startVolume = sfx.volume;
        }

        particle = GetComponentsInChildren<ParticleSystem>();
        startIntensity = new float[particle.Length];
        for (int i = 0; i < particle.Length; i++)
        {
            startIntensity[i] = particle[i].emission.rateOverTimeMultiplier;
        }
    }

    private void Update()
    {
        if (isLit && currentIntensity < 1.0f && Time.time - timeLastWatered >= regenDelay && start)
        {
            currentIntensity += regenRate * Time.deltaTime;
            
            for (int i = 0; i < particle.Length; i++)
            {
                ChangeIntensity(particle[i], startIntensity[i]);
            }

            ChangeVolume(sfx, startVolume);
        }
    }

    private void ChangeIntensity(ParticleSystem particle, float startIntensity)
    {
        var emission = particle.emission;
        emission.rateOverTime = currentIntensity * startIntensity;
    }

    private void ChangeVolume(AudioSource sfx, float startVolume)
    {
        if (sfx != null)
        {
            sfx.volume = currentIntensity * startVolume;
        }
    }

    public bool TryExtinguish(float amount)
    {
        timeLastWatered = Time.time;
        
        currentIntensity -= amount;

        for (int i = 0; i < particle.Length; i++)
        {
            ChangeIntensity(particle[i], startIntensity[i]);
        }

        ChangeVolume(sfx, startVolume);

        if (currentIntensity <= 0)
        {
            isLit = false;
            return true;
        }
        
        return false;
    }

    private void StartSpreading()
    {
        Ray ray = new Ray(transform.position, Vector3.one);
        foreach (var hit in Physics.SphereCastAll(ray, spreadRange))
        {
            if (hit.collider.TryGetComponent(out Computer computer))
            {
                if (!computer.fire.start)
                {
                    computer.fire.StartFire();
                    break;
                }
            }
        }
    }

    public void Stop()
    {
        start = false;
        for (int i = 0; i < particle.Length; i++)
        {
            particle[i].Stop();
        }
    }

    public void StartFire()
    {
        start = true;
        for (int i = 0; i < particle.Length; i++)
        {
            particle[i].Play();
        }
    }
}
