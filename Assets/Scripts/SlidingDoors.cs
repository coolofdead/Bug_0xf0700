using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoors : MonoBehaviour
{
    public Animator doorsAnimator;

    private bool isOpening;
    private bool isClosing;

    private void DoorsOpened()
    {
        isOpening = false;
    }

    private void DoorsClosed()
    {
        isClosing = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" || isOpening || isClosing)
        {
            if (isClosing) doorsAnimator.SetTrigger("OpenDoorsAfterClosing");
            return;
        }

        isOpening = true;
        doorsAnimator.Play("OpenDoors");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player" || isOpening || isClosing)
        {
            if (isOpening) doorsAnimator.SetTrigger("CloseDoorsAfterOpening");
            return;
        }

        isClosing = true;
        doorsAnimator.Play("CloseDoors");
    }
}
