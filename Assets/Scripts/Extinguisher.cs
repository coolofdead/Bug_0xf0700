using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    [SerializeField] private float amountExtinguishedPerSecond = 1f;
    [SerializeField] private WaterJet waterJet;

    void Start()
    {
        
    }

    void Update()
    {

        if (!PlayerInteractionController.Instance.HoldingObject)
        {
            waterJet.StopEmit();
            Debug.Log("no object");
            return;
        }

        if (!PlayerInteractionController.Instance.ObjectPicked.TryGetComponent(out Extinguisher extinguisher))
        {
            waterJet.StopEmit();
            Debug.Log("Not extinguisher");
            return;
        }
        
        if (!PlayerInteractionController.Instance.ObjectPicked.isPick)
        {
            waterJet.StopEmit();
            return;
        }
        
        if (!Input.GetMouseButton(0))
        {
            waterJet.StopEmit();
            return;
        }

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
