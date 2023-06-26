using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cinemachine;
using System;

public class Computer : MonoBehaviour, IInteractableDisablePlayerMovement
{
    public static Action<Computer> onComputerFire;
    public static Action<Computer> onComputerHack;
    public static Action<Computer> onComputerFix;

    [SerializeField] private CinemachineVirtualCamera computerCamera;
    [SerializeField] private RansomWare ransomWare;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject bugRedLight;

    [Header("Outline")]
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color pickedColor;
    [SerializeField] private Outline outline;

    private TMP_SelectionCaret caret;

    public bool IsBugged { get; private set; }
    [field: SerializeField] public int FloorLevel { get; set; } = 1;

    private Action releasePlayerMovementCallback;

    public void CreateBug()
    {
        IsBugged = true;
        particles.SetActive(true);
        canvas.SetActive(true);
        bugRedLight.SetActive(true);
        
        onComputerHack?.Invoke(this);
    }

    public void Interact()
    {
        computerCamera.enabled = !computerCamera.enabled;

        ransomWare.EnableInputField(computerCamera.enabled);

        if (!computerCamera.enabled) releasePlayerMovementCallback?.Invoke();
    }

    public void FixBug()
    {
        IsBugged = false;
        particles.SetActive(false);
        canvas.SetActive(false);
        bugRedLight.SetActive(false);

        onComputerFix?.Invoke(this);
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

    public void DisablePlayerMovement(Action releasePlayerMovementCallback)
    {
        this.releasePlayerMovementCallback = releasePlayerMovementCallback;
    }
}
