using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
using Cinemachine;

public class BookController : MonoBehaviour
{
    public BookFix bookFix;
    public Animator bookAnimator;
    public FirstPersonController firstPersonController;
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public GameObject pointerUI;
    public GameObject scrollWheelFeedback;
    public float showScrollWheelFeedbackAfterDelay = 0.7f;

    [Header("Fucked Up Rotation")]
    public Vector3 fuckedUpBookRotation = new Vector3(-0.000169047184f, 74.9999847f, 270.000061f);
    public Vector3 defaultBookRotation = new Vector3(0, 285, 90);
    [Range(0f, 1f)] public float chanceOpenBookFuckedUp = 0.3f;

    [HideInInspector] public bool CanPickupBook = true;

    [field:SerializeField] public bool PickupBook { get; private set; } = false;

    private bool hasShownMouseScrollFeedback;

    public void OnPickupBook(InputValue value)
    {
        PickupBookInHand(value.isPressed);
    }

    public void OnBookScrollPage(InputValue value)
    {
        var scrollValue = value.Get<float>();
        if (scrollValue > 0)
        {
            BookPreviousPage();
        }
        else if (scrollValue < 0)
        {
            BookNextPage();
        }
    }

    public void OnBookNextPage(InputValue value)
    {
        if (value.isPressed)
        {
            scrollWheelFeedback.SetActive(false);
            BookNextPage();
        }
    }

    public void OnBookPreviousPage(InputValue value)
    {
        if (value.isPressed)
        {
            scrollWheelFeedback.SetActive(false);
            BookPreviousPage();
        }
    }

    private void PickupBookInHand(bool pickup)
    {
        if (!CanPickupBook) return;

        PickupBook = !PickupBook;

        CancelInvoke();
        // Either show or hide book
        if (PickupBook) Invoke("ShowScrollWheelFeedback", showScrollWheelFeedbackAfterDelay);
        if (PickupBook)
            firstPersonController.Disable();
        else
            firstPersonController.Enable();
        cinemachineVirtualCamera.enabled = PickupBook;
        pointerUI.SetActive(!PickupBook);
        bookAnimator.Play(PickupBook ? "OpenBook" : "CloseBook");
        if (!PickupBook) scrollWheelFeedback.SetActive(false);

        if (PickupBook) bookFix.transform.localRotation = Random.Range(0f, 1f) <= chanceOpenBookFuckedUp ? Quaternion.Euler(fuckedUpBookRotation) : Quaternion.Euler(defaultBookRotation);
    }

    private void BookNextPage()
    {
        if (!PickupBook) return;

        bookFix.FlipNextPage();
        scrollWheelFeedback.SetActive(false);
    }

    private void BookPreviousPage()
    {
        if (!PickupBook) return;

        bookFix.FlipPreviousPage();
        scrollWheelFeedback.SetActive(false);
    }

    private void ShowScrollWheelFeedback()
    {
        if (bookFix.currentPage == 0 && PickupBook)
        {
            hasShownMouseScrollFeedback = true;
            scrollWheelFeedback.SetActive(PickupBook);
        }
    }
}
