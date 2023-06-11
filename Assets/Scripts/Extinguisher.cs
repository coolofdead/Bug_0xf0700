using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    [SerializeField] private float amountExtinguishedPerSecond = 1f;
    void Start()
    {
        
    }

    void Update()
    {
        if (!Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 10f))
        {
            return;
        }

        if (!hit.collider.TryGetComponent(out Fire fire))
        {
            return;
        }

        if (PlayerInteractionController.GetInstance().GetPickedObject() == null)
        {
            Debug.Log("no object");
            return;
        }

        if (!PlayerInteractionController.GetInstance().GetPickedObject().TryGetComponent(out Extinguisher extinguisher))
        {
            Debug.Log("Not extinguisher");
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Debug.Log("Extinguishing !");
            fire.TryExtinguish(amountExtinguishedPerSecond * Time.deltaTime);
        }
    }
}
