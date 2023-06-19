using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionController : MonoBehaviour
{
    [Header("Interact")]
    [SerializeField] private float pickedObjectPhysicsForce;
    [SerializeField] private float pickedObjectDistance = 4;
    [SerializeField] private float maxInteractDistance;
    [SerializeField] private Transform pickupObjectHand;

    [Header("Input")]
    [SerializeField]  private PlayerInput playerInput;

    private ObjectPickable objectPicked;
    private ObjectPickable hoverObjectPicked;
    private RaycastHit hit;

    private static PlayerInteractionController instance; //Singleton

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public static PlayerInteractionController GetInstance()
    {
        return instance;
    }

    public void OnInteract(InputValue value)
    {
        InteractInput(value.isPressed);
    }

    public void OnLook(InputValue value)
    {
        HandlePickupObjectPhysics(value.Get<Vector2>());
    }
    
    private void Update()
    {
        if (objectPicked == null) return;
        
        objectPicked.transform.position = Camera.main.transform.position + Camera.main.transform.forward * pickedObjectDistance;

        Debug.Log(GetInstance().GetPickedObject().name);
    }

    private void FixedUpdate()
    {
        if (objectPicked != null) return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.forward, out hit, maxInteractDistance))
        {
            var objectToPick = hit.transform.GetComponent<IInteractable>();
            if (objectToPick == null || objectToPick == hoverObjectPicked) return;

            objectToPick.Hover();
            //hoverObjectPicked?.ExitHover();
            //hoverObjectPicked = objectToPick;
            //hoverObjectPicked.Hover();
        }
        else if (hoverObjectPicked != null)
        {
            hoverObjectPicked.ExitHover();
            hoverObjectPicked = null;
        }
    }

    private void HandlePickupObjectPhysics(Vector2 moveDirection)
    {
        if (objectPicked == null) return;

        //objectPicked.rb.AddTorque(new Vector3(moveDirection.x, 0, moveDirection.y) * pickedObjectPhysicsForce);
        objectPicked.rb.AddForce(moveDirection * pickedObjectPhysicsForce);
    }

    public void InteractInput(bool interact)
    {
        if (interact)
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * hit.distance, Color.yellow, 5);

            if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.forward, out hit, maxInteractDistance))
            {
                var objectPickable = hit.transform.GetComponent<IInteractable>();

                if (objectPickable == null) return;

                objectPickable.Interact();

                if (objectPickable is ObjectPickable) PickObject(objectPickable as ObjectPickable);
            }
        }
        else
        {
            ReleaseObject();
        }
    }

    private void PickObject(ObjectPickable newObjectPicked)
    {
        objectPicked = newObjectPicked;
        objectPicked.Pick();
        objectPicked.transform.localPosition = Vector3.zero;
    }

    private void ReleaseObject()
    {
        if (objectPicked == null) return;

        objectPicked.Release();
        objectPicked = null;
    }

    public ObjectPickable GetPickedObject()
    {
        return objectPicked;
    }
}
