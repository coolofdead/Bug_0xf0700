using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
using Cinemachine;

public class BookController : MonoBehaviour
{
    public AutoFlip bookAutoFlip;
    public GameObject bookParent;
    public Animator bookAnimator;
    public AnimationClip closeBookAnimation;
    public FirstPersonController firstPersonController;
    public CinemachineVirtualCamera cinemachineVirtualCamera;

    private bool pickupBook = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnPickupBook(InputValue value)
    {
        PickupBook(value.isPressed);
    }

    public void OnBookNextPage(InputValue value)
    {
        if (value.isPressed) BookNextPage();
    }

    public void OnBookPreviousPage(InputValue value)
    {
        if (value.isPressed) BookPreviousPage();
    }

    private void PickupBook(bool pickup)
    {
        pickupBook = !pickupBook;
        
        // Either show or hide book
        firstPersonController.enabled = !pickupBook;
        cinemachineVirtualCamera.enabled = pickupBook;
        if (pickupBook) bookParent.SetActive(true);
        bookAnimator.Play(pickupBook ? "OpenBook" : "CloseBook");
        if (!pickupBook) Invoke("HideBook", closeBookAnimation.averageDuration);
    }

    private void HideBook()
    {
        if (pickupBook) bookParent.SetActive(false);
    }

    private void BookNextPage()
    {
        bookAutoFlip.FlipRightPage();
    }

    private void BookPreviousPage()
    {
        bookAutoFlip.FlipLeftPage();
    }
}
