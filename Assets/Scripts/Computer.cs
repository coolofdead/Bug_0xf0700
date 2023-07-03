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
    [SerializeField] private GameObject extinguisherBigImage;
    [field:SerializeField] public Fire fire { get; private set; }

    [Header("Outline")]
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color pickedColor;
    [SerializeField] private Outline outline;

    public bool IsBugged { get; private set; } = false;
    [field: SerializeField] public int FloorLevel { get; set; } = 1;

    private Action releasePlayerMovementCallback;

    private void Awake()
    {
        fire.onFireStop += OnFireStop;
    }

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

    private void OnFireStop()
    {
        extinguisherBigImage.SetActive(false);
        canvas.SetActive(false);
    }

    private void StartFire()
    {
        if (!IsBugged) return;

        outline.enabled = false;
        particles.SetActive(false);
        bugRedLight.SetActive(false);
        timeLeftSlider.SetActive(false);
        extinguisherBigImage.SetActive(true);
        LeanTween.cancel(timeLeftImage.gameObject);

        fire.StartFire();
    }

    public void Interact()
    {
        if (!IsInteractable()) return;

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

        computerCamera.enabled = false;
        releasePlayerMovementCallback?.Invoke();
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
