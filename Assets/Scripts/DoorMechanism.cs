using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(IInteractable))]
public class DoorMechanism : MonoBehaviour, IInteractable {

	private Door door;
    [SerializeField] private UI_PopUp popup;
    [field: SerializeField] public float interactionDistance { get; set; }
    public RaycastHit hit { get; set; }    
    public bool isInteract { get; set; }
    public bool isInteractable { get; set; }
    [field: SerializeField] public GameObject interactionGUI { get; set; }
    [field: SerializeField] public TextMeshProUGUI interactionText { get; set; }
    public KeyHolder keyHolder;

	void Update () 
	{
        RaycastHit hitInfo;
        isInteractable = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hitInfo, interactionDistance);
        hit = hitInfo;

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * interactionDistance, Color.yellow);

        Interact();
    }

    public void Interact()
    {
        if (!isInteractable) // Check for raycast hit
        {
            popup.Close();
            return;
        }
        
        if (!hit.transform.CompareTag("Door")) // Check if the hit object is a door
        {
            return;
        }


        if (!hit.transform.TryGetComponent<Door>(out door)) // Check if the hit object have a Door component
        {
            return;
        }

        if (door.isLock) // Check if the door is lock
        {
            popup.text.text = "Unlock the door ?";
        }

        popup.Pop();

        if (door.isOpen)
            popup.text.text = "Close the door ?";
        else if (!door.isLock)
            popup.text.text = "Open the door ?";


        if (!Input.GetKeyDown(KeyCode.E)) // Check input interaction with the door
        {
            return;
        }

        if (!door.isLock) // if the door has no key needed, open the door
        {
            door.isOpen = !door.isOpen;
            return;
        }
        
        if (door.isLock) // if the door need a key, check if the player have the key
        {
            door.TryGetComponent<KeyDoor>(out var keyDoor);
            door.isLock = !keyHolder.ContainsKey(keyDoor.GetKeyType());
            door.isOpen = !door.isLock;
            return;
        }
    }

    public void Hover()
    {
    }

    public void ExitHover()
    {
    }

    //public void Open()
    //{
    //    if (!hit.transform.GetComponent<Door>().isOpen)
    //    {
    //        hit.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(doorPosOpen), rotSpeed * Time.deltaTime);
    //        hit.transform.GetComponent<Door>().isOpen = true;
    //        Debug.Log("Open the door");
    //    }
    //    else
    //    {
    //        hit.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(doorPosClose), rotSpeed * Time.deltaTime);
    //        hit.transform.GetComponent<Door>().isOpen = false;
    //        Debug.Log("Close the door");
    //    }

    //}


}
