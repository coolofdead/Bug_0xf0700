using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceMusicManager : MonoBehaviour
{
    public AmbianceMusic[] ambianceMusics;

    public AudioSource audioSource;
    
    private void Awake()
    {
        BugsManager.onHack += OnHack;

        StartCoroutine(PlayMusics());
    }

    private void OnHack()
    {
        audioSource.volume *= 0.5f;
        //audioSource.Pause();

        Invoke("RestoreAmbiance", WarningLight.warningLightDuration);
    }

    private void RestoreAmbiance()
    {
        audioSource.volume *= 2f;

        //audioSource.Play();
    }

    private void OnDestroy()
    {
        BugsManager.onHack -= OnHack;
    }

    private IEnumerator PlayMusics()
    {
        int currentTrack = 0;
        while (true)
        {
            audioSource.clip = ambianceMusics[currentTrack].audioClip;
            audioSource.volume = ambianceMusics[currentTrack].volume;
            audioSource.Play();

            yield return new WaitWhile(() => audioSource.isPlaying);

            currentTrack = currentTrack + 1 >= ambianceMusics.Length ? 0 : currentTrack + 1;
        }
    }

    [System.Serializable]
    public struct AmbianceMusic
    {
        public AudioClip audioClip;
        [Range(0, 1)] public float volume;
    }
}
