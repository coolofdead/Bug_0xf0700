using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    [SerializeField] private float amountExtinguishedPerSecond = 1f;
    [SerializeField] private WaterJet waterJet;
    [SerializeField] private AudioSource SFX_Water;

    void Start()
    {
        
    }

    void Update()
    {

        if (!PlayerInteractionController.Instance.HoldingObject)
        {
            waterJet.StopEmit();
            SFX_Water.volume = 0f;
            Debug.Log("no object");
            return;
        }

        if (!PlayerInteractionController.Instance.ObjectPicked.TryGetComponent(out Extinguisher extinguisher))
        {
            waterJet.StopEmit();
            SFX_Water.volume = 0f;
            Debug.Log("Not extinguisher");
            return;
        }
        
        if (!PlayerInteractionController.Instance.ObjectPicked.isPick)
        {
            waterJet.StopEmit();
            SFX_Water.volume = 0f;
            return;
        }
        
        if (!Input.GetMouseButton(0))
        {
            waterJet.StopEmit();
            SFX_Water.volume = 0f;
            return;
        }

        SFX_Water.volume = 0.5f;
        waterJet.Emit();


        if (!Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 10f))
        {
            return;
        }

        if (!hit.collider.TryGetComponent(out Fire fire))
        {
            return;
        }

        Debug.Log("Extinguishing !");
        fire.TryExtinguish(amountExtinguishedPerSecond * Time.deltaTime);
    }
}
