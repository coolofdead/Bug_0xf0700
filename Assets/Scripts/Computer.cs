using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System;

public class Computer : MonoBehaviour, IInteractableDisablePlayerMovement
{
    public static Action<Computer> onComputerFire;
    public static Action<Computer> onComputerHack;
    public static Action<Computer> onComputerFix;

    [Header("Computer")]
    [SerializeField] private CinemachineVirtualCamera computerCamera;
    [SerializeField] private RansomWare ransomWare;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject bugRedLight;

    [Header("Fire")]
    [SerializeField] private float timeBeforePuttingFire = 15;
    [SerializeField] private Image timeLeftImage;
    [SerializeField] private GameObject timeLeftSlider;
    [field:SerializeField] public Fire fire  { get; private set; }

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
        timeLeftSlider.SetActive(true);

        LeanTween.value(timeLeftImage.gameObject, (float v) => timeLeftImage.fillAmount = v, 0, 1, timeBeforePuttingFire);
        Invoke("StartFire", timeBeforePuttingFire);

        onComputerHack?.Invoke(this);
    }

    private void StartFire()
    {
        if (!IsBugged) return;

        outline.enabled = false;
        fire.StartFire();
    }

    public void Interact()
    {
        if (fire.start) return;

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
        timeLeftSlider.SetActive(false);
        LeanTween.cancel(timeLeftImage.gameObject);

        onComputerFix?.Invoke(this);
    }

    public void Hover()
    {
        if (fire.start) return;

        outline.enabled = true;
        outline.OutlineColor = hoverColor;
    }

    public void ExitHover()
    {
        if (fire.start) return;

        outline.enabled = false;
        outline.OutlineColor = pickedColor;
    }

    public void DisablePlayerMovement(Action releasePlayerMovementCallback)
    {
        this.releasePlayerMovementCallback = releasePlayerMovementCallback;
    }

    public bool IsInteractable()
    {
        return !fire.start;
    }
}
