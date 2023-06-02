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
    public GameObject bookParent;
    public Animator bookAnimator;
    public AnimationClip closeBookAnimation;
    public FirstPersonController firstPersonController;
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public GameObject pointerUI;
    public GameObject scrollWheelFeedback;
    public float showScrollWheelFeedbackAfterDelay = 0.7f;

    private bool pickupBook = false;

    public void OnPickupBook(InputValue value)
    {
        PickupBook(value.isPressed);
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

    private void PickupBook(bool pickup)
    {
        pickupBook = !pickupBook;
        
        // Either show or hide book
        if (pickupBook) Invoke("ShowScrollWheelFeedback", showScrollWheelFeedbackAfterDelay);
        firstPersonController.enabled = !pickupBook;
        cinemachineVirtualCamera.enabled = pickupBook;
        if (pickupBook) bookParent.SetActive(true);
        pointerUI.SetActive(!pickupBook);
        bookAnimator.Play(pickupBook ? "OpenBook" : "CloseBook");
        if (!pickupBook) scrollWheelFeedback.SetActive(false);
        if (!pickupBook) Invoke("HideBook", closeBookAnimation.length);
    }

    private void HideBook()
    {
        bookParent.SetActive(false);
    }

    private void BookNextPage()
    {
        bookFix.FlipNextPage();
    }

    private void BookPreviousPage()
    {
        bookFix.FlipPreviousPage();
    }

    private void ShowScrollWheelFeedback()
    {
        if (bookFix.currentPage == 0) scrollWheelFeedback.SetActive(true);
    }
}
