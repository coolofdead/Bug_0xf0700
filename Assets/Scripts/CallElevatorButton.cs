using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallElevatorButton : MonoBehaviour, IInteractable
{
    [Header("Elevator")]
    public ElevatorFront elevatorFront;

    [Header("Btn")]
    public Material btnMaterial;
    public Color calledElevatorColor = new Color(0, 1.754318f, 0, 1);
    public Color errorElevatorColor = Color.white;
    public Color defaultElevatorColor = Color.white;

    [Header("Outline")]
    [SerializeField] private Outline outline;

    public bool status = true;

    private void Awake()
    {
        ResetColor();
    }

    public void Interact()
    {
        if (!status) return;

        btnMaterial.SetColor("_EmissionColor", calledElevatorColor * 10);
        elevatorFront.CallElevator();
    }

    public void Hover()
    {
        if (!status) return;

        outline.enabled = true;
    }

    public void ExitHover()
    {
        if (!status) return;

        outline.enabled = false;
    }

    public void ResetColor()
    {
        btnMaterial.SetColor("_EmissionColor", defaultElevatorColor * 10);
    }

    public bool IsInteractable()
    {
        return true;
    }
}
