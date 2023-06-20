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
    public Color defaultElevatorColor = Color.white;

    [Header("Outline")]
    [SerializeField] private Outline outline;

    private void Awake()
    {
        ResetColor();
    }

    public void Interact()
    {
        btnMaterial.SetColor("_EmissionColor", calledElevatorColor * 10);
        elevatorFront.CallElevator();
    }

    public void Hover()
    {
        outline.enabled = true;
    }

    public void ExitHover()
    {
        outline.enabled = false;
    }

    public void ResetColor()
    {
        btnMaterial.SetColor("_EmissionColor", defaultElevatorColor * 10);
    }
}
