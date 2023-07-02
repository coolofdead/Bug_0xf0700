using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireEvent : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private Fire fire;
    void Start()
    {
        fire.Stop();
        if (TryGetComponent<Slider>(out slider))
        {
            foreach(var img in slider.GetComponentsInChildren<Image>())
            {
                img.enabled = true;
            }
            StartCoroutine(ProgressBar());
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    private IEnumerator ProgressBar()
    {
        Debug.Log("PROGGGGRE");
        while (slider.value > 0)
        {
            slider.value -= 0.01f;
            yield return new WaitForSeconds(0.1f);
        }

        StartFire();
    }

    private void StartFire()
    {
        Debug.Log("Start Fire ! ");
        fire.StartFire();
        foreach (var img in slider.GetComponentsInChildren<Image>())
        {
            img.enabled = false;
        }
        //fire.enabled = true;
    }
}
