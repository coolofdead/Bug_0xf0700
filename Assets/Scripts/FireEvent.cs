using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireEvent : MonoBehaviour
{
    private Slider slider;
    void Start()
    {
        if (TryGetComponent<Slider>(out slider))
        {
            StartCoroutine(ProgressBar());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private IEnumerator ProgressBar()
    {
        while (slider.value >= 0)
        {
            slider.value -= 0.01f;
            yield return new WaitForSeconds(1f);
        }
    }
}
