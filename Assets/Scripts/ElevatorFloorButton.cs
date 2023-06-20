using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElevatorFloorButton : MonoBehaviour, IInteractable
{
    [Header("Outline")]
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color pickedColor;
    [SerializeField] private Outline outline;

    public Elevator elevator;
    public int targetFloorLevel;

    public void Interact()
    {
        elevator.PickFloor(targetFloorLevel);
    }

    public void Hover()
    {
        outline.enabled = true;
        outline.OutlineColor = hoverColor;
    }

    public void ExitHover()
    {
        outline.enabled = false;
        outline.OutlineColor = pickedColor;
    }
}
