using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
using Cinemachine;

public class BookController : MonoBehaviour
{
    //public AutoFlip bookAutoFlip;
    public BookFix bookFix;
    public Animator bookAnimator;
    public FirstPersonController firstPersonController;
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public GameObject pointerUI;
    public GameObject scrollWheelFeedback;
    public float showScrollWheelFeedbackAfterDelay = 0.7f;

    [HideInInspector] public bool CanPickupBook = true;

    public bool PickupBook { get; private set; } = false;

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
        
        // Either show or hide book
        if (PickupBook) Invoke("ShowScrollWheelFeedback", showScrollWheelFeedbackAfterDelay);
        firstPersonController.enabled = !PickupBook;
        cinemachineVirtualCamera.enabled = PickupBook;
        pointerUI.SetActive(!PickupBook);
        bookAnimator.Play(PickupBook ? "OpenBook" : "CloseBook");
        if (!PickupBook) scrollWheelFeedback.SetActive(false);
    }

    private void BookNextPage()
    {
        bookFix.FlipNextPage();
        scrollWheelFeedback.SetActive(false);
    }

    private void BookPreviousPage()
    {
        bookFix.FlipPreviousPage();
        scrollWheelFeedback.SetActive(false);
    }

    private void ShowScrollWheelFeedback()
    {
        if (bookFix.currentPage == 0) scrollWheelFeedback.SetActive(PickupBook);
    }
}
