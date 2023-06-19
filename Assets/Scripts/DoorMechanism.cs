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
    [field: SerializeField] Transform IInteractable.camera { get; set; }

    void Start () 
	{
		
	}
	

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
        if (!isInteractable)
        {
            popup.Close();
            return;
        }
        
        if (!hit.transform.CompareTag("Door"))
        {
            return;
        }


        if (!hit.transform.TryGetComponent<Door>(out door))
        {
            return;
        }

        popup.Pop();

        if (door.isOpen)
            popup.text.text = "Close the door ?";
        else
            popup.text.text = "Open the door ?";


        if (!Input.GetKeyDown(KeyCode.E))
        {
            return;
        }

        door.isOpen = !door.isOpen;
    }

    public void Hover()
    {
        throw new System.NotImplementedException();
    }

    public void ExitHover()
    {
        throw new System.NotImplementedException();
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
