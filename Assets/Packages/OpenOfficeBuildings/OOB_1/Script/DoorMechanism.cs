using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(IInteractable))]
public class DoorMechanism : MonoBehaviour, IInteractable {

	//public GameObject Door;

	public Vector3 doorPosOpen, doorPosClose;

	public float rotSpeed = 1f;

    [field: SerializeField] public float interactionDistance { get; set; }
    public RaycastHit hit { get; set; }    
    public bool isInteract { get; set; }
    public bool isInteractable { get; set; }
    [field: SerializeField] public GameObject interactionGUI { get; set; }
    [field: SerializeField] public TextMeshProUGUI interactionText { get; set; }
    [field: SerializeField] Transform IInteractable.camera { get; set; }

    void Start () 
	{
		
	}
	

	void Update () 
	{
        RaycastHit hitInfo;
        isInteractable = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hitInfo, interactionDistance);
        hit = hitInfo;

        //debug raycast
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * interactionDistance, Color.yellow);

        Interact();
    }

    public void Interact()
    {
        if (!isInteractable)
        {
            interactionGUI.SetActive(false);
            return;
        }

        Debug.Log("Interactable : " + hit);
        
        if (!hit.transform.CompareTag("Door"))
        {
            return;
        }
        
        if (!Input.GetKeyDown(KeyCode.E))
        {
            return;
        }
        
        Open();
    }

    public void Open()
    {
        if (isInteract)
            hit.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(doorPosOpen), rotSpeed * Time.deltaTime);
        else
            hit.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(doorPosClose), rotSpeed * Time.deltaTime);
    }


}
