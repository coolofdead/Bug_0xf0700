using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cinemachine;
using System;

public class Computer : MonoBehaviour, IInteractable
{
    public static Action<Computer> onComputerHack;

    [SerializeField] private CinemachineVirtualCamera computerCamera;
    [SerializeField] private RansomWare ransomWare;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject particles;

    [Header("Outline")]
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color pickedColor;
    [SerializeField] private Outline outline;

    private TMP_SelectionCaret caret;

    public bool IsBugged { get; private set; }
    public int FloorLevel { get; set; }

    public void CreateBug()
    {
        IsBugged = true;
        particles.SetActive(true);
        canvas.SetActive(true);

        onComputerHack?.Invoke(this);
    }

    public void Interact()
    {
        computerCamera.enabled = !computerCamera.enabled;

        ransomWare.EnableInputField(computerCamera.enabled);
    }

    public void FixBug()
    {
        IsBugged = false;
        particles.SetActive(false);
        canvas.SetActive(false);
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
