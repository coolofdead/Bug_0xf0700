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

    [Header("Btn TMP")]
    public TextMeshProUGUI floorBtnTMP;
    public Color defaultColor;
    public Color errorColor;

    [Header("Elevator")]
    public Elevator elevator;
    public int targetFloorLevel;

    private void Awake()
    {
        elevator.onElevatorError += OnElevatorError;
        elevator.onElevatorErrorResolved += OnElevatorErrorResolved;
    }

    private void OnElevatorError()
    {
        floorBtnTMP.color = errorColor;
    }

    private void OnElevatorErrorResolved()
    {
        floorBtnTMP.color = defaultColor;
    }

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

    public bool IsInteractable()
    {
        return true;
    }

    private void OnDestroy()
    {
        elevator.onElevatorError -= OnElevatorError;
        elevator.onElevatorErrorResolved -= OnElevatorErrorResolved;
    }
}
