using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cinemachine;

public class Computer : MonoBehaviour, IInteractable
{
    [SerializeField] private CinemachineVirtualCamera computerCamera;
    [field: SerializeField] public float interactionDistance { get; set; }
    public bool isInteract { get; set; }
    [field: SerializeField]public bool isInteractable { get; set; }
    [field: SerializeField] public GameObject interactionGUI { get; set; }
    [field: SerializeField] public TextMeshProUGUI interactionText { get; set; }
    public RaycastHit hit { get; set; }
    [field: SerializeField] Transform IInteractable.camera { get; set; }

    private void Update()
    {
        RaycastHit hitInfo;
        isInteractable = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hitInfo, interactionDistance);
        hit = hitInfo;
        
        Interact();
    }
    public void Interact()
    {
        if (!isInteractable)
        {
            return;
        }
        
        if (!hit.transform.CompareTag("Computer"))
        {
            return;
        }

        if (!Input.GetKeyDown(KeyCode.E))
        {
            return;
        }

        if (isInteract)
        {
            computerCamera.enabled = false;
            isInteract = false;
            return;
        }

        isInteract = true;
        computerCamera.enabled = true;
    }
}
