using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;


public class WaterJet : MonoBehaviour
{
    ObiEmitter emitter;
    public float emissionSpeed = 10;
    void Start()
    {
        emitter = GetComponentInChildren<ObiEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            emitter.speed = emissionSpeed;
        }
        else
        {
            emitter.speed = 0;
        }
    }
}
