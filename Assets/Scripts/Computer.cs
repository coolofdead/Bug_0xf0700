using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cinemachine;

public class Computer : MonoBehaviour, IInteractable
{
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

    public void CreateBug()
    {
        IsBugged = true;
        particles.SetActive(true);
        canvas.SetActive(true);
    }

    public void Interact()
    {
        computerCamera.enabled = !computerCamera.enabled;

        ransomWare.EnableInputField(computerCamera.enabled);
    }

    public void FixBug()
    {
        IsBugged = false;
        particles.SetActive(true);
        canvas.SetActive(true);
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
