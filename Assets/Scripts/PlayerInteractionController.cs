using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionController : MonoBehaviour
{
    [Header("Interact")]
    [SerializeField] private float raycastInteractableInterval = 0.1f;
    [SerializeField] private float pickedObjectPhysicsForce;
    [SerializeField] private float pickedObjectDistance = 4;
    [SerializeField] private float maxInteractDistance;
    [SerializeField] private Transform pickupObjectHand;

    [Header("Input")]
    [SerializeField] private BookController bookController;
    [SerializeField] private FirstPersonController fpsController;
    [SerializeField] private PlayerInput playerInput;

    public ObjectPickable ObjectPicked { get; private set; }
    public bool HoldingObject => ObjectPicked != null;

    private IInteractable hoverInteractable;
    private RaycastHit hit;

    public static PlayerInteractionController Instance { get; private set; } //Singleton

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        StartCoroutine(RaycastInteractable());
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
        if (!HoldingObject) return;

        ObjectPicked.transform.position = Camera.main.transform.position + Camera.main.transform.forward * pickedObjectDistance;
    }

    private void HandlePickupObjectPhysics(Vector2 moveDirection)
    {
        if (ObjectPicked == null) return;
        
        //objectPicked.rb.AddTorque(new Vector3(moveDirection.x, 0, moveDirection.y) * pickedObjectPhysicsForce);
        ObjectPicked.Rb.AddForce(moveDirection * pickedObjectPhysicsForce);
    }

    public void InteractInput(bool interact)
    {
        if (bookController.PickupBook) return;

        if (!interact)
        {
            ObjectPicked?.Release();
            ObjectPicked = null;
            return;
        }

        var interactable = RaycastForInteractable();
        if (interactable == null) return;

        if (interactable is ObjectPickable)
        {
            ObjectPicked = interactable as ObjectPickable;
            ObjectPicked.Pick();
            return;
        }

        if (interactable is IInteractableDisablePlayerMovement)
        {
            fpsController.Disable();
            bookController.CanPickupBook = false;

            ((IInteractableDisablePlayerMovement)interactable).DisablePlayerMovement(ReleaseMovements);
        }

        interactable.Interact();
    }

    private void ReleaseMovements()
    {
        fpsController.Enable();
        bookController.CanPickupBook = true;
    }

    private IEnumerator RaycastInteractable()
    {
        while (true)
        {
            yield return new WaitForSeconds(raycastInteractableInterval);

            if (HoldingObject || bookController.PickupBook)
            {
                hoverInteractable?.ExitHover();
                hoverInteractable = null;
                continue;
            }

            var interactable = RaycastForInteractable();

            if (interactable == null)
            {
                hoverInteractable?.ExitHover();
                hoverInteractable = null;

                continue;
            }

            if (interactable == hoverInteractable) continue;

            hoverInteractable?.ExitHover();
            hoverInteractable = interactable;
            hoverInteractable.Hover();
        }
    }

    private IInteractable? RaycastForInteractable()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxInteractDistance))
        {
            return hit.transform.GetComponent<IInteractable>();
        }

        return null;
    }
}
